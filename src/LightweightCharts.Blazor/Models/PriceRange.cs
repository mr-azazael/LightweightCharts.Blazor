using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models
{
	/// <summary>
	/// Represents a price range.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceRange
	/// </summary>
	public class PriceRange
	{
		/// <summary>
		/// Minimum value in the range.
		/// </summary>
		[JsonPropertyName("minValue")]
		public bool MinValue { get; set; }

		/// <summary>
		/// Maximum value in the range.
		/// </summary>
		[JsonPropertyName("maxValue")]
		public bool MaxValue { get; set; }
	}
}
