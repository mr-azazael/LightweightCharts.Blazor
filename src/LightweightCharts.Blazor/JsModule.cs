using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
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

		public static ValueTask<string> GetUniqueJavascriptId(IJSRuntime jsRuntime, IJSObjectReference jsObject)
			=> jsRuntime.InvokeAsync<string>("lightweightChartsBlazor.getUniqueJavascriptId", jsObject);

		public static ValueTask<IJSObjectReference> SubscribeToEvent<T>(IJSRuntime jsRuntime, IJSObjectReference jsObject, DotNetObjectReference<T> dotnetRef, EventDescriptor descriptor, string handler)
			where T : class
			=> jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.subscribeToEvent", jsObject, dotnetRef, descriptor.SubscribeMethod, handler);

		public static ValueTask UnsubscribeFromEvent(IJSRuntime jsRuntime, IJSObjectReference jsObject, IJSObjectReference jSObjectCallback, EventDescriptor descriptor)
			=> jsRuntime.InvokeVoidAsync("lightweightChartsBlazor.unsubscribeFromEvent", jsObject, jSObjectCallback, descriptor.UnsubscribeMethod);

		public static ValueTask<byte[]> TakeScreenshot(IJSRuntime jsRuntime, IJSObjectReference chartReference)
			=> jsRuntime.InvokeAsync<byte[]>("lightweightChartsBlazor.takeScreenshot", chartReference);

		public static ValueTask<IJSObjectReference> AddChartSeries<T>(IJSRuntime jsRuntime, IJSObjectReference chartReference, T type, SeriesOptionsCommon options, int paneIndex)
			=> jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.addSeries", chartReference, type, options, paneIndex);

		public static ValueTask<string[]> GetPaneSeries(IJSRuntime jsRuntime, IJSObjectReference paneReference)
			=> jsRuntime.InvokeAsync<string[]>("lightweightChartsBlazor.getPaneSeries", paneReference);

		#region charts api methods

		public static ValueTask<IJSObjectReference> CreateChart(IJSRuntime jsRuntime, string id, ChartOptions options)
			=> jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createChart", id, options);

		public static ValueTask<IJSObjectReference> CreateYieldCurveChart(IJSRuntime jsRuntime, string id, YieldCurveChartOptions options)
			=> jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createYieldCurveChart", id, options);

		public static ValueTask<IJSObjectReference> CreateOptionsChart(IJSRuntime jsRuntime, string id, PriceChartOptions options)
			=> jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createOptionsChart", id, options);

		public static async ValueTask<IImageWatermarkPluginApi<H>> CreateImageWatermark<H>(IJSRuntime jsRuntime, IPaneApi<H> pane, string url, ImageWatermarkOptions options)
			where H : struct
		{
			var api = await jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createImageWatermark", pane.JsObjectReference, url, options);
			return new ImageWatermarkPluginApi<H>(jsRuntime, pane, api, options);
		}

		public static async ValueTask<ISeriesMarkersPluginApi<H>> CreateSeriesMarkers<H, M>(IJSRuntime jsRuntime, ISeriesApi<H> series, M[] markers)
			where H : struct
			where M : SeriesMarkerBase<H>
		{
			var api = await jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createSeriesMarkers", series.JsObjectReference, markers);
			return new SeriesMarkersPluginApi<H>(jsRuntime, series, api, markers);
		}

		public static async ValueTask<ITextWatermarkPluginApi<H>> CreateTextWatermark<H>(IJSRuntime jsRuntime, IPaneApi<H> pane, TextWatermarkOptions options)
			where H : struct
		{
			var api = await jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createTextWatermark", pane.JsObjectReference, options);
			return new TextWatermarkPluginApi<H>(jsRuntime, pane, api, options);
		}

		public static async ValueTask<ISeriesUpDownMarkerPluginApi<H>> CreateUpDownMarkers<H>(IJSRuntime jsRuntime, ISeriesApi<H> series, UpDownMarkersPluginOptions options)
			where H : struct
		{
			var api = await jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createUpDownMarkers", series.JsObjectReference, options);
			return new SeriesUpDownMarkerPluginApi<H>(jsRuntime, api, series);
		}

		public static ValueTask<string> Version(IJSRuntime jsRuntime)
			=> jsRuntime.InvokeAsync<string>("lightweightChartsBlazor.version");

		#endregion
	}
}
