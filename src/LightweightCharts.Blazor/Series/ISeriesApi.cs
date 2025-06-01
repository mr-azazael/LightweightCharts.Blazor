using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using LightweightCharts.Blazor.Plugins;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Series
{
	/// <summary>
	/// Represents the interface for interacting with series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi"/>
	/// </summary>
	/// <typeparam name="H">horizontal axis value type</typeparam>
	public interface ISeriesApi<H> : IJsObjectWrapper
		where H : struct
	{
		/// <summary>
		/// Unique instance id.
		/// </summary>
		string UniqueJavascriptId { get; }

		/// <summary>
		/// Price lines added to this series.
		/// </summary>
		IEnumerable<IPriceLine<H>> PriceLines { get; }

		/// <summary>
		/// Subscribe to the data changed event. This event is fired whenever the update or setData method is evoked on the series.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#subscribedatachanged"/>
		/// </summary>
		event EventHandler<DataChangedScope> DataChanged;

		/// <summary>
		/// Converts specified series price to pixel coordinate according to the series price scale.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#pricetocoordinate"/>
		/// </summary>
		/// <param name="price">Input price to be converted.</param>
		/// <returns>Pixel coordinate of the price level on the chart.</returns>
		Task<double> PriceToCoordinate(double price);

		/// <summary>
		/// Converts specified coordinate to price value according to the series price scale.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#coordinatetoprice"/>
		/// </summary>
		/// <param name="coordinate">Input coordinate to be converted.</param>
		/// <returns>Price value of the coordinate on the chart.</returns>
		Task<double> CoordinateToPrice(double coordinate);

		/// <summary>
		/// Returns bars information for the series in the provided logical range or null, if no series data has been found in the requested range.<br/>
		/// This method can be used, for instance, to implement downloading historical data while scrolling to prevent a user from seeing empty space.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#barsinlogicalrange"/>
		/// </summary>
		/// <param name="logicalRange">The logical range to retrieve info for.</param>
		/// <returns>The bars info for the given logical range.</returns>
		Task<BarsInfo> BarsInLogicalRange(LogicalRange logicalRange);

		/// <summary>
		/// Returns interface of the price scale the series is currently attached.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#pricescale"/>
		/// </summary>
		/// <returns>IPriceScaleApi object to control the price scale.</returns>
		Task<IPriceScaleApi> PriceScale();

		/// <summary>
		/// Sets or replaces series data.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#setdata"/>
		/// </summary>
		/// <param name="data">Ordered (earlier time point goes first) array of data items. Old data is fully replaced with the new one.</param>
		Task SetData(IEnumerable<ISeriesData<H>> data);

		/// <summary>
		/// Adds new data item to the existing set (or updates the latest item if times of the passed/latest items are equal).<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#update"/>
		/// </summary>
		/// <param name="item">A single data item to be added. Time of the new item must be greater or equal to the latest existing time point. If the new item's time is equal to the last existing item's time, then the existing item is replaced with the new one.</param>
		Task Update(ISeriesData<H> item);

		/// <summary>
		/// Returns a bar data by provided logical index.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#databyindex"/>
		/// </summary>
		/// <param name="logicalIndex">	Logical index.</param>
		/// <param name="mismatchDirection">Search direction if no data found at provided logical index.</param>
		/// <returns>Original data item provided via setData or update methods.</returns>
		Task<ISeriesData<H>> DataByIndex(int logicalIndex, MismatchDirection? mismatchDirection);

		/// <summary>
		/// Returns all the bar data for the series.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#data"/>
		/// </summary>
		/// <returns>Original data items provided via setData or update methods.</returns>
		Task<IEnumerable<ISeriesData<H>>> Data();

		/// <summary>
		/// Creates a new price line.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#createpriceline"/>
		/// </summary>
		/// <param name="options">Any subset of options, however price is required.</param>
		Task<IPriceLine<H>> CreatePriceLine(PriceLineOptions options);

		/// <summary>
		/// Removes the price line that was created before.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#removepriceline"/>
		/// </summary>
		/// <param name="priceLine">A line to remove.</param>
		Task RemovePriceLine(IPriceLine<H> priceLine);

		/// <summary>
		/// Return current series type.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#seriestype"/>
		/// </summary>
		/// <returns>Type of the series.</returns>
		Task<SeriesType> SeriesType();

		/// <summary>
		/// Move the series to another pane.<br/>
		/// If the pane with the specified index does not exist, the pane will be created.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#movetopane"/>
		/// </summary>
		/// <param name="paneIndex">The index of the pane. Should be a number between 0 and the total number of panes.</param>
		Task MoveToPane(int paneIndex);

		/// <summary>
		/// Returns the pane to which the series is currently attached.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#getpane"/>
		/// </summary>
		/// <returns>Pane API object to control the pane</returns>
		Task<IPaneApi<H>> GetPane();

		/// <summary>
		/// Gets the zero-based index of this series within the list of all series on the current pane.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#seriesOrder"/>
		/// </summary>
		/// <returns>The current index of the series in the pane's series collection.</returns>
		Task<int> SeriesOrder();

		/// <summary>
		/// Sets the zero-based index of this series within the pane's series collection, thereby adjusting its rendering order.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#setSeriesOrder"/>
		/// </summary>
		/// <param name="order">The desired zero-based index to set for this series within the pane.</param>
		Task SetSeriesOrder(int order);

		#region plugins

		/// <summary>
		/// A function to create a series markers primitive.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/functions/createSeriesMarkers"/>
		/// </summary>
		/// <param name="markers">An array of markers to be displayed on the series.</param>
		ValueTask<ISeriesMarkersPluginApi<H>> CreateSeriesMarkers(IEnumerable<SeriesMarkerBase<H>> markers);

		/// <summary>
		/// Creates and attaches the Series Up Down Markers Plugin.<br/>
		/// Only works with Line and Area series, otherwise it throws an <see cref="InvalidOperationException"/>
		/// </summary>
		/// <param name="options">options for the Up Down Markers Plugin</param>
		/// <returns>Api for Series Up Down Marker Plugin.</returns>
		ValueTask<ISeriesUpDownMarkerPluginApi<H>> CreateUpDownMarkers(UpDownMarkersPluginOptions options = null);

		#endregion
	}

	/// <summary>
	/// Adds Options and ApplyOptions to the <see cref="ISeriesApi{H}"/> interface.
	/// </summary>
	/// <typeparam name="H">horizontal axis value type</typeparam>
	/// <typeparam name="O">options type</typeparam>
	public interface ISeriesApi<H, O> : ISeriesApi<H>, ICustomizableObject<O>
		where H : struct
		where O : SeriesOptionsCommon, new()
	{

	}
}
