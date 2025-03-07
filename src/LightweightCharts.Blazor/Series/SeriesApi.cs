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
using LightweightCharts.Blazor.Plugins;
using LightweightCharts.Blazor.Customization.Chart;

namespace LightweightCharts.Blazor.Series
{
	internal interface ISeriesApiInternal : ISeriesApi
	{
		IEnumerable<Type> AllowedDataItemTypes { get; }
	}

	internal class SeriesApi<O> : CustomizableObject<O>, ISeriesApi<O>, ISeriesApiInternal
		where O : SeriesOptionsCommon, new()
	{
		public SeriesApi(string uniqueId, IJSRuntime jsRuntime, IJSObjectReference jsObject, ChartComponent parent, SeriesType seriesType, params Type[] dataItemTypes)
			: base(jsRuntime, jsObject)
		{
			UniqueJavascriptId = uniqueId;
			Parent = parent;
			_AllowedDataItemTypes = dataItemTypes;
			_SeriesType = seriesType;
		}

		SeriesType _SeriesType;
		Type[] _AllowedDataItemTypes;
		List<IPriceLine> _PriceLines = new();
		IPriceScaleApi _PriceScale;

		public string UniqueJavascriptId { get; }

		public IEnumerable<IPriceLine> PriceLines
			=> _PriceLines;

		public IEnumerable<Type> AllowedDataItemTypes
			=> _AllowedDataItemTypes;

		public event EventHandler<DataChangedScope> DataChanged;

		public ChartComponent Parent { get; init; }

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
			DataChanged?.Invoke(this, DataChangedScope.Full);
		}

		public async Task Update(ISeriesData item)
		{
			await JsObjectReference.InvokeVoidAsync("update", item);
			DataChanged?.Invoke(this, DataChangedScope.Update);
		}

		public async Task<ISeriesData> DataByIndex(int logicalIndex, MismatchDirection? mismatchDirection)
		{
			return await JsObjectReference.InvokeAsync<ISeriesData>("dataByIndex", logicalIndex, mismatchDirection);
		}

		public async Task<IEnumerable<ISeriesData>> Data()
		{
			return await JsObjectReference.InvokeAsync<ISeriesData[]>("data");
		}

		public async Task<IPriceLine> CreatePriceLine(PriceLineOptions options)
		{
			var priceLineRef = await JsObjectReference.InvokeAsync<IJSObjectReference>("createPriceLine", options ?? new PriceLineOptions());
			var priceLine = new PriceLine(JsRuntime, priceLineRef, this);
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

		public async Task<IPaneApi> GetPane()
		{
			var paneReference = await JsObjectReference.InvokeAsync<IJSObjectReference>("getPane");
			return new PaneApi(JsRuntime, Parent, paneReference);
		}

		public ValueTask<ISeriesMarkersPluginApi> CreateSeriesMarkers(IEnumerable<SeriesMarker> markers)
			=> JsModule.CreateSeriesMarkers(JsRuntime, this, markers?.ToArray() ?? []);

		public ValueTask<ISeriesUpDownMarkerPluginApi> CreateUpDownMarkers(UpDownMarkersPluginOptions options = null)
		{
			if (_SeriesType != Customization.Enums.SeriesType.Line && _SeriesType != Customization.Enums.SeriesType.Area)
				throw new InvalidOperationException("Method only available for Line and Area series");

			return JsModule.CreateUpDownMarkers(JsRuntime, this, options ?? new());
		}
	}
}
