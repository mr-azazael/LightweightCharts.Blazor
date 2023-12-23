using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class AreaSeries
{
	ChartComponent ChartComponent
	{
		set => _ = InitializeChartComponent(value);
	}

	async Task InitializeChartComponent(ChartComponent chart)
	{
		await chart.InitializationCompleted;

		var series = await chart.AddAreaSeriesAsync();
		await series.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new SingleValueData
		{
			Time = x.OpenTime,
			Value = x.ClosePrice
		}));

		var timeScale = await chart.TimeScaleAsync();
		await timeScale.SetVisibleLogicalRange(new LogicalRange
		{
			From = -2,
			To = BtcUsdDataPoints.OneWeek.Count() + 2
		});
	}
}
