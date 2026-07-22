using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class Conflation
{
	bool _Initialize;
	ChartComponent _ChartComponent;
	bool _Enabled;
	LineData<long>[] _data;
	ITimeScaleApi<long> _Timescale;

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

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);

		if (_Initialize)
		{
			_Initialize = false;
			await InitializeChartComponent();
		}
	}

	async Task InitializeChartComponent()
	{
		if (_ChartComponent == null)
			return;

		var start = new DateTime(2000, 1, 1);
		_data = Enumerable.Range(0, 500000).Select(x => new LineData<long>
		{
			Time = x * 100,
			Value = Random.Shared.NextDouble() * 100
		}).ToArray();

		_Timescale = await _ChartComponent.TimeScale();
		var series = await _ChartComponent.AddSeries<LineStyleOptions>(Customization.Enums.SeriesType.Line);
		await series.SetData(_data);
	}

	async Task ToggleDataConflation()
	{
		_Enabled = !_Enabled;
		await _Timescale.ApplyOptions(new Customization.Chart.TimeScaleOptions
		{
			EnableConflation = _Enabled,
			ConflationThresholdFactor = 8,
			PrecomputeConflationPriority = Customization.Enums.PrecomputeConflationPriority.UserBlocking
		});
	}
}
