using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Series;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class LineSeries
{
	bool _InitLineChartComponent;
	ChartComponent _LineChartComponent;
	bool _InitAreaChartComponent;
	ChartComponent _AreaChartComponent;

	ITimeScaleApi _LineChartTimescale;
	ISeriesApi _LineSeries;
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

	ITimeScaleApi _AreaChartTimescale;
	ISeriesApi _AreaSeries;
	ChartComponent AreaChartComponent
	{
		set
		{
			if (_AreaChartComponent == value)
				return;

			_AreaChartComponent = value;
			_InitAreaChartComponent = true;
			StateHasChanged();
		}
	}

	bool _InLineChartCallback;
	bool _InAreaChartCallback;

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);

		if (_InitLineChartComponent)
		{
			_InitLineChartComponent = false;
			await InitializeLineChartComponent();
		}

		if (_InitAreaChartComponent)
		{
			_InitAreaChartComponent = false;
			await InitializeAreaChartComponent();
		}
	}

	async Task InitializeLineChartComponent()
	{
		if (_LineChartComponent == null)
			return;

		await _LineChartComponent.InitializationCompleted;
		await _LineChartComponent.ApplyOptions(new Customization.Chart.ChartOptions
		{
			LeftPriceScale = new Customization.Chart.PriceScaleOptions
			{
				Visible = false
			}
		});

		_LineSeries = await _LineChartComponent.AddSeries(Customization.Enums.SeriesType.Line);
		await _LineSeries.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new LineData
		{
			Time = x.OpenTime,
			Value = x.ClosePrice
		}));

		_LineChartTimescale = await _LineChartComponent.TimeScaleAsync();
		_LineChartTimescale.VisibleLogicalRangeChanged += async (s, e) =>
		{
			if (_InLineChartCallback)
				return;

			_InLineChartCallback = true;
			await _AreaChartTimescale.SetVisibleLogicalRange(e);
			_InLineChartCallback = false;
		};
		await _LineChartTimescale.ApplyOptions(new Customization.Chart.TimeScaleOptions
		{
			Visible = false
		});

		_LineChartComponent.CrosshairMoved += (s, e) =>
		{
			if (e.SeriesPrices.Length > 0)
			{
				var data = e.SeriesPrices[0].DataItem as SingleValueData;
				_AreaChartComponent?.SetCrosshairPosition(data.Value, data.UnixTime, _AreaSeries);
			}
			else
			{
				_AreaChartComponent?.ClearCrosshairPosition();
			}
		};

		await _LineChartTimescale.FitContent();
		//await SyncTimelines();
	}

	async Task InitializeAreaChartComponent()
	{
		if (_AreaChartComponent == null)
			return;

		await _AreaChartComponent.InitializationCompleted;
		await _AreaChartComponent.ApplyOptions(new Customization.Chart.ChartOptions
		{
			LeftPriceScale = new Customization.Chart.PriceScaleOptions
			{
				Visible = false
			}
		});

		_AreaSeries = await _AreaChartComponent.AddSeries(Customization.Enums.SeriesType.Area);
		await _AreaSeries.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new AreaData
		{
			Time = x.OpenTime,
			Value = x.ClosePrice
		}));

		_AreaChartTimescale = await _AreaChartComponent.TimeScaleAsync();
		_AreaChartTimescale.VisibleLogicalRangeChanged += async (s, e) =>
		{
			if (_InAreaChartCallback)
				return;

			_InAreaChartCallback = true;
			await _LineChartTimescale.SetVisibleLogicalRange(e);
			_InAreaChartCallback = false;
		};

		_AreaChartComponent.CrosshairMoved += (s, e) =>
		{
			if (e.SeriesPrices.Length > 0)
			{
				var data = e.SeriesPrices[0].DataItem as SingleValueData;
				_LineChartComponent?.SetCrosshairPosition(data.Value, data.UnixTime, _LineSeries);
			}
			else
			{
				_LineChartComponent?.ClearCrosshairPosition();
			}
		};

		await _AreaChartTimescale.FitContent();
		//await SyncTimelines();
	}

	//async Task SyncTimelines()
	//{
	//	if (_LineChartTimescale == null || _AreaChartTimescale == null)
	//		return;

	//	await _LineChartTimescale.SetVisibleLogicalRange(new LogicalRange
	//	{
	//		From = -2,
	//		To = BtcUsdDataPoints.OneWeek.Count() + 2
	//	});
	//}
}
