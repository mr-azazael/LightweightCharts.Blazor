using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using System.Drawing;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class OptionsChart
{
	bool _OptionsInit;
	OptionsChartComponent _Options;
	OptionsChartComponent Options
	{
		set
		{
			if (_Options == value)
				return;

			_Options = value;
			StateHasChanged();
		}
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);

		if (!_OptionsInit)
		{
			_OptionsInit = true;
			await InitializeOptions();
		}
	}

	async Task InitializeOptions()
	{
		if (_Options == null)
			return;

		await _Options.InitializationCompleted;
		var leftPriceScale = await _Options.PriceScale("left");
		await leftPriceScale.ApplyOptions(new Customization.Chart.PriceScaleOptions
		{
			Visible = true
		});
		var rightPriceScale = await _Options.PriceScale("right");
		await rightPriceScale.ApplyOptions(new Customization.Chart.PriceScaleOptions
		{
			Visible = false
		});

		var series = await _Options.AddSeries(SeriesType.Baseline, new BaselineStyleOptions
		{
			PriceScaleId = "left",
			TopFillColor1 = Color.FromArgb(100, 38, 166, 154),
			TopFillColor2 = Color.FromArgb(100, 38, 166, 154),
			BottomFillColor1 = Color.FromArgb(100, 239, 83, 80),
			BottomFillColor2 = Color.FromArgb(100, 239, 83, 80)
		});
		await series.SetData([
			new BaselineData<double>
			{
				Time = 150,
				Value = -4000
			},
			new BaselineData<double>
			{
				Time = 360,
				Value = -4000
			},
			new BaselineData<double>
			{
				Time = 750,
				Value = 30000
			}]);

		var timescale = await _Options.TimeScale();
		await timescale.FitContent();
	}
}
