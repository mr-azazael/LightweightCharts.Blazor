using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using LightweightCharts.Blazor.Utilities;
using System;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class AreaSeries : IDisposable
{
	bool _Initialize;
	ChartComponent _ChartComponent;
	bool _Disposed;

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
		await _ChartComponent.ApplyOptions(new ChartOptions
		{
			AutoSize = true
		});

		var series = await _ChartComponent.AddSeries<AreaStyleOptions>(SeriesType.Area);
		var pane = await series.GetPane();
		var textMark = pane.CreateTextWatermark(new TextWatermarkOptions
		{
			HorizontalAlignment = HorizontalAlignment.Left,
			VerticalAlignment = VerticalAlignment.Center,
			Lines = [new TextWatermarkLineOptions { Color = Color.LightBlue, Text = "Text watermark example" }]
		});

		var timeScale = await _ChartComponent.TimeScale();
		var upDown = await series.CreateUpDownMarkers(new UpDownMarkersPluginOptions
		{
			PositiveColor = Color.Blue,
			NegativeColor = Color.Red,
			UpdateVisibilityDuration = 1500
		});
		await upDown.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new AreaData<long>
		{
			Time = x.OpenTime.ToUnix(),
			Value = x.ClosePrice
		}));
		await timeScale.ApplyOptions(new TimeScaleOptions
		{
			AllowBoldLabels = false
		});
		await timeScale.SetVisibleLogicalRange(new LogicalRange
		{
			From = -2,
			To = BtcUsdDataPoints.OneWeek.Count() + 2
		});

		_ = Task.Run(async () =>
		{
			while (_Disposed == false)
			{
				await Task.Delay(2000);
				if (_Disposed)
					return;

				var index = Random.Shared.Next(BtcUsdDataPoints.OneWeek.Count());
				var point = BtcUsdDataPoints.OneWeek.ElementAt(index);
				var data = new AreaData<long>
				{
					Time = point.OpenTime.ToUnix(),
					Value = (100 + (20 * Math.Max(Random.Shared.NextDouble(), 0.25) - 10)) * point.ClosePrice / 100.0
				};
				await InvokeAsync(() => upDown.Update(data, true));
			}
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

	public void Dispose()
	{
		_Disposed = true;
	}
}
