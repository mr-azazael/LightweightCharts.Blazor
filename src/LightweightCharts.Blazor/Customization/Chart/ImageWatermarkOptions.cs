using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ImageWatermarkOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<ImageWatermarkOptions>))]
	public class ImageWatermarkOptions : JsonOptionsObject
	{
		/// <summary>
		/// Maximum width for the image watermark.
		/// </summary>
		[JsonPropertyName("maxWidth")]
		public int? MaxWidth
		{
			get => GetValue<int?>();
			set => SetValue(value);
		}

		/// <summary>
		/// Maximum height for the image watermark.
		/// </summary>
		[JsonPropertyName("maxHeight")]
		public int? MaxHeight
		{
			get => GetValue<int?>();
			set => SetValue(value);
		}

		/// <summary>
		/// Padding to maintain around the image watermark relative to the chart pane edges.
		/// </summary>
		[JsonPropertyName("padding")]
		public int Padding
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		/// <summary>
		/// The alpha (opacity) for the image watermark. Where 1 is fully opaque (visible) and 0 is fully transparent.
		/// </summary>
		[JsonPropertyName("alpha")]
		public float Alpha
		{
			get => GetValue(() => 1);
			set => SetValue(value);
		}
	}
}
