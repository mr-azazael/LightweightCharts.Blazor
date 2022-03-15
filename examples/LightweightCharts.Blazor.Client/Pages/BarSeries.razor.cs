using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
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
			var series = await chart.AddBarSeriesAsync();
			await series.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new BarData
			{
				Time = x.OpenTime,
				Open = x.OpenPrice,
				Close = x.ClosePrice,
				High = x.HighPrice,
				Low = x.LowPrice,
				Color = Color.Purple
			})); ;
			var timeScale = await chart.TimeScaleAsync();
			await timeScale.SetVisibleLogicalRange(new LogicalRange
			{
				From = -2,
				To = BtcUsdDataPoints.OneWeek.Count() + 2
			});

			await JsRuntime.InvokeVoidAsync("JavascriptHelpers.setTimeScaleTickMarkFormatter", timeScale.JsObjectReference);
		}
	}
}
