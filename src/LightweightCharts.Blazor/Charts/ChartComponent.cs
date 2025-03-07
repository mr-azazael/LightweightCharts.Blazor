using LightweightCharts.Blazor.Customization;
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
using System.Linq;
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
		IJSObjectReference _JsObjectReference;
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
				_JsObjectReference = await JsModule.CreateChart(JsRuntime, Id, new ChartOptions());
				_EventsHelper = new EventsHelper(this, JsRuntime);

				_EventsHelper.AddEvent<InternalMouseEventParams>(OnClicked, Events.Click);
				_EventsHelper.AddEvent<InternalMouseEventParams>(OnDoubleClicked, Events.DoubleClick);
				_EventsHelper.AddEvent<InternalMouseEventParams>(OnCrosshairMoved, Events.CrosshairMove);

				_InitializationCompleted.SetResult();
			}
		}

		/// <summary>
		/// <inheritdoc cref="ICustomizableObject{ChartOptions}.Options"/>
		/// </summary>
		public async Task<ChartOptions> Options()
		{
			await InitializationCompleted;
			return await JsModule.InvokeAsync<ChartOptions>(JsRuntime, JsObjectReference, "options", false);
		}

		/// <summary>
		/// <inheritdoc cref="ICustomizableObject{ChartOptions}.ApplyOptions"/>
		/// </summary>
		public async Task ApplyOptions(ChartOptions options)
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, _JsObjectReference, "applyOptions", true, options ?? new());
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
		/// <inheritdoc/>
		/// </summary>
		/// <typeparam name="O"><inheritdoc/></typeparam>
		/// <param name="type"><inheritdoc/></param>
		/// <param name="options"><inheritdoc/></param>
		/// <param name="paneIndex"><inheritdoc/></param>
		/// <returns><inheritdoc/></returns>
		public async Task<ISeriesApi<O>> AddSeries<O>(SeriesType type, O options = null, int? paneIndex = null)
			where O : SeriesOptionsCommon, new()
		{
			bool ThrowIfOptionsTypeDoesntMatch<C, R>()
				=> typeof(C) == typeof(R) ? true : throw new InvalidOperationException($"Series type {type} requires options of type {typeof(R)}");

			//validate options type and resolve expected data item type
			Type dataItemType = null;
			switch (type)
			{
				case SeriesType.Line:
					{
						ThrowIfOptionsTypeDoesntMatch<O, LineStyleOptions>();
						dataItemType = typeof(LineData);
						break;
					}
				case SeriesType.Area:
					{
						ThrowIfOptionsTypeDoesntMatch<O, AreaStyleOptions>();
						dataItemType = typeof(AreaData);
						break;
					}
				case SeriesType.Bar:
					{
						ThrowIfOptionsTypeDoesntMatch<O, BarStyleOptions>();
						dataItemType = typeof(BarData);
						break;
					}
				case SeriesType.Candlestick:
					{
						ThrowIfOptionsTypeDoesntMatch<O, CandlestickStyleOptions>();
						dataItemType = typeof(CandlestickData);
						break;
					}
				case SeriesType.Histogram:
					{
						ThrowIfOptionsTypeDoesntMatch<O, HistogramStyleOptions>();
						dataItemType = typeof(HistogramData);
						break;
					}
				case SeriesType.Baseline:
					{
						ThrowIfOptionsTypeDoesntMatch<O, BaselineStyleOptions>();
						dataItemType = typeof(BaselineData);
						break;
					}
				default:
					throw new NotImplementedException("chart type not handled");
			};

			await InitializationCompleted;
			var javascriptRef = await JsModule.AddChartSeries(JsRuntime, _JsObjectReference, type, options ?? new O());
			var uniqueId = await JsModule.GetUniqueJavascriptId(JsRuntime, javascriptRef);
			var series = new SeriesApi<O>(uniqueId, JsRuntime, javascriptRef, this, type, dataItemType);
			_Series.Add(uniqueId, series);
			return series;
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="type"><inheritdoc/></param>
		/// <param name="paneIndex"><inheritdoc/></param>
		/// <returns><inheritdoc/></returns>
		public async Task<ISeriesApi> AddSeries(SeriesType type, int? paneIndex = null)
		{
			switch (type)
			{
				case SeriesType.Line:
					return await AddSeries<LineStyleOptions>(type, new(), paneIndex);
				case SeriesType.Area:
					return await AddSeries<AreaStyleOptions>(type, new(), paneIndex);
				case SeriesType.Bar:
					return await AddSeries<BarStyleOptions>(type, new(), paneIndex);
				case SeriesType.Candlestick:
					return await AddSeries<CandlestickStyleOptions>(type, new(), paneIndex);
				case SeriesType.Histogram:
					return await AddSeries<HistogramStyleOptions>(type, new(), paneIndex);
				case SeriesType.Baseline:
					return await AddSeries<BaselineStyleOptions>(type, new(), paneIndex);
				default:
					throw new NotImplementedException("chart type not handled");
			}
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.RemoveSeriesAsync"/>
		/// </summary>
		public async Task RemoveSeriesAsync(ISeriesApi series)
		{
			await InitializationCompleted;
			if (_Series.ContainsKey(series.UniqueJavascriptId))
			{
				await JsModule.InvokeVoidAsync(JsRuntime, _JsObjectReference, "removeSeries", false, series.JsObjectReference);
				_Series.Remove(series.UniqueJavascriptId);
			}
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.TimeScaleAsync"/>
		/// </summary>
		public async Task<ITimeScaleApi> TimeScaleAsync()
		{
			await InitializationCompleted;
			if (_TimeScale == null)
			{
				var reference = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, _JsObjectReference, "timeScale", false);
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
				var priceScale = await JsModule.InvokeAsync<IJSObjectReference>(JsRuntime, JsObjectReference, "priceScale", false, id);
				if (priceScale != null)
					_PriceScales[id] = new PriceScaleApi(JsRuntime, priceScale);
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
			await JsModule.InvokeVoidAsync(JsRuntime, _JsObjectReference, "setCrosshairPosition", false, price, horizontalPosition, series.JsObjectReference);
		}

		/// <summary>
		/// <inheritdoc cref="IChartApiBase.ClearCrosshairPosition"/>
		/// </summary>
		public async Task ClearCrosshairPosition()
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, _JsObjectReference, "clearCrosshairPosition", false);
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public async Task<IPaneApi[]> Panes()
		{
			await InitializationCompleted;
			var panes = await JsModule.InvokeAsync<IJSObjectReference[]>(JsRuntime, JsObjectReference, "panes");
			return panes.Select(x => new PaneApi(JsRuntime, this, x)).ToArray();
		}

		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		/// <param name="index"><inheritdoc/></param>
		/// <returns><inheritdoc/></returns>
		public async Task RemovePane(int index)
		{
			await InitializationCompleted;
			await JsModule.InvokeVoidAsync(JsRuntime, _JsObjectReference, "removePane");
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

		internal ISeriesApi[] ResolveSeriesFromIds(string[] seriesIds)
			=> seriesIds.Select(x => _Series.TryGetValue(x, out var api) ? api : null).Where(x => x != null).ToArray();
	}
}
