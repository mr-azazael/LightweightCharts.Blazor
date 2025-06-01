using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using LightweightCharts.Blazor.Plugins;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Series
{
	internal interface ISeriesApiInternal<H> : ISeriesApi<H>
		where H : struct
	{
		IEnumerable<Type> AllowedDataItemTypes { get; }
	}

	internal class SeriesApi<H, O> : CustomizableObject<O>, ISeriesApi<H, O>, ISeriesApiInternal<H>
		where H : struct
		where O : SeriesOptionsCommon, new()
	{
		public SeriesApi(string uniqueId, IJSRuntime jsRuntime, IJSObjectReference jsObject, IChartApiBase<H> parent, SeriesType seriesType, params Type[] dataItemTypes)
			: base(jsRuntime, jsObject)
		{
			UniqueJavascriptId = uniqueId;
			Parent = parent;
			_AllowedDataItemTypes = dataItemTypes;
			_SeriesType = seriesType;
			_GetData = GetType().GetMethod(nameof(GetData), BindingFlags.Static | BindingFlags.NonPublic).MakeGenericMethod(dataItemTypes[0]);
		}

		SeriesType _SeriesType;
		Type[] _AllowedDataItemTypes;
		List<IPriceLine<H>> _PriceLines = new();
		IPriceScaleApi _PriceScale;
		MethodInfo _GetData;

		public string UniqueJavascriptId { get; }

		public IEnumerable<IPriceLine<H>> PriceLines
			=> _PriceLines;

		public IEnumerable<Type> AllowedDataItemTypes
			=> _AllowedDataItemTypes;

		public event EventHandler<DataChangedScope> DataChanged;

		public IChartApiBase<H> Parent { get; init; }

		public async Task SetData(IEnumerable<ISeriesData<H>> items)
		{
			foreach (var item in items)
			{
				var itemType = item.GetType();
				if (!_AllowedDataItemTypes.Contains(itemType) && itemType != typeof(WhitespaceData<H>))
				{
					var typeName = _AllowedDataItemTypes.Select(x => x.GetType().Name);
					var seriesType = await SeriesType();
					throw new InvalidOperationException($"{seriesType} series only supports data items of type {string.Join(", ", typeName)}.");
				}
			}

			//having mixed data items with whitespaces will result in serializing all of them as whitespaces, losing any price data
			//this ensures data items are serialized correctly before sending the to the javascript methods
			var jsonObjects = items.Select(x => System.Text.Json.JsonSerializer.SerializeToDocument(x, x.GetType()));
			await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "setData", false, jsonObjects);
			DataChanged?.Invoke(this, DataChangedScope.Full);
		}

		public async Task Update(ISeriesData<H> item)
		{
			await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "update", false, item);
			DataChanged?.Invoke(this, DataChangedScope.Update);
		}

		public async Task<ISeriesData<H>> DataByIndex(int logicalIndex, MismatchDirection? mismatchDirection)
			=> await JsModule.InvokeAsync<ISeriesData<H>>(JsRuntime, JsObjectReference, "dataByIndex", false, logicalIndex, mismatchDirection);

		public async Task<IEnumerable<ISeriesData<H>>> Data()
		{
			var dataTask = await (Task<Array>)_GetData.Invoke(null, [JsObjectReference]);
			return dataTask.Cast<ISeriesData<H>>();
		}

		public async Task<IPriceLine<H>> CreatePriceLine(PriceLineOptions options)
		{
			var priceLineRef = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, JsObjectReference, "createPriceLine", false, options ?? new PriceLineOptions());
			var priceLine = new PriceLine<H>(JsRuntime, priceLineRef, this);
			_PriceLines.Add(priceLine);
			return priceLine;
		}

		public async Task RemovePriceLine(IPriceLine<H> priceLine)
		{
			if (_PriceLines.Contains(priceLine))
			{
				await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "removePriceLine", false, priceLine.JsObjectReference);
				_PriceLines.Remove(priceLine);
			}
		}

		public async Task<BarsInfo> BarsInLogicalRange(LogicalRange logicalRange)
			=> await JsModule.InvokeAsync<BarsInfo>(JsRuntime, JsObjectReference, "barsInLogicalRange", false, logicalRange);

		public async Task<SeriesType> SeriesType()
			=> await JsModule.InvokeAsync<SeriesType>(JsRuntime, JsObjectReference, "seriesType");

		public async Task<double> PriceToCoordinate(double price)
			=> await JsModule.InvokeAsync<double>(JsRuntime, JsObjectReference, "priceToCoordinate", false, price);

		public async Task<double> CoordinateToPrice(double coordinate)
			=> await JsModule.InvokeAsync<double>(JsRuntime, JsObjectReference, "coordinateToPrice", false, coordinate);

		public async Task<IPriceScaleApi> PriceScale()
		{
			if (_PriceScale == null)
			{
				var reference = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, JsObjectReference, "priceScale");
				_PriceScale = new PriceScaleApi(JsRuntime, reference);
			}

			return _PriceScale;
		}

		public async Task MoveToPane(int paneIndex)
			=> await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "moveToPane", false, paneIndex);

		public async Task<IPaneApi<H>> GetPane()
		{
			var paneReference = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, JsObjectReference, "getPane");
			return new PaneApi<H>(JsRuntime, Parent, paneReference);
		}

		public async Task<int> SeriesOrder()
			=> await JsModule.InvokeAsync<int>(JsRuntime, JsObjectReference, "seriesOrder");

		public async Task SetSeriesOrder(int order)
			=> await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "setSeriesOrder", false, order);

		public ValueTask<ISeriesMarkersPluginApi<H>> CreateSeriesMarkers<M>(IEnumerable<M> markers)
			where M : SeriesMarkerBase<H>
			=> JsModule.CreateSeriesMarkers(JsRuntime, this, markers?.ToArray() ?? []);

		public ValueTask<ISeriesUpDownMarkerPluginApi<H>> CreateUpDownMarkers(UpDownMarkersPluginOptions options = null)
		{
			if (_SeriesType != Customization.Enums.SeriesType.Line && _SeriesType != Customization.Enums.SeriesType.Area)
				throw new InvalidOperationException("Method only available for Line and Area series");

			return JsModule.CreateUpDownMarkers(JsRuntime, this, options ?? new());
		}

		static async Task<Array> GetData<T>(IJSRuntime runtime, IJSObjectReference jsObject)
			=> await JsModule.InvokeAsync<T[]>(runtime, jsObject, "data");
	}
}
