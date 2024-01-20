using LightweightCharts.Blazor.Converters;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	/// Structure describing a single item of data for baseline series.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/BaselineData
	/// </summary>
	public class BaselineData : SingleValueData
	{
		/// <summary>
		/// Optional color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("topFillColor1")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? TopFillColor1 { get; set; }

		/// <summary>
		/// Optional top area bottom fill color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("topFillColor2")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? TopFillColor2 { get; set; }

		/// <summary>
		/// Optional top area line color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("topLineColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? TopLineColor { get; set; }

		/// <summary>
		/// Optional bottom area top fill color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("bottomFillColor1")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? BottomFillColor1 { get; set; }

		/// <summary>
		/// Optional bottom area bottom fill color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("bottomFillColor2")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? BottomFillColor2 { get; set; }

		/// <summary>
		/// Optional bottom area line color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("bottomLineColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? BottomLineColor { get; set; }
	}
}
