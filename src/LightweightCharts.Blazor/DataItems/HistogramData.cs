using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	/// Structure describing a single item of data for histogram series.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/HistogramData
	/// </summary>
	public class HistogramData : SingleValueData
	{
		/// <summary>
		/// Optional color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color { get; set; } = Color.LightBlue;
	}
}
