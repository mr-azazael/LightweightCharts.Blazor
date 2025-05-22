using LightweightCharts.Blazor.Customization.Enums;
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
	public interface IChartApiBase<H>
		where H : struct
	{
		/// <summary>
		/// Subscribe to the chart click event.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#subscribeclick"/>
		/// </summary>
		event EventHandler<MouseEventParams<H>> Clicked;

		/// <summary>
		/// Subscribe to the chart double-click event.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#subscribedblclick"/>
		/// </summary>
		event EventHandler<MouseEventParams<H>> DoubleClicked;

		/// <summary>
		/// Subscribe to the crosshair move event.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#subscribecrosshairmove"/>
		/// </summary>
		event EventHandler<MouseEventParams<H>> CrosshairMoved;

		/// <summary>
		/// Removes the chart object including all DOM elements. This is an irreversible operation, you cannot do anything with the chart after removing it.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#remove"/>
		/// </summary>
		Task Remove();

		/// <summary>
		/// Sets fixed size of the chart. By default chart takes up 100% of its container.<br/>
		/// If chart has the autoSize option enabled, and the ResizeObserver is available then the width and height values will be ignored.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#resize"/>
		/// </summary>
		/// <param name="width">Target width of the chart.</param>
		/// <param name="height">Target height of the chart.</param>
		/// <param name="repaint">True to initiate resize immediately. One could need this to get screenshot immediately after resize.</param>
		Task Resize(double width, double height, bool repaint);

		/// <summary>
		/// Creates a series with specified parameters.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#addseries"/>
		/// </summary>
		/// <typeparam name="O">series options type</typeparam>
		/// <param name="type">Series type.</param>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <param name="paneIndex">An index of the pane where the series should be created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<H, O>> AddSeries<O>(SeriesType type, O options = null, int paneIndex = 0)
			where O : SeriesOptionsCommon, new();

		/// <summary>
		/// Removes a series of any type. This is an irreversible operation, you cannot do anything with the series after removing it.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#removeseries"/>
		/// </summary>
		/// <param name="series">The series to be removed.</param>
		Task RemoveSeries(ISeriesApi<H> series);

		/// <summary>
		/// Returns API to manipulate a price scale.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#pricescale"/>
		/// </summary>
		/// <param name="priceScaleId">ID of the price scale.</param>
		/// <param name="paneIndex">Index of the pane (default: 0)</param>
		/// <returns>Price scale API.</returns>
		Task<IPriceScaleApi> PriceScale(string priceScaleId, int paneIndex = 0);

		/// <summary>
		/// Returns API to manipulate the time scale.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#timescale"/>
		/// </summary>
		/// <returns>Time scale API</returns>
		Task<ITimeScaleApi<H>> TimeScale();

		/// <summary>
		/// Make a screenshot of the chart with all the elements excluding crosshair.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#takescreenshot"/>
		/// </summary>
		/// <returns>Returns the canvas data.</returns>
		Task<byte[]> TakeScreenshot();

		/// <summary>
		/// Returns array of panes' API.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#panes"/>
		/// </summary>
		/// <returns>Returns array of panes' API</returns>
		Task<IPaneApi<H>[]> Panes();

		/// <summary>
		/// Removes a pane with index.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#removepane"/>
		/// </summary>
		/// <param name="index">the pane to be removed</param>
		Task RemovePane(int index);

		/// <summary>
		/// Swap the position of two panes.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#swappanes"/>
		/// </summary>
		/// <param name="first">the first index</param>
		/// <param name="second">the second index</param>
		Task SwapPanes(int first, int second);

		/// <summary>
		/// Returns the active state of the autoSize option. This can be used to check whether the chart is handling resizing automatically with a ResizeObserver.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#autosizeactive"/>
		/// </summary>
		/// <returns>Whether the autoSize option is enabled and the active.</returns>
		Task<bool> AutoSizeActive();

		/// <summary>
		/// Set the crosshair position within the chart.<br/>
		/// Usually the crosshair position is set automatically by the user's actions. However in some cases you may want to set it explicitly.<br/>
		/// For example if you want to synchronise the crosshairs of two separate charts.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#setcrosshairposition"/>
		/// </summary>
		/// <param name="price">The price (vertical coordinate) of the new crosshair position.</param>
		/// <param name="horizontalPosition">The horizontal coordinate (time by default) of the new crosshair position.</param>
		/// <param name="series"></param>
		Task SetCrosshairPosition(double price, H horizontalPosition, ISeriesApi<H> series);

		/// <summary>
		/// Clear the crosshair position within the chart.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#clearcrosshairposition"/>
		/// </summary>
		Task ClearCrosshairPosition();

		/// <summary>
		/// Returns the dimensions of the chart pane (the plot surface which excludes time and price scales). This would typically only be useful for plugin development.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApiBase#panesize"/>
		/// </summary>
		/// <param name="paneIndex">The index of the pane</param>
		/// <returns>Dimensions of the chart pane</returns>
		Task<PaneSize> PaneSize(int? paneIndex);

		#region plugins

		/// <summary>
		/// Returns the current version as a string. For example '3.3.0'.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/functions/version"/>
		/// </summary>
		ValueTask<string> Version();

		#endregion

		internal ISeriesApi<H>[] ResolveSeriesFromIds(string[] seriesIds);
	}
}
