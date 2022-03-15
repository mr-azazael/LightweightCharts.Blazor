using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;

namespace LightweightCharts.Blazor.Client.Pages
{
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
			_LineChart.VisibleLogicalRangeChanged += (s, e) =>
			{
				_AreaChart.SetVisibleLogicalRange(e);
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
			_AreaChart.VisibleLogicalRangeChanged += (s, e) =>
			{
				_LineChart.SetVisibleLogicalRange(e);
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
}
