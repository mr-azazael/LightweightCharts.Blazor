using LightweightCharts.Blazor.Customization.Enums;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	/// Represents a marker drawn above or below a data point to indicate a price change update.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/SeriesUpDownMarker"/>
	/// </summary>
	public class SeriesUpDownMarker<H> : WhitespaceData<H>
		where H : struct
	{
		/// <summary>
		/// The price value for the data point.
		/// </summary>
		[JsonPropertyName("value")]
		public double Value { get; set; }

		/// <summary>
		/// The direction of the price change.
		/// </summary>
		[JsonPropertyName("sign")]
		public MarkerSign Sign { get; set; }
	}
}
