using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	/// Represents a bar with a Time and open, high, low, and close prices.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/OhlcData"/>
	/// </summary>
	public class OhlcData<H> : WhitespaceData<H>
		where H : struct
	{
		/// <summary>
		/// The open price.
		/// </summary>
		[JsonPropertyName("open")]
		public double Open { get; set; }

		/// <summary>
		/// The high price.
		/// </summary>
		[JsonPropertyName("high")]
		public double High { get; set; }

		/// <summary>
		/// The low price.
		/// </summary>
		[JsonPropertyName("low")]
		public double Low { get; set; }

		/// <summary>
		/// The close price.
		/// </summary>
		[JsonPropertyName("close")]
		public double Close { get; set; }
	}
}
