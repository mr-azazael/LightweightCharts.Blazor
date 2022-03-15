using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models.Events;
using LightweightCharts.Blazor.Series;
using System.Drawing;

namespace LightweightCharts.Blazor.Client.Pages
{
	partial class CandlestickSeries
	{
		ISeriesApi<CandlestickStyleOptions> _Candlestick;
		ISeriesApi<HistogramStyleOptions> _Histogram;
		IEnumerable<SeriesPrice> _MouseHoverPrices;
		string _LastClickedId;

		ChartComponent ChartComponent
		{
			set => InitializeChartComponent(value);
		}

		async void InitializeChartComponent(ChartComponent chart)
		{
			await chart.InitializationCompleted;

			//clicking on markers seem to return the candlestick series
			//no hovered series is sent when clicking on the actual series items...
			chart.Clicked += OnChartClicked;
			chart.CrosshairMoved += OnChartCrosshairMoved;

			await chart.ApplyOptions(new Customization.Chart.ChartOptions
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
						Top = 0
					}
				},
				OverlayPriceScale = new Customization.Chart.BasePriceScaleOptions
				{
					ScaleMargins = new Customization.PriceScaleMargins
					{
						Bottom = 0,
						Top = 0.9d
					}
				}
			});

			await SetupCandlestickSeries(chart);
			await SetupHistogramSeries(chart);

			var timeScale = await chart.TimeScaleAsync();
			await timeScale.FitContent();
		}

		async Task SetupCandlestickSeries(ChartComponent chart)
		{
			_Candlestick = await chart.AddCandlestickSeriesAsync();
			await _Candlestick.SetData(BtcUsdDataPoints.OneWeek.OrderBy(x => x.OpenTime).Select(x => new OhlcData
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

			await _Candlestick.SetMarkers(new Marker[]
			{
				new Marker
				{
					Time = minLowPoint.OpenTime,
					Position = SeriesMarkerPosition.BelowBar,
					Shape = SeriesMarkerShape.Circle,
					Color = Color.Purple,
					Size = 1,
					Text = "Minimum low"
				},
				new Marker
				{
					Time = maxHighPoint.OpenTime,
					Position = SeriesMarkerPosition.AboveBar,
					Shape = SeriesMarkerShape.Circle,
					Color = Color.Purple,
					Size = 1,
					Text = "Maximum high"
				},
				new Marker
				{
					Time = minClose.OpenTime,
					Position = SeriesMarkerPosition.BelowBar,
					Shape = SeriesMarkerShape.ArrowUp,
					Color = Color.Red,
					Size = 3,
					Text = "Minimum close"
				},
				new Marker
				{
					Time = maxClose.OpenTime,
					Position = SeriesMarkerPosition.AboveBar,
					Shape = SeriesMarkerShape.ArrowDown,
					Color = Color.Red,
					Size = 3,
					Text = "Maximum close"
				}
			}.OrderBy(x => x.Time));
		}

		async Task SetupHistogramSeries(ChartComponent chart)
		{
			_Histogram = await chart.AddHistogramSeriesAsync(new Customization.Series.HistogramStyleOptions
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

		void OnChartCrosshairMoved(object sender, MouseEventArgs e)
		{
			_MouseHoverPrices = e.SeriesPrices;
			StateHasChanged();
		}

		async void OnChartClicked(object sender, MouseEventArgs e)
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
	}
}
