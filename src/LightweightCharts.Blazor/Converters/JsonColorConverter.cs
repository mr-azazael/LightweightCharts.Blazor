using System;
using System.Drawing;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Converters
{
	class JsonColorConverter : JsonConverter<Color>
	{
		public override Color Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
		{
			var hexValue = reader.GetString();
			if (!hexValue.StartsWith('#'))
				throw new InvalidOperationException();

			byte r = 0;
			byte g = 0;
			byte b = 0;
			byte a = 255;

			r = byte.Parse(hexValue.Substring(1, 2), NumberStyles.HexNumber);
			g = byte.Parse(hexValue.Substring(3, 2), NumberStyles.HexNumber);
			b = byte.Parse(hexValue.Substring(5, 2), NumberStyles.HexNumber);

			if (hexValue.Length > 7)
				a = byte.Parse(hexValue.Substring(7, 2), NumberStyles.HexNumber);

			return Color.FromArgb(a, r, g, b);
		}

		public override void Write(Utf8JsonWriter writer, Color value, JsonSerializerOptions options)
		{
			var hexValue = $"{value.R:X2}{value.G:X2}{value.B:X2}{value.A:X2}";
			writer.WriteStringValue("#" + hexValue);
		}
	}
}
