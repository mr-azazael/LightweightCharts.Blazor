using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models
{
	/// <summary>
	/// Dimensions of the Chart Pane (the main chart area which excludes the time and price scales).<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PaneSize
	/// </summary>
	public class PaneSize
	{
		/// <summary>
		/// Height of the Chart Pane (pixels)
		/// </summary>
		[JsonPropertyName("height")]
		public int Height { get; set; }

		/// <summary>
		/// Width of the Chart Pane (pixels)
		/// </summary>
		[JsonPropertyName("width")]
		public int Width { get; set; }
	}
}
