using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.Models;
using LightweightCharts.Blazor.Models.Events;
using LightweightCharts.Blazor.Series;
using System;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Charts
{
	/// <summary>
	/// The main interface of a single chart.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase"/>
	/// </summary>
	public interface IChartApiBase
	{
		/// <summary>
		/// Subscribe to the chart click event.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#subscribeclick"/>
		/// </summary>
		event EventHandler<MouseEventParams> Clicked;

		/// <summary>
		/// Subscribe to the chart double-click event.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#subscribedblclick"/>
		/// </summary>
		event EventHandler<MouseEventParams> DoubleClicked;

		/// <summary>
		/// Subscribe to the crosshair move event.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#subscribecrosshairmove"/>
		/// </summary>
		event EventHandler<MouseEventParams> CrosshairMoved;

		/// <summary>
		/// Removes the chart object including all DOM elements. This is an irreversible operation, you cannot do anything with the chart after removing it.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#remove"/>
		/// </summary>
		Task RemoveAsync();

		/// <summary>
		/// Sets fixed size of the chart. By default chart takes up 100% of its container.<br/>
		/// If chart has the autoSize option enabled, and the ResizeObserver is available then the width and height values will be ignored.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#resize"/>
		/// </summary>
		/// <param name="width">Target width of the chart.</param>
		/// <param name="height">Target height of the chart.</param>
		/// <param name="repaint">True to initiate resize immediately. One could need this to get screenshot immediately after resize.</param>
		Task ResizeAsync(double width, double height, bool repaint);

		/// <summary>
		/// Creates an area series with specified parameters.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addareaseries"/>
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<AreaStyleOptions>> AddAreaSeriesAsync(AreaStyleOptions options = null);

		/// <summary>
		/// Creates a baseline series with specified parameters.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addbaselineseries"/>
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<BaselineStyleOptions>> AddBaselineSeriesAsync(BaselineStyleOptions options = null);

		/// <summary>
		/// Creates a bar series with specified parameters.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addbarseries"/>
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<BarStyleOptions>> AddBarSeriesAsync(BarStyleOptions options = null);

		/// <summary>
		/// Creates a candlestick series with specified parameters.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addcandlestickseries"/>
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<CandlestickStyleOptions>> AddCandlestickSeriesAsync(CandlestickStyleOptions options = null);

		/// <summary>
		/// Creates a histogram series with specified parameters.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addhistogramseries"/>
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<HistogramStyleOptions>> AddHistogramSeriesAsync(HistogramStyleOptions options = null);

		/// <summary>
		/// Creates a line series with specified parameters.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addlineseries"/>
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<LineSeriesOptions>> AddLineSeriesAsync(LineSeriesOptions options = null);

		/// <summary>
		/// Removes a series of any type. This is an irreversible operation, you cannot do anything with the series after removing it.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#removeseries"/>
		/// </summary>
		/// <param name="series">The series to be removed.</param>
		Task RemoveSeriesAsync(ISeriesApi series);

		/// <summary>
		/// Returns API to manipulate a price scale.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#pricescale"/>
		/// </summary>
		/// <param name="id">ID of the price scale.</param>
		/// <returns>Price scale API.</returns>
		Task<IPriceScaleApi> PriceScaleAsync(string id);

		/// <summary>
		/// Returns API to manipulate the time scale.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#timescale"/>
		/// </summary>
		/// <returns>Time scale API</returns>
		Task<ITimeScaleApi> TimeScaleAsync();

		/// <summary>
		/// Applies new options to the chart.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#applyoptions"/>
		/// </summary>
		/// <param name="options">Any subset of options.</param>
		Task ApplyOptions(ChartOptionsBase options);

		/// <summary>
		/// Returns currently applied options.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#options"/>
		/// </summary>
		/// <returns>Full set of currently applied options, including defaults.</returns>
		Task<ChartOptionsBase> Options();

		/// <summary>
		/// Make a screenshot of the chart with all the elements excluding crosshair.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#takescreenshot"/>
		/// </summary>
		/// <returns>Returns the canvas data.</returns>
		Task<byte[]> TakeScreenshotAsync();

		/// <summary>
		/// Returns the active state of the autoSize option. This can be used to check whether the chart is handling resizing automatically with a ResizeObserver.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#autosizeactive"/>
		/// </summary>
		/// <returns>Whether the autoSize option is enabled and the active.</returns>
		Task<bool> AutoSizeActiveAsync();

		/// <summary>
		/// Set the crosshair position within the chart.<br/>
		/// Usually the crosshair position is set automatically by the user's actions. However in some cases you may want to set it explicitly.<br/>
		/// For example if you want to synchronise the crosshairs of two separate charts.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#setcrosshairposition"/>
		/// </summary>
		/// <param name="price">The price (vertical coordinate) of the new crosshair position.</param>
		/// <param name="horizontalPosition">The horizontal coordinate (time by default) of the new crosshair position.</param>
		/// <param name="series"></param>
		Task SetCrosshairPosition(double price, long horizontalPosition, ISeriesApi series);

		/// <summary>
		/// Clear the crosshair position within the chart.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#clearcrosshairposition"/>
		/// </summary>
		Task ClearCrosshairPosition();

		/// <summary>
		/// Returns the dimensions of the chart pane (the plot surface which excludes time and price scales). This would typically only be useful for plugin development.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#panesize"/>
		/// </summary>
		/// <returns>Dimensions of the chart pane.</returns>
		Task<PaneSize> PaneSize();
	}
}
