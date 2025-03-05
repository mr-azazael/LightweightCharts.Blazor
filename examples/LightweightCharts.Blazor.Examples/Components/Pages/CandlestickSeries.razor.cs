using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models.Events;
using LightweightCharts.Blazor.Series;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Examples.Components.Pages;

partial class CandlestickSeries
{
	ISeriesApi<CandlestickStyleOptions> _Candlestick;
	ISeriesApi<HistogramStyleOptions> _Histogram;
	IEnumerable<SeriesPrice> _MouseHoverPrices;
	string _LastClickedId;
	bool _InitChartComponent;
	ChartComponent _ChartComponent;
	ChartOptions _Options;

	ChartComponent ChartComponent
	{
		get => _ChartComponent;
		set
		{
			if (_ChartComponent == value)
				return;

			_ChartComponent = value;
			_InitChartComponent = true;
			StateHasChanged();
		}
	}

	string _BackgroundType;
	string BackgroundType
	{
		get => _BackgroundType;
		set
		{
			_BackgroundType = value;
			_Options.Layout.Background = _BackgroundType switch
			{
				"Solid" => new SolidColor(),
				_ => new VerticalGradientColor()
			};
		}
	}

	protected override async Task OnAfterRenderAsync(bool firstRender)
	{
		await base.OnAfterRenderAsync(firstRender);

		if(_InitChartComponent)
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

		//clicking on markers seem to return the candlestick series
		//no hovered series is sent when clicking on the actual series items...
		_ChartComponent.Clicked += OnChartClicked;
		_ChartComponent.CrosshairMoved += OnChartCrosshairMoved;

		await _ChartComponent.ApplyOptions(new ChartOptions
		{
			Crosshair = new CrosshairOptions
			{
				Mode = CrosshairMode.Normal
			},
			LeftPriceScale = new PriceScaleOptions
			{
				Visible = false
			},
			RightPriceScale = new PriceScaleOptions
			{
				ScaleMargins = new Customization.PriceScaleMargins
				{
					Bottom = 0.1d,
					Top = 0
				}
			},
			OverlayPriceScale = new BasePriceScaleOptions
			{
				ScaleMargins = new Customization.PriceScaleMargins
				{
					Bottom = 0,
					Top = 0.9d
				}
			}
		});

		_Options = await _ChartComponent.Options();
		BackgroundType = "Solid";
		StateHasChanged();

		await SetupCandlestickSeries();
		await SetupHistogramSeries();

		var timeScale = await _ChartComponent.TimeScaleAsync();
		await timeScale.FitContent();
	}

	async Task SetupCandlestickSeries()
	{
		_Candlestick = await _ChartComponent.AddCandlestickSeriesAsync();
		await _Candlestick.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new CandlestickData
		{
			Time = x.OpenTime,
			Open = x.OpenPrice,
			Close = x.ClosePrice,
			High = x.HighPrice,
			Low = x.LowPrice
		}));

		//markers
		var minLowPoint = BtcUsdDataPoints.OneWeek.OrderBy(x => x.LowPrice).First();
		var maxHighPoint = BtcUsdDataPoints.OneWeek.OrderByDescending(x => x.HighPrice).First();
		var minClose = BtcUsdDataPoints.OneWeek.OrderBy(x => x.ClosePrice).First();
		var maxClose = BtcUsdDataPoints.OneWeek.OrderByDescending(x => x.ClosePrice).First();

		await _Candlestick.SetMarkers(new SeriesMarker[]
		{
			new SeriesMarker
			{
				Time = minLowPoint.OpenTime,
				Position = SeriesMarkerPosition.BelowBar,
				Shape = SeriesMarkerShape.Circle,
				Color = Color.Purple,
				Size = 1,
				Text = "Minimum low",
				Id = "min_low"
			},
			new SeriesMarker
			{
				Time = maxHighPoint.OpenTime,
				Position = SeriesMarkerPosition.AboveBar,
				Shape = SeriesMarkerShape.Circle,
				Color = Color.Purple,
				Size = 1,
				Text = "Maximum high",
				Id = "max_high"
			},
			new SeriesMarker
			{
				Time = minClose.OpenTime,
				Position = SeriesMarkerPosition.BelowBar,
				Shape = SeriesMarkerShape.ArrowUp,
				Color = Color.Red,
				Size = 3,
				Text = "Minimum close",
				Id = "min_close"
			},
			new SeriesMarker
			{
				Time = maxClose.OpenTime,
				Position = SeriesMarkerPosition.AboveBar,
				Shape = SeriesMarkerShape.ArrowDown,
				Color = Color.Red,
				Size = 3,
				Text = "Maximum close",
				Id = "max_close"
			}
		}.OrderBy(x => x.Time));
	}

	async Task SetupHistogramSeries()
	{
		_Histogram = await _ChartComponent.AddHistogramSeriesAsync(new HistogramStyleOptions
		{
			PriceScaleId = "overlay",
			PriceLineVisible = false,
			LastValueVisible = false
		});
		await _Histogram.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new HistogramData
		{
			Time = x.OpenTime,
			Value = x.Volume,
			Color = x.ClosePrice > x.OpenPrice ? Color.Green : Color.Red
		}));
	}

	void OnChartCrosshairMoved(object sender, MouseEventParams e)
	{
		_MouseHoverPrices = e.SeriesPrices;
		StateHasChanged();
	}

	async void OnChartClicked(object sender, MouseEventParams e)
	{
		if (_LastClickedId == _Candlestick.UniqueJavascriptId)
			await _Candlestick.ApplyOptions(new());
		else if (_LastClickedId == _Histogram.UniqueJavascriptId)
			await _Histogram.ApplyOptions(new());

		_LastClickedId = e.HoveredSeries?.UniqueJavascriptId;

		if (_LastClickedId == _Candlestick.UniqueJavascriptId)
			await _Candlestick.ApplyOptions(new()
			{
				UpColor = Color.LightGreen,
				DownColor = Color.Pink
			});
		else if (_LastClickedId == _Histogram.UniqueJavascriptId)
			await _Histogram.ApplyOptions(new()
			{
				Color = Color.LightBlue
			});
	}

	async Task OnApplyOptions()
	{
		await ChartComponent.ApplyOptions(_Options);
	}
}
