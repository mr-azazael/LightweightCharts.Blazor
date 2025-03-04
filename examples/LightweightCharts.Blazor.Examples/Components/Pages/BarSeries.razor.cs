using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using LightweightCharts.Blazor.Series;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class BarSeries
{
	bool _InitChartComponent;
	ChartComponent _ChartComponent;

	[Inject]
	IJSRuntime JsRuntime { get; set; }

	ChartComponent ChartComponent
	{
		set
		{
			if (_ChartComponent == value)
				return;

			_ChartComponent = value;
			_InitChartComponent = true;
			StateHasChanged();
		}
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);

		if (_InitChartComponent)
		{
			_InitChartComponent = false;
			await InitializeChartComponent();
		}
	}

	async Task InitializeChartComponent()
	{
		if (_ChartComponent == null)
			return;

		await _ChartComponent.InitializationCompleted;
		await _ChartComponent.ApplyOptions(new Customization.Chart.ChartOptionsBase
		{
			Localization = new Customization.Chart.LocalizationOptions
			{
				TimeFormatter = new Customization.JsDelegate("javascriptHelpers.customTimeFormatter")
			}
		});
		var series = await _ChartComponent.AddBarSeriesAsync(new Customization.Series.BarStyleOptions
		{
			ThinBars = false
		});

		var timeScale = await _ChartComponent.TimeScaleAsync();
		await timeScale.ApplyOptions(new Customization.Chart.TimeScaleOptions
		{
			TimeVisible = true,
			TickMarkFormatter = new Customization.JsDelegate("customTimeScaleTickMarkFormatter")
		});

		_ = Run(series, timeScale);
	}

	async Task Run(ISeriesApi seriesApi, ITimeScaleApi timeScale)
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
