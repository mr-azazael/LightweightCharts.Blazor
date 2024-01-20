using LightweightCharts.Blazor.Converters;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	/// Structure describing a single item of data for candlestick series.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CandlestickData
	/// </summary>
	public class CandlestickData : OhlcData
	{
		/// <summary>
		/// Optional color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? Color { get; set; }

		/// <summary>
		/// Optional border color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("borderColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? BorderColor { get; set; }

		/// <summary>
		/// Optional wick color value for certain data item. If missed, color from options is used.
		/// </summary>
		[JsonPropertyName("wickColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? WickColor { get; set; }
	}
}
