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
			await JsObjectReference.InvokeVoidAsync("setData", jsonObjects);
			DataChanged?.Invoke(this, DataChangedScope.Full);
		}

		public async Task Update(ISeriesData<H> item)
		{
			await JsObjectReference.InvokeVoidAsync("update", item);
			DataChanged?.Invoke(this, DataChangedScope.Update);
		}

		public async Task<ISeriesData<H>> DataByIndex(int logicalIndex, MismatchDirection? mismatchDirection)
		{
			return await JsObjectReference.InvokeAsync<ISeriesData<H>>("dataByIndex", logicalIndex, mismatchDirection);
		}

		public async Task<IEnumerable<ISeriesData<H>>> Data()
		{
			var dataTask = await (Task<Array>)_GetData.Invoke(null, [JsObjectReference]);
			return dataTask.Cast<ISeriesData<H>>();
		}

		public async Task<IPriceLine<H>> CreatePriceLine(PriceLineOptions options)
		{
			var priceLineRef = await JsObjectReference.InvokeAsync<IJSObjectReference>("createPriceLine", options ?? new PriceLineOptions());
			var priceLine = new PriceLine<H>(JsRuntime, priceLineRef, this);
			_PriceLines.Add(priceLine);
			return priceLine;
		}

		public async Task RemovePriceLine(IPriceLine<H> priceLine)
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

		public async Task<double> PriceToCoordinate(double price)
			=> await JsObjectReference.InvokeAsync<double>("priceToCoordinate", price);

		public async Task<double> CoordinateToPrice(double coordinate)
			=> await JsObjectReference.InvokeAsync<double>("coordinateToPrice", coordinate);

		public async Task<IPriceScaleApi> PriceScale()
		{
			if (_PriceScale == null)
			{
				var reference = await JsObjectReference.InvokeAsync<IJSObjectReference>("priceScale");
				_PriceScale = new PriceScaleApi(JsRuntime, reference);
			}

			return _PriceScale;
		}

		public async Task MoveToPane(int paneIndex)
			=> await JsObjectReference.InvokeVoidAsync("moveToPane", paneIndex);

		public async Task<IPaneApi<H>> GetPane()
		{
			var paneReference = await JsObjectReference.InvokeAsync<IJSObjectReference>("getPane");
			return new PaneApi<H>(JsRuntime, Parent, paneReference);
		}

		public ValueTask<ISeriesMarkersPluginApi<H>> CreateSeriesMarkers(IEnumerable<SeriesMarker<H>> markers)
			=> JsModule.CreateSeriesMarkers(JsRuntime, this, markers?.ToArray() ?? []);

		public ValueTask<ISeriesUpDownMarkerPluginApi<H>> CreateUpDownMarkers(UpDownMarkersPluginOptions options = null)
		{
			if (_SeriesType != Customization.Enums.SeriesType.Line && _SeriesType != Customization.Enums.SeriesType.Area)
				throw new InvalidOperationException("Method only available for Line and Area series");

			return JsModule.CreateUpDownMarkers(JsRuntime, this, options ?? new());
		}

		static async Task<Array> GetData<T>(IJSObjectReference jSObject)
			=> await jSObject.InvokeAsync<T[]>("data");
	}
}
