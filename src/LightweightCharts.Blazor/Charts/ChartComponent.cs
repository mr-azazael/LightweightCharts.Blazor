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
	/// <summary>
	/// implementation for <see cref="IChartApi"/>
	/// </summary>
	public class ChartComponent : ComponentBase, IChartApi, IAsyncDisposable
	{
		/// <summary>
		/// default constructor
		/// </summary>
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

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="builder"><inheritdoc/></param>
		protected override void BuildRenderTree(RenderTreeBuilder builder)
		{
			builder.OpenElement(1, "div");
			builder.AddAttribute(2, "id", Id);
			builder.AddAttribute(3, "style", CssStyle);
			builder.CloseElement();
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="firstRender"><inheritdoc/></param>
		/// <returns><inheritdoc/></returns>
		protected override async Task OnAfterRenderAsync(bool firstRender)
		{
			await base.OnAfterRenderAsync(firstRender);

			if (firstRender)
			{
				_Layout = await JsModule.CreateChartLayout(JsRuntime, Id, new ChartOptions());
				_EventsHelper = new EventsHelper(this, JsRuntime);

				_EventsHelper.AddEvent<InternalMouseEventParams>(OnClicked, Events.Click);
				_EventsHelper.AddEvent<InternalMouseEventParams>(OnDoubleClicked, Events.DoubleClick);
				_EventsHelper.AddEvent<InternalMouseEventParams>(OnCrosshairMoved, Events.CrosshairMove);

				_InitializationCompleted.SetResult();
			}
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.Options"/>
		/// </summary>
		public async Task<ChartOptions> Options()
		{
			await InitializationCompleted;
			return await JsModule.InvokeAsync<ChartOptions>(JsRuntime, JsObjectReference, "options", false);
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.ApplyOptions"/>
		/// </summary>
		public async Task ApplyOptions(ChartOptions options)
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, _Layout, "applyOptions", true, options ?? new());
		}

		#region Api

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.RemoveAsync"/>
		/// </summary>
		public async Task RemoveAsync()
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "remove", false);
			//reset status to not initialized?
			//_InitializationCompleted = new TaskCompletionSource();
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.ResizeAsync"/>
		/// </summary>
		public async Task ResizeAsync(double width, double height, bool repaint)
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "resize", false, width, height, repaint);
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.RemoveSeriesAsync"/>
		/// </summary>
		public async Task RemoveSeriesAsync(ISeriesApi series)
		{
			await InitializationCompleted;
			if (_Series.ContainsKey(series.UniqueJavascriptId))
			{
				await JsModule.InvokeVoidAsync(JsRuntime, _Layout, "removeSeries", false, series.JsObjectReference);
				_Series.Remove(series.UniqueJavascriptId);
			}
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.AddLineSeriesAsync"/>
		/// </summary>
		public async Task<ISeriesApi<LineSeriesOptions>> AddLineSeriesAsync(LineSeriesOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, _Layout, "addLineSeries", true, [options ?? new LineSeriesOptions()]);
			var uniqueId = await JsModule.GetUniqueJavascriptId(JsRuntime, javascriptRef);
			var series = new SeriesApi<LineSeriesOptions>(uniqueId, JsRuntime, javascriptRef, this, typeof(LineData));
			_Series.Add(uniqueId, series);
			return series;
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.AddBaselineSeriesAsync"/>
		/// </summary>
		public async Task<ISeriesApi<BaselineStyleOptions>> AddBaselineSeriesAsync(BaselineStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, _Layout, "addBaselineSeries", true, [options ?? new BaselineStyleOptions()]);
			var uniqueId = await JsModule.GetUniqueJavascriptId(JsRuntime, javascriptRef);
			var series = new SeriesApi<BaselineStyleOptions>(uniqueId, JsRuntime, javascriptRef, this, typeof(BaselineData));
			_Series.Add(uniqueId, series);
			return series;
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.AddCandlestickSeriesAsync"/>
		/// </summary>
		public async Task<ISeriesApi<CandlestickStyleOptions>> AddCandlestickSeriesAsync(CandlestickStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, _Layout, "addCandlestickSeries", true, [options ?? new CandlestickStyleOptions()]);
			var uniqueId = await JsModule.GetUniqueJavascriptId(JsRuntime, javascriptRef);
			var series = new SeriesApi<CandlestickStyleOptions>(uniqueId, JsRuntime, javascriptRef, this, typeof(CandlestickData));
			_Series.Add(uniqueId, series);
			return series;
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.AddAreaSeriesAsync"/>
		/// </summary>
		public async Task<ISeriesApi<AreaStyleOptions>> AddAreaSeriesAsync(AreaStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, _Layout, "addAreaSeries", true, options ?? new AreaStyleOptions());
			var uniqueId = await JsModule.GetUniqueJavascriptId(JsRuntime, javascriptRef);
			var series = new SeriesApi<AreaStyleOptions>(uniqueId, JsRuntime, javascriptRef, this, typeof(AreaData));
			_Series.Add(uniqueId, series);
			return series;
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.AddBarSeriesAsync"/>
		/// </summary>
		public async Task<ISeriesApi<BarStyleOptions>> AddBarSeriesAsync(BarStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, _Layout, "addBarSeries", true, [options ?? new BarStyleOptions()]);
			var uniqueId = await JsModule.GetUniqueJavascriptId(JsRuntime, javascriptRef);
			var series = new SeriesApi<BarStyleOptions>(uniqueId, JsRuntime, javascriptRef, this, typeof(BarData));
			_Series.Add(uniqueId, series);
			return series;
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.AddHistogramSeriesAsync"/>
		/// </summary>
		public async Task<ISeriesApi<HistogramStyleOptions>> AddHistogramSeriesAsync(HistogramStyleOptions options = null)
		{
			await InitializationCompleted;
			var javascriptRef = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, _Layout, "addHistogramSeries", true, [options ?? new HistogramStyleOptions()]);
			var uniqueId = await JsModule.GetUniqueJavascriptId(JsRuntime, javascriptRef);
			var series = new SeriesApi<HistogramStyleOptions>(uniqueId, JsRuntime, javascriptRef, this, typeof(HistogramData));
			_Series.Add(uniqueId, series);
			return series;
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.TimeScaleAsync"/>
		/// </summary>
		public async Task<ITimeScaleApi> TimeScaleAsync()
		{
			await InitializationCompleted;
			if (_TimeScale == null)
			{
				var reference = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, _Layout, "timeScale", false);
				_TimeScale = new TimeScaleApi(this, reference, JsRuntime);
			}

			return _TimeScale;
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.PriceScaleAsync"/>
		/// </summary>
		public async Task<IPriceScaleApi> PriceScaleAsync(string id)
		{
			await InitializationCompleted;
			if (!_PriceScales.ContainsKey(id))
			{
				var priceScale = await JsModule.InvokeAsync<IPriceScaleApi>(JsRuntime, JsObjectReference, "priceScale", false, id);
				if (priceScale != null)
					_PriceScales[id] = priceScale;
				else
					return null;
			}

			return _PriceScales[id];
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.TakeScreenshotAsync"/>
		/// </summary>
		public async Task<byte[]> TakeScreenshotAsync()
			=> await JsModule.TakeScreenshot(JsRuntime, JsObjectReference);

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.PaneSize"/>
		/// </summary>
		public async Task<PaneSize> PaneSize()
		{
			await InitializationCompleted;
			return await JsModule.InvokeAsync<PaneSize>(JsRuntime, JsObjectReference, "paneSize", false);
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.AutoSizeActiveAsync"/>
		/// </summary>
		public async Task<bool> AutoSizeActiveAsync()
		{
			await InitializationCompleted;
			return await JsModule.InvokeAsync<bool>(JsRuntime, JsObjectReference, "autoSizeActive");
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.SetCrosshairPosition"/>
		/// </summary>
		public async Task SetCrosshairPosition(double price, long horizontalPosition, ISeriesApi series)
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, _Layout, "setCrosshairPosition", false, price, horizontalPosition, series.JsObjectReference);
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.ClearCrosshairPosition"/>
		/// </summary>
		public async Task ClearCrosshairPosition()
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, _Layout, "clearCrosshairPosition", false);
		}

		#endregion

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <returns><inheritdoc/></returns>
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
