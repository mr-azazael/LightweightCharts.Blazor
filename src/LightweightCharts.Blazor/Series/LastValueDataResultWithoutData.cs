using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Series;

/// <summary>
/// Represents last value data result of a series for plugins when there is no data.<br/>
/// <see href="https://tradingview.github.io/lightweight-charts/docs/5.0/api/interfaces/LastValueDataResultWithoutData"/>
/// </summary>
public class LastValueDataResultWithoutData
{
	/// <summary>
	/// Indicates if the series has data.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/5.0/api/interfaces/LastValueDataResultWithoutData#nodata"/>
	/// </summary>
	[JsonPropertyName("noData")]
	public bool NoData { get; set; } = true;
}
