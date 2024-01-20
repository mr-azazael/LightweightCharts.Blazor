using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class BaselineSeries
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

		var series = await _ChartComponent.AddBaselineSeriesAsync(new BaselineStyleOptions
		{
			BaseValue = new BaseValuePrice { Price = BtcUsdDataPoints.OneWeek.Average(x => x.ClosePrice) }
		});
		await series.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new BaselineData
		{
			Time = x.OpenTime,
			Value = x.ClosePrice
		}));

		await JsRuntime.InvokeVoidAsync("javascriptHelpers.setAutoscaleInfoProvider", series.JsObjectReference);

		var timeScale = await _ChartComponent.TimeScaleAsync();
		await timeScale.SetVisibleLogicalRange(new LogicalRange
		{
			From = -2,
			To = BtcUsdDataPoints.OneWeek.Count() + 2
		});
	}
}
