using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Examples;
using LightweightCharts.Blazor.Models;
using System.Drawing;
using System.Linq;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class HistogramSeries
{
	ChartComponent ChartComponent
	{
		set => InitializeChartComponent(value);
	}

	async void InitializeChartComponent(ChartComponent chart)
	{
		await chart.InitializationCompleted;
		var series = await chart.AddHistogramSeriesAsync();
		await series.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new HistogramData
		{
			Time = x.OpenTime,
			Value = x.Volume,
			Color = x.OpenPrice > x.ClosePrice ? Color.Green : Color.Red
		}));
		var timeScale = await chart.TimeScaleAsync();
		await timeScale.SetVisibleLogicalRange(new LogicalRange
		{
			From = -2,
			To = BtcUsdDataPoints.OneWeek.Count() + 2
		});
	}
}
