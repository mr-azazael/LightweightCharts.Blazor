using LightweightCharts.Blazor.Models;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization
{
	/// <summary>
	/// Represents information used to update a price scale.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/AutoscaleInfo
	/// </summary>
	public class AutoscaleInfo : BaseModel
	{
		/// <summary>
		/// Price range.
		/// </summary>
		[JsonPropertyName("priceRange")]
		public PriceRange PriceRange { get; set; }

		/// <summary>
		/// Scale margins.
		/// </summary>
		[JsonPropertyName("margins")]
		public AutoScaleMargins Margins { get; set; }
	}
}
