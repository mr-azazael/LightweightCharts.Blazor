using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	///	Represents a whitespace data item, which is a data point without a value.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/WhitespaceData"/>
	/// </summary>
	public class WhitespaceData<H> : ISeriesData<H>
		where H : struct
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		[JsonPropertyName("time")]
		public H Time { get; set; }
	}
}
