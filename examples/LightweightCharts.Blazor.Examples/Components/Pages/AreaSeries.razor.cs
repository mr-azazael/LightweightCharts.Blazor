using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class AreaSeries
{
	bool _Initialize;
	ChartComponent _ChartComponent;

	ChartComponent ChartComponent
	{
		set
		{
			if (_ChartComponent == value)
				return;

			_ChartComponent = value;
			_Initialize = true;
			StateHasChanged();
		}
	}

	async Task InitializeChartComponent()
	{
		if (_ChartComponent == null)
			return;

		await _ChartComponent.InitializationCompleted;
		await _ChartComponent.ApplyOptions(new Customization.Chart.ChartOptionsBase
		{
			AutoSize = true
		});

		var series = await _ChartComponent.AddAreaSeriesAsync();
		await series.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new AreaData
		{
			Time = x.OpenTime,
			Value = x.ClosePrice
		}));

		var timeScale = await _ChartComponent.TimeScaleAsync();
		await timeScale.ApplyOptions(new Customization.Chart.TimeScaleOptions
		{
			AllowBoldLabels = false
		});
		await timeScale.SetVisibleLogicalRange(new LogicalRange
		{
			From = -2,
			To = BtcUsdDataPoints.OneWeek.Count() + 2
		});
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);

		if (_Initialize)
		{
			_Initialize = false;
			await InitializeChartComponent();
		}
	}
}
