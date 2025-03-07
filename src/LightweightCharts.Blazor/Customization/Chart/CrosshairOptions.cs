using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure describing crosshair options.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<CrosshairOptions>))]
	public class CrosshairOptions : JsonOptionsObject
	{
		/// <summary>
		/// Crosshair mode.
		/// </summary>
		[JsonPropertyName("mode")]
		public CrosshairMode Mode
		{
			get => GetValue(() => CrosshairMode.Magnet);
			set => SetValue(value);
		}

		/// <summary>
		/// Vertical line options.
		/// </summary>
		[JsonPropertyName("vertLine")]
		public CrosshairLineOption VerticalLine
		{
			get => GetValue(() => new CrosshairLineOption());
			set => SetValue(value);
		}

		/// <summary>
		/// Horizontal line options.
		/// </summary>
		[JsonPropertyName("horzLine")]
		public CrosshairLineOption HorizontalLine
		{
			get => GetValue(() => new CrosshairLineOption());
			set => SetValue(value);
		}
	}
}
