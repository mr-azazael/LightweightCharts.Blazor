using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.Models;
using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.DataItems;
using System;

namespace LightweightCharts.Blazor.Series
{
	/// <summary>
	/// Represents the interface for interacting with series.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi
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
		/// Sets or replaces series data.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#setdata
		/// </summary>
		/// <param name="items">Ordered (earlier time point goes first) array of data items. Old data is fully replaced with the new one.</param>
		Task SetData(IEnumerable<ISeriesData> data);

		/// <summary>
		/// Adds new data item to the existing set (or updates the latest item if times of the passed/latest items are equal).<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#update
		/// </summary>
		/// <param name="item">A single data item to be added. Time of the new item must be greater or equal to the latest existing time point. If the new item's time is equal to the last existing item's time, then the existing item is replaced with the new one.</param>
		Task Update(ISeriesData item);

		/// <summary>
		/// Allows to set/replace all existing series markers with new ones.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#setmarkers
		/// </summary>
		/// <param name="markers">An array of series markers. This array should be sorted by time. Several markers with same time are allowed.</param>
		Task SetMarkers(IEnumerable<Marker> markers);

		/// <summary>
		/// Creates a new price line.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#createpriceline
		/// </summary>
		/// <param name="options">Any subset of options.</param>
		Task<IPriceLine> CreatePriceLine(PriceLineOptions options);

		/// <summary>
		/// Removes the price line that was created before.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#removepriceline
		/// </summary>
		/// <param name="priceLine">A line to remove.</param>
		Task RemovePriceLine(IPriceLine priceLine);

		/// <summary>
		/// Returns bars information for the series in the provided logical range or null, if no series data has been found in the requested range.<br/>
		/// This method can be used, for instance, to implement downloading historical data while scrolling to prevent a user from seeing empty space.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#barsinlogicalrange
		/// </summary>
		/// <param name="logicalRange">The logical range to retrieve info for.</param>
		/// <returns>The bars info for the given logical range.</returns>
		Task<BarsInfo> BarsInLogicalRange(LogicalRange logicalRange);

		/// <summary>
		/// Return current series type.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#seriestype
		/// </summary>
		/// <returns>Type of the series.</returns>
		Task<SeriesType> SeriesType();

		/// <summary>
		/// Converts specified series price to pixel coordinate according to the series price scale.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#pricetocoordinate
		/// </summary>
		/// <param name="price">Input price to be converted.</param>
		/// <returns>Pixel coordinate of the price level on the chart.</returns>
		Task<long?> PriceToCoordinate(double price);

		/// <summary>
		/// Converts specified coordinate to price value according to the series price scale.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#coordinatetoprice
		/// </summary>
		/// <param name="coordinate">Input coordinate to be converted.</param>
		/// <returns>Price value of the coordinate on the chart.</returns>
		Task<long?> CoordinateToPrice(double coordinate);

		/// <summary>
		/// Returns interface of the price scale the series is currently attached.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesApi#pricescale
		/// </summary>
		/// <returns>IPriceScaleApi object to control the price scale.</returns>
		Task<IPriceScaleApi> PriceScale();
	}

	/// <summary>
	/// Adds Options and ApplyOptions to the <see cref="ISeriesApi"/> interface.
	/// </summary>
	/// <typeparam name="O"></typeparam>
	public interface ISeriesApi<O> : ISeriesApi, ICustomizableObject<O> where O : SeriesOptionsCommon, new()
	{

	}

	internal interface ISeriesApiInternal : ISeriesApi
	{
		IEnumerable<Type> AllowedDataItemTypes { get; }
	}

	internal class SeriesApi<T> : CustomizableObject<T>, ISeriesApi<T>, ISeriesApiInternal where T : SeriesOptionsCommon, new()
	{
		internal SeriesApi(string uniqueId, IJSObjectReference jsObject, ChartComponent parent, params Type[] dataItemTypes) : base(jsObject)
		{
			UniqueJavascriptId = uniqueId;
			Parent = parent;
			_AllowedDataItemTypes = dataItemTypes;
		}

		Type[] _AllowedDataItemTypes;
		List<IPriceLine> _PriceLines = new();
		IPriceScaleApi _PriceScale;

		public IEnumerable<IPriceLine> PriceLines => _PriceLines;
		public IEnumerable<Type> AllowedDataItemTypes => _AllowedDataItemTypes;

		public ChartComponent Parent { get; init; }

		public string UniqueJavascriptId { get; }

		public async Task SetData(IEnumerable<ISeriesData> items)
		{
			foreach (var item in items)
			{
				var itemType = item.GetType();
				if (!_AllowedDataItemTypes.Contains(itemType) && itemType != typeof(WhitespaceData))
				{
					var typeName = _AllowedDataItemTypes.Select(x => x.GetType().Name);
					var seriesType = await SeriesType();
					throw new InvalidOperationException($"{seriesType} series only supports data items of type {string.Join(", ", typeName)}.");
				}
			}

			//having mixed data items with whitespaces will result in serializing all of them as whitespaces, losing any price data
			//this ensures data items are serialized correctly before sending the to the javascript methods
			var jsonObjects = items.Select(x => System.Text.Json.JsonSerializer.SerializeToDocument(x, x.GetType()));
			await JsObjectReference.InvokeVoidAsync("setData", jsonObjects);
		}

		public async Task Update(ISeriesData item)
			=> await JsObjectReference.InvokeVoidAsync("update", item);

		public async Task SetMarkers(IEnumerable<Marker> markers)
			=> await JsObjectReference.InvokeVoidAsync("setMarkers", markers);

		public async Task<IPriceLine> CreatePriceLine(PriceLineOptions options)
		{
			var priceLineRef = await JsObjectReference.InvokeAsync<IJSObjectReference>("createPriceLine", options ?? new PriceLineOptions());
			var priceLine = new PriceLine(priceLineRef, this);
			_PriceLines.Add(priceLine);
			return priceLine;
		}

		public async Task RemovePriceLine(IPriceLine priceLine)
		{
			if (_PriceLines.Contains(priceLine))
			{
				await JsObjectReference.InvokeVoidAsync("removePriceLine", priceLine.JsObjectReference);
				_PriceLines.Remove(priceLine);
			}
		}

		public async Task<BarsInfo> BarsInLogicalRange(LogicalRange logicalRange)
			=> await JsObjectReference.InvokeAsync<BarsInfo>("barsInLogicalRange", logicalRange);

		public async Task<SeriesType> SeriesType()
			=> await JsObjectReference.InvokeAsync<SeriesType>("seriesType");

		public async Task<long?> PriceToCoordinate(double price)
			=> await JsObjectReference.InvokeAsync<long?>("priceToCoordinate", price);

		public async Task<long?> CoordinateToPrice(double coordinate)
			=> await JsObjectReference.InvokeAsync<long?>("coordinateToPrice", coordinate);

		public async Task<IPriceScaleApi> PriceScale()
		{
			if (_PriceScale == null)
			{
				var reference = await JsObjectReference.InvokeAsync<IJSObjectReference>("priceScale");
				_PriceScale = new PriceScaleApi(reference);
			}

			return _PriceScale;
		}
	}
}
