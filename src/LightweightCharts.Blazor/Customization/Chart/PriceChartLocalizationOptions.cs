using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Extends LocalizationOptions for price-based charts. Includes settings specific to formatting price values on the horizontal scale.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceChartLocalizationOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<PriceChartLocalizationOptions>))]
	public class PriceChartLocalizationOptions : LocalizationOptions
	{
		/// <summary>
		/// The number of decimal places to display for price values on the horizontal scale.
		/// </summary>
		[JsonPropertyName("precision")]
		public int Precision
		{
			get => GetValue<int>();
			set => SetValue(value);
		}
	}
}
