using LightweightCharts.Blazor.Customization;
using System;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;

namespace LightweightCharts.Blazor.Utilities
{
	partial class RgbaFormatRegex
	{
		[GeneratedRegex(@"rgba\((?<r>[0-9]{1,3}), (?<g>[0-9]{1,3}), (?<b>[0-9]{1,3}), (?<a>[0-9\.]{1,5})\)")]
		public static partial Regex MatchRgba();

		[GeneratedRegex(@"rgb\((?<r>[0-9]{1,3}), (?<g>[0-9]{1,3}), (?<b>[0-9]{1,3})\)")]
		public static partial Regex MatchRgb();
	}

	/// <summary>
	/// expension methods
	/// </summary>
	public static class Extensions
	{
		static Extensions()
		{
#if DEBUG
			var optionTypes = typeof(Extensions).Assembly.DefinedTypes.Where(x => typeof(JsonOptionsObject).IsAssignableFrom(x));
			foreach (var type in optionTypes)
				JsonOptionsObject.ValidateConverterAttribute(type);
#endif
		}

		/// <summary>
		/// Allows custom color parsing in the <see cref="ParseColorCode"/> method
		/// </summary>
		public static Func<string, Color> CustomColorParser { get; set; }

		internal static void SetValue<T>(ref T field, T value, Action<T> propertyChanged)
		{
			if (!Equals(field, value))
			{
				var prev = field;
				field = value;
				propertyChanged?.Invoke(prev);
			}
		}

		/// <summary>
		/// unix timestamp to DateTime
		/// </summary>
		/// <param name="unixTime"></param>
		/// <returns></returns>
		public static DateTime UnixTimeToDateTime(long unixTime)
			=> DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;

		/// <summary>
		/// DateTime to unix timestamp
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static long DateTimeToUnixTime(DateTime dateTime)
			=> new DateTimeOffset(dateTime).ToUnixTimeSeconds();

		/// <summary>
		/// Accepted formats are RGB and RGBA with two values per channel (ex: #FF0000 = red, #00FF0088 = green, 50% alpha)
		/// </summary>
		public static Color ParseColorCode(string value)
		{
			if (CustomColorParser != null)
				return CustomColorParser(value);

			if (string.IsNullOrEmpty(value))
				return Color.Empty;

			if (value.StartsWith('#'))
				return ColorTranslator.FromHtml(value);

			if (value.StartsWith("rgba("))
			{
				var match = RgbaFormatRegex.MatchRgba().Match(value);
				var a = (int)(float.Parse(match.Groups["a"].Value) * 255);
				var r = int.Parse(match.Groups["r"].Value);
				var g = int.Parse(match.Groups["g"].Value);
				var b = int.Parse(match.Groups["b"].Value);
				return Color.FromArgb(a, r, g, b);
			}

			if (value.StartsWith("rgb("))
			{
				var match = RgbaFormatRegex.MatchRgb().Match(value);
				var r = int.Parse(match.Groups["r"].Value);
				var g = int.Parse(match.Groups["g"].Value);
				var b = int.Parse(match.Groups["b"].Value);
				return Color.FromArgb(r, g, b);
			}

			System.Diagnostics.Debug.Assert(false, "color value not handled");
			return Color.Empty;
		}
	}
}
