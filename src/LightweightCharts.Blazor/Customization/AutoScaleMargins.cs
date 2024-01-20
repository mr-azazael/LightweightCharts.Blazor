using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization
{
	/// <summary>
	/// Represents the margin used when updating a price scale.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/AutoScaleMargins
	/// </summary>
	public class AutoScaleMargins : BaseModel
	{
		/// <summary>
		/// The number of pixels for bottom margin.
		/// </summary>
		[JsonPropertyName("below")]
		public double Below { get; set; }

		/// <summary>
		/// The number of pixels for top margin.
		/// </summary>
		[JsonPropertyName("above")]
		public double Above { get; set; }
	}
}
