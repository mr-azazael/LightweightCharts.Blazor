using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;

namespace LightweightCharts.Blazor.Charts
{
	/// <summary>
	/// The main interface of a single chart using time for horizontal scale.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi"/>
	/// </summary>
	public interface IChartApi : IChartApiBase, ICustomizableObject<ChartOptionsBase>
	{
		
	}
}
