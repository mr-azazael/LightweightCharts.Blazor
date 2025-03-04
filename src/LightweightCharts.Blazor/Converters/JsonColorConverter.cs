using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace LightweightCharts.Blazor.Converters
{
	partial class JsonColorConverter : JsonConverter<Color>
	{
		public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = reader.GetString();
			if (string.IsNullOrEmpty(value))
				return Color.Empty;

			if (value.StartsWith('#'))
				return ColorTranslator.FromHtml(value);

			if (value.StartsWith("rgba("))
			{
				var match = MatchRgba().Match(value);
				var a = int.Parse(match.Groups["a"].Value);
				var r = int.Parse(match.Groups["r"].Value);
				var g = int.Parse(match.Groups["g"].Value);
				var b = int.Parse(match.Groups["b"].Value);
				return Color.FromArgb(a, r, g, b);
			}

			if (value.StartsWith("rgb("))
			{
				var match = MatchRgb().Match(value);
				var r = int.Parse(match.Groups["r"].Value);
				var g = int.Parse(match.Groups["g"].Value);
				var b = int.Parse(match.Groups["b"].Value);
				return Color.FromArgb(r, g, b);
			}

			System.Diagnostics.Debug.Assert(false, "color value not handled");
			return Color.Empty;
		}

		public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
		{
			var hexValue = $"{value.R:X2}{value.G:X2}{value.B:X2}{value.A:X2}";
			writer.WriteStringValue("#" + hexValue);
		}

		[GeneratedRegex(@"rgba\((?<r>[0-9]{1,3}), (?<g>[0-9]{1,3}), (?<b>[0-9]{1,3}), (?<a>[0-9]{1,3})\)")]
		private static partial Regex MatchRgba();

		[GeneratedRegex(@"rgb\((?<r>[0-9]{1,3}), (?<g>[0-9]{1,3}), (?<b>[0-9]{1,3})\)")]
		private static partial Regex MatchRgb();
	}

	class JsonOptionalColorConverter : JsonConverter<Color?>
	{
		static JsonColorConverter _Default = new();

		public override Color? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			return _Default.Read(ref reader, typeToConvert, options);
		}

		public override void Write(Utf8JsonWriter writer, Color? value, JsonSerializerOptions options)
		{
			if (value == null)
			{
				writer.WriteNullValue();
				return;
			}

			_Default.Write(writer, value.Value, options);
		}
	}
}
