using LightweightCharts.Blazor.Customization;
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
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Charts
{
	/// <summary>
	/// implementation for <see cref="IChartApiBase{H}"/>
	/// </summary>
	public abstract class ChartComponentBase<H, O> : ComponentBase, IChartApiBase<H>, IJsObjectWrapper, IAsyncDisposable
		where H : struct
		where O : class, new()
	{
		/// <summary>
		/// default constructor
		/// </summary>
		public ChartComponentBase()
		{
			Id = Guid.NewGuid().ToString();
		}

		Dictionary<string, ISeriesApiInternal<H>> _Series = new();
		IJSObjectReference _JsObjectReference;
		EventsHelper _EventsHelper;
		TaskCompletionSource _InitializationCompleted = new();
		ITimeScaleApi<H> _TimeScale;
		Dictionary<string, IPriceScaleApi> _PriceScales = new();

		/// <summary>
		/// Unique chart container id.
		/// </summary>
		public string Id { get; }

		/// <summary>
		/// Javascript chart object reference.
		/// </summary>
		public IJSObjectReference JsObjectReference
			=> _JsObjectReference;

		/// <summary>
		/// A task that completes when the chart is ready.
		/// </summary>
		public Task InitializationCompleted
			=> _InitializationCompleted.Task;

		#region events

		/// <summary>
		/// Get notified when a mouse clicks on a chart
		/// </summary>
		public event EventHandler<MouseEventParams<H>> Clicked;

		/// <summary>
		/// Get notified when a mouse double clicks on a chart
		/// </summary>
		public event EventHandler<MouseEventParams<H>> DoubleClicked;

		/// <summary>
		/// Get notified when a mouse moves on a chart
		/// </summary>
		public event EventHandler<MouseEventParams<H>> CrosshairMoved;

		#endregion

		#region parameters

		/// <summary>
		/// Injected JS dispatcher.
		/// </summary>
		[Inject]
		protected IJSRuntime JsRuntime { get; set; }

		/// <summary>
		/// Container div style. By default, its set to width: 100%, height: 100%
		/// </summary>
		[Parameter]
		public string CssStyle { get; set; } = "width: 100%; height: 100%;";

		#endregion

		/// <summary>
		/// used by the events to deserialize price data into series-specific data points
		/// </summary>
		/// <param name="args"></param>
		/// <returns></returns>
		/// <exception cref="NotImplementedException"></exception>
		protected virtual MouseEventParams<H> ParseMouseEventArgs(InternalMouseEventParams<H> args)
		{
			var seriesPrice = new List<SeriesPrice<H>>();
			foreach (var sp in args.SeriesData)
			{
				//find the api for the event id
				var seriesApi = _Series[sp.SeriesId];
				WhitespaceData<H> priceData = null;

				switch (sp.SeriesType)
				{
					case SeriesType.Bar:
					case SeriesType.Candlestick:
						{
							priceData = JsonSerializer.Deserialize<OhlcData<H>>(sp.DataItem);
							break;
						}
					case SeriesType.Area:
					case SeriesType.Line:
					case SeriesType.Baseline:
						{
							priceData = JsonSerializer.Deserialize<SingleValueData<H>>(sp.DataItem);
							break;
						}
					case SeriesType.Histogram:
						{
							priceData = JsonSerializer.Deserialize<HistogramData<H>>(sp.DataItem);
							break;
						}
					default:
						throw new NotImplementedException();
				}

				priceData.Time = args.Time.GetValueOrDefault();
				seriesPrice.Add(new SeriesPrice<H>
				{
					SeriesApi = seriesApi,
					DataItem = priceData
				});
			}

			ISeriesApi<H> hoveredSeries = null;
			if (!string.IsNullOrEmpty(args.HoveredSeriesId))
				hoveredSeries = _Series[args.HoveredSeriesId];

			return new MouseEventParams<H>
			{
				Point = args.Point,
				Time = args.Time,
				Logical = args.Logical,
				HoveredObjectId = args.HoveredObjectId,
				SeriesPrices = [.. seriesPrice],
				HoveredSeries = hoveredSeries,
				SourceEvent = args.SourceEvent
			};
		}

		void OnClicked(object sender, InternalMouseEventParams<H> args)
		{
			Clicked?.Invoke(this, ParseMouseEventArgs(args));
		}

		void OnDoubleClicked(object sender, InternalMouseEventParams<H> args)
		{
			DoubleClicked?.Invoke(this, ParseMouseEventArgs(args));
		}

		void OnCrosshairMoved(object sender, InternalMouseEventParams<H> args)
		{
			CrosshairMoved?.Invoke(this, ParseMouseEventArgs(args));
		}

		/// <summary>
		/// Responsable to create the chart component.
		/// </summary>
		/// <returns>A reference to the js chart component.</returns>
		protected abstract ValueTask<IJSObjectReference> CreateChart();

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
				_JsObjectReference = await CreateChart();
				_EventsHelper = new EventsHelper(this, JsRuntime);
				_EventsHelper.AddEvent<InternalMouseEventParams<H>>(OnClicked, Events.Click);
				_EventsHelper.AddEvent<InternalMouseEventParams<H>>(OnDoubleClicked, Events.DoubleClick);
				_EventsHelper.AddEvent<InternalMouseEventParams<H>>(OnCrosshairMoved, Events.CrosshairMove);
				_InitializationCompleted.SetResult();
			}
		}

		#region Api

		/// <summary>
		/// <inheritdoc cref="IChartApiBase{H}.Remove"/>
		/// </summary>
		public async Task Remove()
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "remove", false);
			//reset status to not initialized?
			//_InitializationCompleted = new TaskCompletionSource();
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase{H}.Resize"/>
		/// </summary>
		public async Task Resize(double width, double height, bool repaint)
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "resize", false, width, height, repaint);
		}

		/// <summary>
		/// helper to validate series options type
		/// </summary>
		/// <typeparam name="C">series options</typeparam>
		/// <typeparam name="R">series options</typeparam>
		/// <param name="type">series type descriptor</param>
		/// <returns>true if option types match, otherwise throws an exception</returns>
		/// <exception cref="InvalidOperationException"></exception>
		protected static bool ThrowIfOptionsTypeDoesntMatch<C, R>(SeriesType type)
			where C : SeriesOptionsCommon
			where R : SeriesOptionsCommon
			=> typeof(C) == typeof(R) ? true : throw new InvalidOperationException($"Series type {type} requires options of type {typeof(R)}");

		/// <summary>
		/// Check if chart supports the series type.<br/>
		/// Check if options type match series options type.<br/>
		/// Resolve series data item type.
		/// </summary>
		/// <typeparam name="S">series options type</typeparam>
		/// <param name="type">series type descriptor</param>
		protected abstract Type ValidateAndResolveSeries<S>(SeriesType type)
			where S : SeriesOptionsCommon, new();

		/// <summary>
		/// generic add series method
		/// </summary>
		/// <typeparam name="S">series options type</typeparam>
		/// <param name="type">series type descriptor</param>
		/// <param name="options">series options</param>
		/// <param name="paneIndex">pane to which to add the series</param>
		/// <returns>api interface for the added series</returns>
		/// <exception cref="InvalidOperationException"></exception>
		/// <exception cref="NotImplementedException"></exception>
		public async Task<ISeriesApi<H, S>> AddSeries<S>(SeriesType type, S options = null, int paneIndex = 0)
			where S : SeriesOptionsCommon, new()
		{
			//validate options type and resolve expected data item type
			var dataItemType = ValidateAndResolveSeries<S>(type);
			await InitializationCompleted;
			var javascriptRef = await JsModule.AddChartSeries(JsRuntime, JsObjectReference, type, options ?? new S(), paneIndex);
			var uniqueId = await JsModule.GetUniqueJavascriptId(JsRuntime, javascriptRef);
			var series = new SeriesApi<H, S>(uniqueId, JsRuntime, javascriptRef, this, type, dataItemType);
			_Series.Add(uniqueId, series);
			return series;
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase{H}.RemoveSeries"/>
		/// </summary>
		public async Task RemoveSeries(ISeriesApi<H> series)
		{
			await InitializationCompleted;
			if (_Series.ContainsKey(series.UniqueJavascriptId))
			{
				await JsModule.InvokeVoidAsync(JsRuntime, _JsObjectReference, "removeSeries", false, series.JsObjectReference);
				_Series.Remove(series.UniqueJavascriptId);
			}
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase{H}.TimeScale"/>
		/// </summary>
		public async Task<ITimeScaleApi<H>> TimeScale()
		{
			await InitializationCompleted;
			if (_TimeScale == null)
			{
				var timeScale = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, _JsObjectReference, "timeScale", false);
				_TimeScale = new TimeScaleApi<H>(this, timeScale, JsRuntime);
			}

			return _TimeScale;
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase{H}.PriceScale"/>
		/// </summary>
		public async Task<IPriceScaleApi> PriceScale(string priceScaleId, int paneIndex = 0)
		{
			await InitializationCompleted;
			var key = $"{priceScaleId}_{paneIndex}";
			if (!_PriceScales.ContainsKey(key))
			{
				var priceScale = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, JsObjectReference, "priceScale", false, priceScaleId, paneIndex);
				if (priceScale != null)
					_PriceScales[key] = new PriceScaleApi(JsRuntime, priceScale);
				else
					return null;
			}

			return _PriceScales[key];
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase{H}.TakeScreenshot"/>
		/// </summary>
		public async Task<byte[]> TakeScreenshot()
			=> await JsModule.TakeScreenshot(JsRuntime, JsObjectReference);

		/// <summary>
		/// <inheritdoc cref="IChartApiBase{H}.PaneSize"/>
		/// </summary>
		public async Task<PaneSize> PaneSize()
		{
			await InitializationCompleted;
			return await JsModule.InvokeAsync<PaneSize>(JsRuntime, JsObjectReference, "paneSize", false);
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase{H}.AutoSizeActive"/>
		/// </summary>
		public async Task<bool> AutoSizeActive()
		{
			await InitializationCompleted;
			return await JsModule.InvokeAsync<bool>(JsRuntime, JsObjectReference, "autoSizeActive");
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase{H}.SetCrosshairPosition"/>
		/// </summary>
		public async Task SetCrosshairPosition(double price, H horizontalPosition, ISeriesApi<H> series)
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, _JsObjectReference, "setCrosshairPosition", false, price, horizontalPosition, series.JsObjectReference);
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase{H}.ClearCrosshairPosition"/>
		/// </summary>
		public async Task ClearCrosshairPosition()
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, _JsObjectReference, "clearCrosshairPosition", false);
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public async Task<IPaneApi<H>[]> Panes()
		{
			await InitializationCompleted;
			var panes = await JsModule.InvokeAsync<IJSObjectReference[]>(JsRuntime, JsObjectReference, "panes");
			return panes.Select(pane => new PaneApi<H>(JsRuntime, this, pane)).ToArray();
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="index"><inheritdoc/></param>
		/// <returns><inheritdoc/></returns>
		public async Task RemovePane(int index)
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, _JsObjectReference, "removePane", false, index);
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="first"><inheritdoc/></param>
		/// <param name="second"><inheritdoc/></param>
		/// <returns><inheritdoc/></returns>
		public async Task SwapPanes(int first, int second)
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, _JsObjectReference, "swapPanes", false, first, second);
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="paneIndex"><inheritdoc/></param>
		/// <returns><inheritdoc/></returns>
		public async Task<PaneSize> PaneSize(int? paneIndex)
		{
			await InitializationCompleted;
			return await JsModule.InvokeAsync<PaneSize>(JsRuntime, _JsObjectReference, "paneSize", false, paneIndex);
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <returns><inheritdoc/></returns>
		public ValueTask<string> Version()
			=> JsModule.Version(JsRuntime);

		/// <summary>
		/// <inheritdoc cref="ICustomizableObject{O}.Options"/>
		/// </summary>
		public async Task<O> Options()
		{
			await InitializationCompleted;
			return await JsModule.InvokeAsync<O>(JsRuntime, JsObjectReference, "options", false);
		}

		/// <summary>
		/// <inheritdoc cref="ICustomizableObject{O}.ApplyOptions"/>
		/// </summary>
		public async Task ApplyOptions(O options)
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "applyOptions", true, options ?? new());
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

		ISeriesApi<H>[] IChartApiBase<H>.ResolveSeriesFromIds(string[] seriesIds)
			=> seriesIds.Select(x => _Series.TryGetValue(x, out var api) ? api : null).Where(x => x != null).ToArray();
	}
}
