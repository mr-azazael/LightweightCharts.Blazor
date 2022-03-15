using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;

namespace LightweightCharts.Blazor.Client.Pages
{
	partial class AreaSeries
	{
		ChartComponent ChartComponent
		{
			set => InitializeChartComponent(value);
		}

		async void InitializeChartComponent(ChartComponent chart)
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
}
