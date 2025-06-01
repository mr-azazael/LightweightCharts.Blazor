using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Series;
using LightweightCharts.Blazor.Utilities;
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Threading.Tasks;
using LightweightCharts.Blazor.Customization.Enums;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class MultiLineSeries
{
	bool _InitLineChartComponent;
	ChartComponent _LineChartComponent;
	ISeriesApi<long>[] _LineSeries;

	ChartComponent LineChartComponent
	{
		set
		{
			if (_LineChartComponent == value)
				return;

			_LineChartComponent = value;
			_InitLineChartComponent = true;
			StateHasChanged();
		}
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);

		if (_InitLineChartComponent)
		{
			_InitLineChartComponent = false;
			await InitializeLineChartComponent();
		}
	}

	IEnumerable<LineData<long>> GenerateLineData(int numberOfPoints, DateTime endDate)
	{
		var randomFactor = 25 + Random.Shared.NextDouble() * 25;
		double samplePoint(double i) =>
			 i * (0.5 +
						Math.Sin(i / 10) * 0.2 +
						Math.Sin(i / 20) * 0.4 +
						Math.Sin(i / randomFactor) * 0.8 +
						Math.Sin(i / 500) * 0.5
				  ) + 200;
		var startDate = endDate - TimeSpan.FromDays(numberOfPoints - 1);
		for (var i = 0; i < numberOfPoints; ++i)
		{
			var time = startDate + TimeSpan.FromDays(i);
			var value = samplePoint(i);
			var data = new LineData<long>
			{
				Time = time.ToUnix(),
				Value = value,
			};
			yield return data;
		}
	}

	async Task InitializeLineChartComponent()
	{
		if (_LineChartComponent == null)
			return;

		await _LineChartComponent.InitializationCompleted;
		_LineSeries = [
			await _LineChartComponent.AddSeries<LineStyleOptions>(SeriesType.Line, new() { Color = Color.Red }, 0),
			await _LineChartComponent.AddSeries<LineStyleOptions>(SeriesType.Line, new() { Color = Color.Blue }, 0),
			await _LineChartComponent.AddSeries<LineStyleOptions>(SeriesType.Line, new() { Color = Color.Green }, 1),
			await _LineChartComponent.AddSeries<LineStyleOptions>(SeriesType.Line, new() { Color = Color.Pink }, 1)];

		var numberOfPoints = 500;
		var endDate = new DateTime(2018, 1, 1, 12, 0, 0, 0);
		foreach (var series in _LineSeries)
			await series.SetData(GenerateLineData(numberOfPoints, endDate));

		var lineChartPriceScale = await _LineChartComponent.PriceScale(DefaultPriceScaleId.Right, 1);
		await lineChartPriceScale.SetAutoScale(false);

		var lineChartTimeScale = await _LineChartComponent.TimeScale();
		await lineChartTimeScale.FitContent();
	}
}
