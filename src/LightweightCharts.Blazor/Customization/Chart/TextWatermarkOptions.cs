using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/TextWatermarkOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<TextWatermarkOptions>))]
	public class TextWatermarkOptions : JsonOptionsObject
	{
		/// <summary>
		/// Display the watermark.
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => GetValue(() => true);
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

		/// <summary>
		/// Text to be displayed within the watermark. Each item in the array is treated as new line.
		/// </summary>
		[JsonPropertyName("lines")]
		public TextWatermarkLineOptions[] Lines
		{
			get => GetValue(System.Array.Empty<TextWatermarkLineOptions>);
			set => SetValue(value);
		}
	}
}
