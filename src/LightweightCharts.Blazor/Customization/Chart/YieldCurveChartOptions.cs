using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Extended chart options that include yield curve specific options. This interface combines the standard chart options with yield curve options.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/YieldCurveChartOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<YieldCurveChartOptions>))]
	public class YieldCurveChartOptions : ChartOptionsBase<LocalizationOptions>
	{
		/// <summary>
		/// Yield curve specific options. This object contains all the settings related to how the yield curve is displayed and behaves.
		/// </summary>
		[JsonPropertyName("yieldCurve")]
		public YieldCurveOptions YieldCurve
		{
			get => GetValue(() => new YieldCurveOptions());
			set => SetValue(value);
		}
	}
}
