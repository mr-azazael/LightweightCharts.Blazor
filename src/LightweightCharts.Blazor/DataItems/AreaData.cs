using LightweightCharts.Blazor.Converters;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	/// Structure describing a single item of data for area series<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/AreaData
	/// </summary>
	public class AreaData : SingleValueData
	{
		/// <summary>
		/// Optional line color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("lineColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? LineColor { get; set; }

		/// <summary>
		/// Optional top color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("topColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? TopColor { get; set; }

		/// <summary>
		/// Optional bottom color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("bottomColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? BottomColor { get; set; }
	}
}
