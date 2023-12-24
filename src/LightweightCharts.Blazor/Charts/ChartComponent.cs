using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Customization.Series;
using LightweightCharts.Blazor.DataItems;
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
	/// <summary>
	/// The main interface of a single chart.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi
	/// </summary>
	public interface IChartApi : ICustomizableObject<ChartOptions>
	{
		/// <summary>
		/// Chart clicked event.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#subscribeclick
		/// </summary>
		event EventHandler<MouseEventArgs> Clicked;

		/// <summary>
		/// Crosshair moved event.
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#subscribecrosshairmove
		/// </summary>
		event EventHandler<MouseEventArgs> CrosshairMoved;

		/// <summary>
		/// Removes the chart object including all DOM elements. This is an irreversible operation, you cannot do anything with the chart after removing it.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#remove
		/// </summary>
		Task RemoveAsync();

		/// <summary>
		/// Sets fixed size of the chart. By default chart takes up 100% of its container.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#resize
		/// </summary>
		/// <param name="width">Target width of the chart.</param>
		/// <param name="height">Target height of the chart.</param>
		/// <param name="repaint">True to initiate resize immediately. One could need this to get screenshot immediately after resize.</param>
		Task ResizeAsync(double width, double height, bool repaint);

		/// <summary>
		/// Creates an area series with specified parameters.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addareaseries
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<AreaStyleOptions>> AddAreaSeriesAsync(AreaStyleOptions options = null);

		/// <summary>
		/// Creates a baseline series with specified parameters.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addbaselineseries
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<BaselineStyleOptions>> AddBaselineSeriesAsync(BaselineStyleOptions options = null);

		/// <summary>
		/// Creates a bar series with specified parameters.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addbarseries
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<BarSeriesOptions>> AddBarSeriesAsync(BarSeriesOptions options = null);

		/// <summary>
		/// Creates a candlestick series with specified parameters.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addcandlestickseries
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<CandlestickStyleOptions>> AddCandlestickSeriesAsync(CandlestickStyleOptions options = null);

		/// <summary>
		/// Creates a histogram series with specified parameters.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addhistogramseries
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<HistogramStyleOptions>> AddHistogramSeriesAsync(HistogramStyleOptions options = null);

		/// <summary>
		/// Creates a line series with specified parameters.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#addlineseries
		/// </summary>
		/// <param name="options">Customization parameters of the series being created.</param>
		/// <returns>An interface of the created series.</returns>
		Task<ISeriesApi<LineSeriesOptions>> AddLineSeriesAsync(LineSeriesOptions options = null);

		/// <summary>
		/// Removes a series of any type. This is an irreversible operation, you cannot do anything with the series after removing it.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#removeseries
		/// </summary>
		/// <param name="series">The series to be removed.</param>
		Task RemoveSeriesAsync(ISeriesApi series);

		/// <summary>
		/// Returns API to manipulate a price scale.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#pricescale
		/// </summary>
		/// <param name="id">ID of the price scale.</param>
		/// <returns>Price scale API.</returns>
		Task<IPriceScaleApi> PriceScaleAsync(string id);

		/// <summary>
		/// Returns API to manipulate the time scale.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#timescale
		/// </summary>
		/// <returns>Time scale API</returns>
		Task<ITimeScaleApi> TimeScaleAsync();

		/// <summary>
		/// Make a screenshot of the chart with all the elements excluding crosshair.<br/>
		/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IChartApi#takescreenshot
		/// </summary>
		/// <returns>Returns the image bytes.</returns>
		Task<byte[]> TakeScreenshotAsync();
	}

	public class ChartComponent : ComponentBase, IChartApi, IAsyncDisposable
	{
		public ChartComponent()
		{
			Id = Guid.NewGuid().ToString();
			_DotNetObjectReference = DotNetObjectReference.Create(this);
		}

		DotNetObjectReference<ChartComponent> _DotNetObjectReference;
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
		public IJSObjectReference JsObjectReference { get => _Layout; }

		/// <summary>
		/// A task that completes when the chart is ready.
		/// </summary>
		public Task InitializationCompleted => _InitializationCompleted.Task;

		#region events

		/// <summary>
		/// Get notified when a mouse clicks on a chart
		/// </summary>
		public event EventHandler<MouseEventArgs> Clicked;

		/// <summary>
		/// Get notified when a mouse moves on a chart
		/// </summary>
		public event EventHandler<MouseEventArgs> CrosshairMoved;

		#endregion

		#region parameters

		[Inject]
		IJSRuntime JsRuntime { get; set; }

		/// <summary>
		/// Container div style. By default, its set to width: 100%, height: 100%
		/// </summary>
		[Parameter]
		public string CssStyle { get; set; } = "width: 100%; height: 100%;";

		/// <summary>
		/// Auto-resizes the chart to the available container space <br/>
		/// Set this to false to control the width/height of the chart with <see cref="ApplyOptions"/>
		/// </summary>
		[Parameter]
		public bool AutoResize { get; set; } = true;

		#endregion

		MouseEventArgs ParseMouseEventArgs(InternalMouseEventArgs args)
		{
			var seriesPrice = new List<SeriesPrice>();
			foreach (var sp in args.SeriesPrices)
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
							priceData = new SingleValueData { Value = JsonSerializer.Deserialize<double>(sp.DataItem) };
							break;
						}
					case SeriesType.Histogram:
						{
							priceData = new HistogramData { Value = JsonSerializer.Deserialize<double>(sp.DataItem) };
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

			return new MouseEventArgs
			{
				Point = args.Point,
				Time = args.Time,
				HoveredMarkerId = args.HoveredMarkerId,
				SeriesPrices = seriesPrice,
				HoveredSeries = hoveredSeries
			};
		}

		void OnClicked(object sender, InternalMouseEventArgs args)
		{
			Clicked?.Invoke(this, ParseMouseEventArgs(args));
		}

		void OnCrosshairMoved(object sender, InternalMouseEventArgs args)
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
				await JsRuntime.InvokeVoidAsync("LightweightChartsBlazor.registerSizeChangedEvent", Id, _DotNetObjectReference, nameof(OnContainerSizeChanged));
				_Layout = await JsRuntime.InvokeAsync<IJSObjectReference>("LightweightChartsBlazor.createChartLayout", Id, new ChartOptions());
				_EventsHelper = new EventsHelper(this, JsRuntime);

				_EventsHelper.AddEvent<InternalMouseEventArgs>(OnClicked, Events.Click);
				_EventsHelper.AddEvent<InternalMouseEventArgs>(OnCrosshairMoved, Events.CrosshairMove);

				_InitializationCompleted.SetResult();
			}
		}

		public async Task<ChartOptions> Options()
		{
			await InitializationCompleted;
			return await JavascriptInvokeAsync<ChartOptions>(JsObjectReference, "options");
		}

		public async Task ApplyOptions(ChartOptions options)
		{
			await InitializationCompleted;
			await JavascriptInvokeVoidAsync(_Layout, "applyOptions", options ?? new());
		}

		#region Javascript events

		[JSInvokable(nameof(OnContainerSizeChanged))]
		public async void OnContainerSizeChanged(double width, double height)
		{
			if (AutoResize)
			{
				await InitializationCompleted;
				await JsRuntime.InvokeVoidAsync("LightweightChartsBlazor.lightweightChartsInvoke", _Layout, "resize", Math.Floor(width), Math.Floor(height), true);
			}
		}

		#endregion

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
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addLineSeries", new object[] { options ?? new LineSeriesOptions() });
			var uniqueId = await JsRuntime.InvokeAsync<string>("LightweightChartsBlazor.getUniqueJavascriptId", javascriptRef);
			var series = new SeriesApi<LineSeriesOptions>(uniqueId, javascriptRef, this, typeof(SingleValueData));
			_Series.Add(uniqueId, series);
			return series;
		}

		public async Task<ISeriesApi<BaselineStyleOptions>> AddBaselineSeriesAsync(BaselineStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addBaselineSeries", new object[] { options ?? new BaselineStyleOptions() });
			var uniqueId = await JsRuntime.InvokeAsync<string>("LightweightChartsBlazor.getUniqueJavascriptId", javascriptRef);
			var series = new SeriesApi<BaselineStyleOptions>(uniqueId, javascriptRef, this, typeof(SingleValueData));
			_Series.Add(uniqueId, series);
			return series;
		}

		public async Task<ISeriesApi<CandlestickStyleOptions>> AddCandlestickSeriesAsync(CandlestickStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addCandlestickSeries", new object[] { options ?? new CandlestickStyleOptions() });
			var uniqueId = await JsRuntime.InvokeAsync<string>("LightweightChartsBlazor.getUniqueJavascriptId", javascriptRef);
			var series = new SeriesApi<CandlestickStyleOptions>(uniqueId, javascriptRef, this, typeof(OhlcData));
			_Series.Add(uniqueId, series);
			return series;
		}

		public async Task<ISeriesApi<AreaStyleOptions>> AddAreaSeriesAsync(AreaStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addAreaSeries", new object[] { options ?? new AreaStyleOptions() });
			var uniqueId = await JsRuntime.InvokeAsync<string>("LightweightChartsBlazor.getUniqueJavascriptId", javascriptRef);
			var series = new SeriesApi<AreaStyleOptions>(uniqueId, javascriptRef, this, typeof(SingleValueData));
			_Series.Add(uniqueId, series);
			return series;
		}

		public async Task<ISeriesApi<BarSeriesOptions>> AddBarSeriesAsync(BarSeriesOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addBarSeries", new object[] { options ?? new BarSeriesOptions() });
			var uniqueId = await JsRuntime.InvokeAsync<string>("LightweightChartsBlazor.getUniqueJavascriptId", javascriptRef);
			var series = new SeriesApi<BarSeriesOptions>(uniqueId, javascriptRef, this, typeof(BarData));
			_Series.Add(uniqueId, series);
			return series;
		}

		public async Task<ISeriesApi<HistogramStyleOptions>> AddHistogramSeriesAsync(HistogramStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JavascriptInvokeAsync<IJSObjectReference>(_Layout, "addHistogramSeries", new object[] { options ?? new HistogramStyleOptions() });
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
