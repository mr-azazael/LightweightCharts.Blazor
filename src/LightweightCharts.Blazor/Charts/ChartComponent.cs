using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Models;
using LightweightCharts.Blazor.Models.Events;
using LightweightCharts.Blazor.Series;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Text.Json;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Charts
{
	public class ChartComponent : ComponentBase, IChartApi, IAsyncDisposable
	{
		public ChartComponent()
		{
			Id = Guid.NewGuid().ToString();
		}

		Dictionary<string, ISeriesApiInternal> _Series = new();
		IJSObjectReference _Layout;
		EventsHelper _EventsHelper;
		TaskCompletionSource _InitializationCompleted = new TaskCompletionSource();
		ITimeScaleApi _TimeScale;
		Dictionary<string, IPriceScaleApi> _PriceScales = new();

		/// <summary>
		/// Unique chart container id.
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Javascript chart object reference.
		/// </summary>
		public IJSObjectReference JsObjectReference
			=> _Layout;

		/// <summary>
		/// A task that completes when the chart is ready.
		/// </summary>
		public Task InitializationCompleted
			=> _InitializationCompleted.Task;

		#region events

		/// <summary>
		/// Get notified when a mouse clicks on a chart
		/// </summary>
		public event EventHandler<MouseEventParams> Clicked;

		/// <summary>
		/// Get notified when a mouse double clicks on a chart
		/// </summary>
		public event EventHandler<MouseEventParams> DoubleClicked;

		/// <summary>
		/// Get notified when a mouse moves on a chart
		/// </summary>
		public event EventHandler<MouseEventParams> CrosshairMoved;

		#endregion

		#region parameters

		[Inject]
		IJSRuntime JsRuntime { get; set; }

		/// <summary>
		/// Container div style. By default, its set to width: 100%, height: 100%
		/// </summary>
		[Parameter]
		public string CssStyle { get; set; } = "width: 100%; height: 100%;";

		#endregion

		MouseEventParams ParseMouseEventArgs(InternalMouseEventParams args)
		{
			var seriesPrice = new List<SeriesPrice>();
			foreach (var sp in args.SeriesData)
			{
				//find the api for the event id
				var seriesApi = _Series[sp.SeriesId];
				WhitespaceData priceData = null;

				switch (sp.SeriesType)
				{
					case SeriesType.Bar:
					case SeriesType.Candlestick:
						{
							priceData = JsonSerializer.Deserialize<OhlcData>(sp.DataItem);
							break;
						}
					case SeriesType.Area:
					case SeriesType.Line:
					case SeriesType.Baseline:
						{
							priceData = JsonSerializer.Deserialize<SingleValueData>(sp.DataItem);
							break;
						}
					case SeriesType.Histogram:
						{
							priceData = JsonSerializer.Deserialize<HistogramData>(sp.DataItem);
							break;
						}
					default:
						throw new NotImplementedException();
				}

				priceData.UnixTime = args.Time.GetValueOrDefault();
				seriesPrice.Add(new SeriesPrice
				{
					SeriesApi = seriesApi,
					DataItem = priceData
				});
			}

			ISeriesApi hoveredSeries = null;
			if (!string.IsNullOrEmpty(args.HoveredSeriesId))
				hoveredSeries = _Series[args.HoveredSeriesId];

			return new MouseEventParams
			{
				Point = args.Point,
				Time = args.Time,
				Logical = args.Logical,
				HoveredObjectId = args.HoveredObjectId,
				SeriesPrices = seriesPrice.ToArray(),
				HoveredSeries = hoveredSeries,
				SourceEvent = args.SourceEvent
			};
		}

		void OnClicked(object sender, InternalMouseEventParams args)
		{
			Clicked?.Invoke(this, ParseMouseEventArgs(args));
		}

		void OnDoubleClicked(object sender, InternalMouseEventParams args)
		{
			DoubleClicked?.Invoke(this, ParseMouseEventArgs(args));
		}

		void OnCrosshairMoved(object sender, InternalMouseEventParams args)
		{
			CrosshairMoved?.Invoke(this, ParseMouseEventArgs(args));
		}

		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			builder.OpenElement(1, "div");
			builder.AddAttribute(2, "id", Id);
			builder.AddAttribute(3, "style", CssStyle);
			builder.CloseElement();
		}

		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender)
			{
				_Layout = await JsRuntime.InvokeAsync<IJSObjectReference>("LightweightChartsBlazor.createChartLayout", Id, new ChartOptionsBase());
				_EventsHelper = new EventsHelper(this, JsRuntime);

				_EventsHelper.AddEvent<InternalMouseEventParams>(OnClicked, Events.Click);
				_EventsHelper.AddEvent<InternalMouseEventParams>(OnDoubleClicked, Events.DoubleClick);
				_EventsHelper.AddEvent<InternalMouseEventParams>(OnCrosshairMoved, Events.CrosshairMove);

				_InitializationCompleted.SetResult();
			}
		}

		public async Task<ChartOptionsBase> Options()
		{
			await InitializationCompleted;
			return await JavascriptInvokeAsync<ChartOptionsBase>(JsObjectReference, "options");
		}

		public async Task ApplyOptions(ChartOptionsBase options)
		{
			await InitializationCompleted;
			await JavascriptInvokeVoidAsync(_Layout, "applyOptions", options ?? new());
		}

		#region Api

		public async Task RemoveAsync()
		{
			await InitializationCompleted;
			await JavascriptInvokeVoidAsync(JsObjectReference, "remove");
			//reset status to not initialized?
			//_InitializationCompleted = new TaskCompletionSource();
		}

		public async Task ResizeAsync(double width, double height, bool repaint)
		{
			await InitializationCompleted;
			await JavascriptInvokeVoidAsync(JsObjectReference, "resize", width, height, repaint);
		}

		public async Task RemoveSeriesAsync(ISeriesApi series)
		{
			await InitializationCompleted;
			if (_Series.ContainsKey(series.UniqueJavascriptId))
			{
				await JavascriptInvokeVoidAsync(_Layout, "removeSeries", series.JsObjectReference);
				_Series.Remove(series.UniqueJavascriptId);
			}
		}

		public async Task<ISeriesApi<LineSeriesOptions>> AddLineSeriesAsync(LineSeriesOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addLineSeries", [options ?? new LineSeriesOptions()]);
			var uniqueId = await JsRuntime.InvokeAsync<string>("LightweightChartsBlazor.getUniqueJavascriptId", javascriptRef);
			var series = new SeriesApi<LineSeriesOptions>(uniqueId, javascriptRef, this, typeof(LineData));
			_Series.Add(uniqueId, series);
			return series;
		}

		public async Task<ISeriesApi<BaselineStyleOptions>> AddBaselineSeriesAsync(BaselineStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addBaselineSeries", [options ?? new BaselineStyleOptions()]);
			var uniqueId = await JsRuntime.InvokeAsync<string>("LightweightChartsBlazor.getUniqueJavascriptId", javascriptRef);
			var series = new SeriesApi<BaselineStyleOptions>(uniqueId, javascriptRef, this, typeof(BaselineData));
			_Series.Add(uniqueId, series);
			return series;
		}

		public async Task<ISeriesApi<CandlestickStyleOptions>> AddCandlestickSeriesAsync(CandlestickStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addCandlestickSeries", [options ?? new CandlestickStyleOptions()]);
			var uniqueId = await JsRuntime.InvokeAsync<string>("LightweightChartsBlazor.getUniqueJavascriptId", javascriptRef);
			var series = new SeriesApi<CandlestickStyleOptions>(uniqueId, javascriptRef, this, typeof(CandlestickData));
			_Series.Add(uniqueId, series);
			return series;
		}

		public async Task<ISeriesApi<AreaStyleOptions>> AddAreaSeriesAsync(AreaStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addAreaSeries", options ?? new AreaStyleOptions());
			var uniqueId = await JsRuntime.InvokeAsync<string>("LightweightChartsBlazor.getUniqueJavascriptId", javascriptRef);
			var series = new SeriesApi<AreaStyleOptions>(uniqueId, javascriptRef, this, typeof(AreaData));
			_Series.Add(uniqueId, series);
			return series;
		}

		public async Task<ISeriesApi<BarStyleOptions>> AddBarSeriesAsync(BarStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addBarSeries", [options ?? new BarStyleOptions()]);
			var uniqueId = await JsRuntime.InvokeAsync<string>("LightweightChartsBlazor.getUniqueJavascriptId", javascriptRef);
			var series = new SeriesApi<BarStyleOptions>(uniqueId, javascriptRef, this, typeof(BarData));
			_Series.Add(uniqueId, series);
			return series;
		}

		public async Task<ISeriesApi<HistogramStyleOptions>> AddHistogramSeriesAsync(HistogramStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addHistogramSeries", [options ?? new HistogramStyleOptions()]);
			var uniqueId = await JsRuntime.InvokeAsync<string>("LightweightChartsBlazor.getUniqueJavascriptId", javascriptRef);
			var series = new SeriesApi<HistogramStyleOptions>(uniqueId, javascriptRef, this, typeof(HistogramData));
			_Series.Add(uniqueId, series);
			return series;
		}

		public async Task<ITimeScaleApi> TimeScaleAsync()
		{
			await InitializationCompleted;
			if (_TimeScale == null)
			{
				var reference = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "timeScale");
				_TimeScale = new TimeScaleApi(this, reference, JsRuntime);
			}

			return _TimeScale;
		}

		public async Task<IPriceScaleApi> PriceScaleAsync(string id)
		{
			await InitializationCompleted;
			if (!_PriceScales.ContainsKey(id))
			{
				var priceScale = await JavascriptInvokeAsync<IPriceScaleApi>(JsObjectReference, "priceScale", id);
				if (priceScale != null)
					_PriceScales[id] = priceScale;
				else
					return null;
			}

			return _PriceScales[id];
		}

		public async Task<byte[]> TakeScreenshotAsync()
			=> await JsRuntime.InvokeAsync<byte[]>("LightweightChartsBlazor.takeScreenshot", JsObjectReference);

		public async Task<PaneSize> PaneSize()
		{
			await InitializationCompleted;
			return await JavascriptInvokeAsync<PaneSize>(JsObjectReference, "paneSize");
		}

		public async Task<bool> AutoSizeActiveAsync()
		{
			await InitializationCompleted;
			return await JavascriptInvokeAsync<bool>(JsObjectReference, "autoSizeActive");
		}

		public async Task SetCrosshairPosition(double price, long horizontalPosition, ISeriesApi series)
		{
			await InitializationCompleted;
			await JavascriptInvokeVoidAsync(_Layout, "setCrosshairPosition", price, horizontalPosition, series.JsObjectReference);
		}

		public async Task ClearCrosshairPosition()
		{
			await InitializationCompleted;
			await JavascriptInvokeVoidAsync(_Layout, "clearCrosshairPosition");
		}

		#endregion

		#region Javascript module exposure

		async Task<T> JavascriptInvokeAsync<T>(IJSObjectReference target, string methodName, params object[] args)
		{
			var javascriptArgs = new List<object> { target, methodName };
			if (args?.Length > 0)
				javascriptArgs.AddRange(args);

			return await JsRuntime.InvokeAsync<T>("LightweightChartsBlazor.lightweightChartsInvoke", javascriptArgs.ToArray());
		}

		async Task JavascriptInvokeVoidAsync(IJSObjectReference target, string methodName, params object[] args)
		{
			var javascriptArgs = new List<object> { target, methodName };
			if (args?.Length > 0)
				javascriptArgs.AddRange(args);

			await JsRuntime.InvokeVoidAsync("LightweightChartsBlazor.lightweightChartsInvoke", javascriptArgs.ToArray());
		}

		#endregion

		public async ValueTask DisposeAsync()
		{
			if (_EventsHelper != null)
			{
				await _EventsHelper.DisposeAsync();
				_EventsHelper = null;
			}
		}
	}
}
