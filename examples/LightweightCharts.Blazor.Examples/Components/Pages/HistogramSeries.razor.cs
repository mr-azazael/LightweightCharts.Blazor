using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class HistogramSeries
{
	bool _InitChartComponent;
	ChartComponent _ChartComponent;

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
		var series = await _ChartComponent.AddHistogramSeriesAsync();
		await series.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new HistogramData
		{
			Time = x.OpenTime,
			Value = x.Volume,
			Color = x.OpenPrice > x.ClosePrice ? Color.Green : Color.Red
		}));
		var timeScale = await _ChartComponent.TimeScaleAsync();
		await timeScale.SetVisibleLogicalRange(new LogicalRange
		{
			From = -2,
			To = BtcUsdDataPoints.OneWeek.Count() + 2
		});
	}
}
