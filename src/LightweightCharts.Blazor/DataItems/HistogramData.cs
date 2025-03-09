using LightweightCharts.Blazor.Converters;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	/// Structure describing a single item of data for histogram series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/HistogramData"/>
	/// </summary>
	public class HistogramData<H> : SingleValueData<H>
		where H : struct
	{
		/// <summary>
		/// Optional color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? Color { get; set; }
	}
}
