using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using LightweightCharts.Blazor.Series;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Drawing;

namespace LightweightCharts.Blazor.Client.Pages
{
	partial class BarSeries
	{
		[Inject]
		IJSRuntime JsRuntime { get; set; }

		ChartComponent ChartComponent
		{
			set => InitializeChartComponent(value);
		}

		async void InitializeChartComponent(ChartComponent chart)
		{
			await chart.InitializationCompleted;

			var series = await chart.AddBarSeriesAsync(new Customization.Series.BarSeriesOptions
			{
				ThinBars = false
			});

			var timeScale = await chart.TimeScaleAsync();
			await JsRuntime.InvokeVoidAsync("JavascriptHelpers.setTimeScaleTickMarkFormatter", timeScale.JsObjectReference);

			Run(series, timeScale);
		}

		async void Run(ISeriesApi seriesApi, ITimeScaleApi timeScale)
		{
			var firstPoints = BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Take(30);

			while (true)
			{
				await seriesApi.SetData(firstPoints.Select(x => new BarData
				{
					Time = x.OpenTime,
					Open = x.OpenPrice,
					Close = x.ClosePrice,
					High = x.HighPrice,
					Low = x.LowPrice,
					Color = x.ClosePrice > x.OpenPrice ? Color.Green : Color.Red
				}));

				await timeScale.SetVisibleLogicalRange(new LogicalRange
				{
					To = 35
				});

				foreach (var point in BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Skip(30))
				{
					await seriesApi.Update(new BarData
					{
						Time = point.OpenTime,
						Open = point.OpenPrice,
						Close = point.ClosePrice,
						High = point.HighPrice,
						Low = point.LowPrice,
						Color = point.ClosePrice > point.OpenPrice ? Color.Green : Color.Red
					});
					await Task.Delay(1000);
				}
			}
		}
	}
}
