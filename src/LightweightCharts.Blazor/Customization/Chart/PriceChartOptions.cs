using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Configuration options specific to price-based charts. Extends the base chart options and includes localization settings for price formatting.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceChartOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<PriceChartOptions>))]
	public class PriceChartOptions : ChartOptionsBase<PriceChartLocalizationOptions>
	{
		
	}
}
