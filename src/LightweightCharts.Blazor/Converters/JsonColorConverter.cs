using LightweightCharts.Blazor.Utilities;
using System;
using System.Drawing;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Converters
{
	partial class JsonColorConverter : JsonConverter<Color>
	{
		public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var value = reader.GetString();
			return Extensions.ParseColorCode(value);
		}

		public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
		{
			string hexValue;
			if (value.A != 255)
				hexValue = $"{value.R:X2}{value.G:X2}{value.B:X2}{value.A:X2}";
			else
				hexValue = $"{value.R:X2}{value.G:X2}{value.B:X2}";

			writer.WriteStringValue("#" + hexValue);
		}
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
