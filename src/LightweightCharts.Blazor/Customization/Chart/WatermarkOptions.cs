using LightweightCharts.Blazor.Converters;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Watermark options.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/WatermarkOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<WatermarkOptions>))]
	public class WatermarkOptions : JsonOptionsObject
	{
		/// <summary>
		/// Watermark color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
		{
			get => GetValue(() => Color.Transparent);
			set => SetValue(value);
		}

		/// <summary>
		/// Display the watermark.
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => GetValue<bool>();
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
		[JsonPropertyName("fontStyle")]
		public string FontStyle
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		/// <summary>
		/// Horizontal alignment inside the chart area.
		/// </summary>
		[JsonPropertyName("horzAlign")]
		public HorizontalAlignment HorizontalAlignment
		{
			get => GetValue(() => HorizontalAlignment.Center);
			set => SetValue(value);
		}

		/// <summary>
		/// Vertical alignment inside the chart area.
		/// </summary>
		[JsonPropertyName("vertAlign")]
		public VerticalAlignment VerticalAlignment
		{
			get => GetValue(() => VerticalAlignment.Center);
			set => SetValue(value);
		}
	}
}
