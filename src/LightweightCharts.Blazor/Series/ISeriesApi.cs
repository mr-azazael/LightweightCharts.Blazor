using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Series
{
	/// <summary>
	/// Represents the interface for interacting with series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi"/>
	/// </summary>
	public interface ISeriesApi : IJsObjectWrapper
	{
		/// <summary>
		/// Unique instance id.
		/// </summary>
		string UniqueJavascriptId { get; }

		/// <summary>
		/// Price lines added to this series.
		/// </summary>
		IEnumerable<IPriceLine> PriceLines { get; }

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
		/// <param name="items">Ordered (earlier time point goes first) array of data items. Old data is fully replaced with the new one.</param>
		Task SetData(IEnumerable<ISeriesData> data);

		/// <summary>
		/// Adds new data item to the existing set (or updates the latest item if times of the passed/latest items are equal).<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#update"/>
		/// </summary>
		/// <param name="item">A single data item to be added. Time of the new item must be greater or equal to the latest existing time point. If the new item's time is equal to the last existing item's time, then the existing item is replaced with the new one.</param>
		Task Update(ISeriesData item);

		/// <summary>
		/// Returns a bar data by provided logical index.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#databyindex"/>
		/// </summary>
		/// <param name="logicalIndex">	Logical index.</param>
		/// <param name="mismatchDirection">Search direction if no data found at provided logical index.</param>
		/// <returns>Original data item provided via setData or update methods.</returns>
		Task<ISeriesData> DataByIndex(int logicalIndex, MismatchDirection? mismatchDirection);

		/// <summary>
		/// Returns all the bar data for the series.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#data"/>
		/// </summary>
		/// <returns>Original data items provided via setData or update methods.</returns>
		Task<IEnumerable<ISeriesData>> Data();

		/// <summary>
		/// Allows to set/replace all existing series markers with new ones.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#setmarkers"/>
		/// </summary>
		/// <param name="markers">An array of series markers. This array should be sorted by time. Several markers with same time are allowed.</param>
		Task SetMarkers(IEnumerable<SeriesMarker> markers);

		/// <summary>
		/// Returns an array of series markers.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#markers"/>
		/// </summary>
		Task<IEnumerable<SeriesMarker>> Markers();

		/// <summary>
		/// Creates a new price line.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#createpriceline"/>
		/// </summary>
		/// <param name="options">Any subset of options, however price is required.</param>
		Task<IPriceLine> CreatePriceLine(PriceLineOptions options);

		/// <summary>
		/// Removes the price line that was created before.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#removepriceline"/>
		/// </summary>
		/// <param name="priceLine">A line to remove.</param>
		Task RemovePriceLine(IPriceLine priceLine);

		/// <summary>
		/// Return current series type.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#seriestype"/>
		/// </summary>
		/// <returns>Type of the series.</returns>
		Task<SeriesType> SeriesType();
	}

	/// <summary>
	/// Adds Options and ApplyOptions to the <see cref="ISeriesApi"/> interface.
	/// </summary>
	/// <typeparam name="O"></typeparam>
	public interface ISeriesApi<O> : ISeriesApi, ICustomizableObject<O>
		where O : SeriesOptionsCommon, new()
	{

	}
}
