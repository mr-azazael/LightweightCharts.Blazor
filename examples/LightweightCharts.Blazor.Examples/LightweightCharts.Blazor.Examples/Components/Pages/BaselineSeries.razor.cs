using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class BaselineSeries
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
		await chart.ApplyOptions(new Customization.Chart.ChartOptions
		{
			LeftPriceScale = new Customization.Chart.PriceScaleOptions
			{
				Visible = false
			},
			RightPriceScale = new Customization.Chart.PriceScaleOptions
			{
				ScaleMargins = new Customization.PriceScaleMargins
				{
					Bottom = 0.1d,
					Top = 0.1d
				}
			}
		});

		var series = await chart.AddBaselineSeriesAsync(new BaselineStyleOptions
		{
			BaseValue = new BaseValuePrice { Price = BtcUsdDataPoints.OneWeek.Average(x => x.ClosePrice) }
		});
		await series.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new SingleValueData
		{
			Time = x.OpenTime,
			Value = x.ClosePrice
		}));

		await JsRuntime.InvokeVoidAsync("javascriptHelpers.setAutoscaleInfoProvider", series.JsObjectReference);

		var timeScale = await chart.TimeScaleAsync();
		await timeScale.SetVisibleLogicalRange(new LogicalRange
		{
			From = -2,
			To = BtcUsdDataPoints.OneWeek.Count() + 2
		});
	}
}
