using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Models.Events;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor
{
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
					JsonOptionsObject.ValidateConverterAttribute(jsonOptions);
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
					JsonOptionsObject.ValidateConverterAttribute(jsonOptions);
#endif

			return jsRuntime.InvokeVoidAsync("lightweightChartsBlazor.lightweightChartsInvoke", javascriptArgs);
		}

		public static ValueTask<IJSObjectReference> CreateChartLayout(IJSRuntime jsRuntime, string id, ChartOptions options)
			=> jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.createChartLayout", id, options);

		public static ValueTask<string> GetUniqueJavascriptId(IJSRuntime jsRuntime, IJSObjectReference jSObject)
			=> jsRuntime.InvokeAsync<string>("lightweightChartsBlazor.getUniqueJavascriptId", jSObject);

		public static ValueTask<IJSObjectReference> SubscribeToEvent<T>(IJSRuntime jsRuntime, IJSObjectReference jSObject, DotNetObjectReference<T> dotnetRef, EventDescriptor descriptor, string handler)
			where T : class
			=> jsRuntime.InvokeAsync<IJSObjectReference>("lightweightChartsBlazor.subscribeToEvent", jSObject, dotnetRef, descriptor.SubscribeMethod, handler);

		public static ValueTask UnsubscribeFromEvent(IJSRuntime jsRuntime, IJSObjectReference jSObject, IJSObjectReference jSObjectCallback, EventDescriptor descriptor)
			=> jsRuntime.InvokeVoidAsync("lightweightChartsBlazor.unsubscribeFromEvent", jSObject, jSObjectCallback, descriptor.UnsubscribeMethod);

		public static ValueTask<byte[]> TakeScreenshot(IJSRuntime jsRuntime, IJSObjectReference chartReference)
			=> jsRuntime.InvokeAsync<byte[]>("lightweightChartsBlazor.takeScreenshot", chartReference);
	}
}
