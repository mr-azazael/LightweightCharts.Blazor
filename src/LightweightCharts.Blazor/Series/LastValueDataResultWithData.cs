using LightweightCharts.Blazor.Converters;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Series;

/// <summary>
/// Represents last value data result of a series for plugins when there is data.<br/>
/// <see href="https://tradingview.github.io/lightweight-charts/docs/5.0/api/interfaces/LastValueDataResultWithData"/>
/// </summary>
public class LastValueDataResultWithData : LastValueDataResultWithoutData
{
	/// <summary>
	/// The last price of the series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/5.0/api/interfaces/LastValueDataResultWithData#price"/>
	/// </summary>
	[JsonPropertyName("price")]
	public double Price { get; set; }

	/// <summary>
	/// The color of the last value.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/5.0/api/interfaces/LastValueDataResultWithData#color"/>
	/// </summary>
	[JsonPropertyName("color")]
	[JsonConverter(typeof(JsonColorConverter))]
	public Color Color { get; set; }
}
