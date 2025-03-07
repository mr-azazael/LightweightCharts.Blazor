using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/TextWatermarkLineOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<TextWatermarkLineOptions>))]
	public class TextWatermarkLineOptions : JsonOptionsObject
	{
		/// <summary>
		/// Watermark color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
		{
			get => GetValue(() => Extensions.ParseColorCode("rgba(0, 0, 0, 0.5)"));
			set => SetValue(value);
		}

		/// <summary>
		/// Text of the watermark. Word wrapping is not supported.
		/// </summary>
		[JsonPropertyName("text")]
		public string Text
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		/// <summary>
		/// Font size in pixels.
		/// </summary>
		[JsonPropertyName("fontSize")]
		public int FontSize
		{
			get => GetValue(() => 48);
			set => SetValue(value);
		}

		/// <summary>
		/// Line height in pixels.
		/// </summary>
		[JsonPropertyName("fontSize")]
		public int? LineHeight
		{
			get => GetValue(() => (int)(1.2 * FontSize));
			set => SetValue(value);
		}

		/// <summary>
		/// Font family.
		/// </summary>
		[JsonPropertyName("fontFamily")]
		public string FontFamily
		{
			get => GetValue(() => "-apple-system, BlinkMacSystemFont, 'Trebuchet MS', Roboto, Ubuntu, sans-serif");
			set => SetValue(value);
		}

		/// <summary>
		/// Font style.
		/// </summary>
		[JsonPropertyName("fontFamily")]
		public string FontStyle
		{
			get => GetValue<string>();
			set => SetValue(value);
		}
	}
}
