using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models
{
	/// <summary>
	/// Represents a type of priced base value of baseline series type.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/BaseValuePrice"/>
	/// </summary>
	public class BaseValuePrice
	{
		/// <summary>
		/// Distinguished type value.
		/// </summary>
		[JsonPropertyName("type")]
		public string Type { get; set; } = "price";

		/// <summary>
		/// Price value.
		/// </summary>
		[JsonPropertyName("price")]
		public double Price { get; set; }
	}
}
