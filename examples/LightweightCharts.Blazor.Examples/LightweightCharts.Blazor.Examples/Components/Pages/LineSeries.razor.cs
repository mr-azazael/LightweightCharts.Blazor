using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using System.Linq;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class LineSeries
{
	ITimeScaleApi _LineChart;
	ChartComponent LineChartComponent
	{
		set => InitializeLineChartComponent(value);
	}

	ITimeScaleApi _AreaChart;
	ChartComponent AreaChartComponent
	{
		set => InitializeLineAreaComponent(value);
	}

	bool _InLineChartCallback;
	bool _InAreaChartCallback;

	async void InitializeLineChartComponent(ChartComponent chart)
	{
		await chart.InitializationCompleted;
		await chart.ApplyOptions(new Customization.Chart.ChartOptions
		{
			LeftPriceScale = new Customization.Chart.PriceScaleOptions
			{
				Visible = false
			}
		});

		var series = await chart.AddLineSeriesAsync();
		await series.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new SingleValueData
		{
			Time = x.OpenTime,
			Value = x.ClosePrice
		}));

		_LineChart = await chart.TimeScaleAsync();
		_LineChart.VisibleLogicalRangeChanged += async (s, e) =>
		{
			if (_InLineChartCallback)
				return;

			_InLineChartCallback = true;
			await _AreaChart.SetVisibleLogicalRange(e);
			_InLineChartCallback = false;
		};
		await _LineChart.ApplyOptions(new Customization.Chart.TimeScaleOptions
		{
			Visible = false
		});

		SyncTimelines();
	}

	async void InitializeLineAreaComponent(ChartComponent chart)
	{
		await chart.InitializationCompleted;
		await chart.ApplyOptions(new Customization.Chart.ChartOptions
		{
			LeftPriceScale = new Customization.Chart.PriceScaleOptions
			{
				Visible = false
			}
		});

		var series = await chart.AddAreaSeriesAsync();
		await series.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new SingleValueData
		{
			Time = x.OpenTime,
			Value = x.ClosePrice
		}));

		_AreaChart = await chart.TimeScaleAsync();
		_AreaChart.VisibleLogicalRangeChanged += async (s, e) =>
		{
			if (_InAreaChartCallback)
				return;

			_InAreaChartCallback = true;
			await _LineChart.SetVisibleLogicalRange(e);
			_InAreaChartCallback = false;
		};
		SyncTimelines();
	}

	async void SyncTimelines()
	{
		if (_LineChart == null || _AreaChart == null)
			return;

		await _LineChart.SetVisibleLogicalRange(new LogicalRange
		{
			From = -2,
			To = BtcUsdDataPoints.OneWeek.Count() + 2
		});
	}
}
