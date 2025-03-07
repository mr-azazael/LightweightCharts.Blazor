using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models.Events;
using LightweightCharts.Blazor.Plugins;
using LightweightCharts.Blazor.Series;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor
{
	/// <summary>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api#functions"/>
	/// </summary>
	static class JsModule
	{
		public static ValueTask<T> InvokeAsync<T>(IJSRuntime jsRuntime, IJSObjectReference target, string methodName)
			=> InvokeAsync<T>(jsRuntime, target, methodName, false, null);

		public static ValueTask<T> InvokeAsync<T>(IJSRuntime jsRuntime, IJSObjectReference target, string methodName, bool replaceDelegates, params object[] args)
		{
			var javascriptArgs = new object[3 + (args?.Length ?? 0)];
			javascriptArgs[0] = target;
			javascriptArgs[1] = methodName;
			javascriptArgs[2] = replaceDelegates;

			if (args?.Length > 0)
				args.CopyTo(javascriptArgs, 3);

#if DEBUG
			foreach (var arg in javascriptArgs)
				if (arg is JsonOptionsObject jsonOptions)
					JsonOptionsObject.ValidateConverterAttribute(jsonOptions.GetType());
#endif

			return jsRuntime.InvokeAsync<T>("lightweightChartsBlazor.lightweightChartsInvoke", javascriptArgs);
		}

		public static ValueTask InvokeVoidAsync(IJSRuntime jsRuntime, IJSObjectReference target, string methodName)
			=> InvokeVoidAsync(jsRuntime, target, methodName, false, null);

		public static ValueTask InvokeVoidAsync(IJSRuntime jsRuntime, IJSObjectReference target, string methodName, bool replaceDelegates, params object[] args)
		{
			var javascriptArgs = new object[3 + (args?.Length ?? 0)];
			javascriptArgs[0] = target;
			javascriptArgs[1] = methodName;
			javascriptArgs[2] = replaceDelegates;

			if (args?.Length > 0)
				args.CopyTo(javascriptArgs, 3);

#if DEBUG
			foreach (var arg in javascriptArgs)
				if (arg is JsonOptionsObject jsonOptions)
					JsonOptionsObject.ValidateConverterAttribute(jsonOptions.GetType());
#endif

			return jsRuntime.InvokeVoidAsync("lightweightChartsBlazor.lightweightChartsInvoke", javascriptArgs);
		}

		public static ValueTask<string> GetUniqueJavascriptId(IJSRuntime jsRuntime, IJSObjectReference jSObject)
			=> jsRuntime.InvokeAsync<string>("lightweightChartsBlazor.getUniqueJavascriptId", jSObject);

		public static ValueTask<IJSObjectReference> SubscribeToEvent<T>(IJSRuntime jsRuntime, IJSObjectReference jSObject, DotNetObjectReference<T> dotnetRef, EventDescriptor descriptor, string handler)
			where T : class
			=> jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.subscribeToEvent", jSObject, dotnetRef, descriptor.SubscribeMethod, handler);

		public static ValueTask UnsubscribeFromEvent(IJSRuntime jsRuntime, IJSObjectReference jSObject, IJSObjectReference jSObjectCallback, EventDescriptor descriptor)
			=> jsRuntime.InvokeVoidAsync("lightweightChartsBlazor.unsubscribeFromEvent", jSObject, jSObjectCallback, descriptor.UnsubscribeMethod);

		public static ValueTask<byte[]> TakeScreenshot(IJSRuntime jsRuntime, IJSObjectReference chartReference)
			=> jsRuntime.InvokeAsync<byte[]>("lightweightChartsBlazor.takeScreenshot", chartReference);

		public static ValueTask<IJSObjectReference> AddChartSeries(IJSRuntime jsRuntime, IJSObjectReference chartReference, SeriesType type, SeriesOptionsCommon options)
			=> jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.addSeries", chartReference, type, options);

		public static ValueTask<string[]> GetPaneSeries(IJSRuntime jsRuntime, IJSObjectReference paneReference)
			=> jsRuntime.InvokeAsync<string[]>("lightweightChartsBlazor.getPaneSeries", paneReference);

		#region charts api methods

		public static ValueTask<IJSObjectReference> CreateChart(IJSRuntime jsRuntime, string id, ChartOptions options)
			=> jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createChartLayout", id, options);

		public static async ValueTask<IImageWatermarkPluginApi> CreateImageWatermark(IJSRuntime jsRuntime, IPaneApi pane, string url, ImageWatermarkOptions options)
		{
			var api = await jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createImageWatermark", pane.JsObjectReference, url, options);
			return new ImageWatermarkPluginApi(jsRuntime, pane, api, options);
		}

		public static async ValueTask<ISeriesMarkersPluginApi> CreateSeriesMarkers(IJSRuntime jsRuntime, ISeriesApi series, SeriesMarker[] markers)
		{
			var api = await jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createSeriesMarkers", series.JsObjectReference, markers);
			return new SeriesMarkersPluginApi(jsRuntime, series, api, markers);
		}

		public static async ValueTask<ITextWatermarkPluginApi> CreateTextWatermark(IJSRuntime jsRuntime, IPaneApi pane, TextWatermarkOptions options)
		{
			var api = await jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createTextWatermark", pane.JsObjectReference, options);
			return new TextWatermarkPluginApi(jsRuntime, pane, api, options);
		}

		public static async ValueTask<ISeriesUpDownMarkerPluginApi> CreateUpDownMarkers(IJSRuntime jsRuntime, ISeriesApi series, UpDownMarkersPluginOptions options)
		{
			var api = await jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createUpDownMarkers", series.JsObjectReference, options);
			return new SeriesUpDownMarkerPluginApi(jsRuntime, api, series);
		}

		public static ValueTask<string> Version(IJSRuntime jsRuntime)
			=> jsRuntime.InvokeAsync<string>("lightweightChartsBlazor.version");

		#endregion
	}
}
