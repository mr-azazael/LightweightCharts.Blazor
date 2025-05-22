using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Plugins;
using LightweightCharts.Blazor.Series;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Charts
{
	/// <summary>
	/// Represents the interface for interacting with a pane in a lightweight chart.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPaneApi"/>
	/// </summary>
	public interface IPaneApi<H> : IJsObjectWrapper
		where H : struct
	{
		/// <summary>
		/// Retrieves the height of the pane in pixels.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPaneApi#getheight"/>
		/// </summary>
		/// <returns>The height of the pane in pixels.</returns>
		Task<int> GetHeight();

		/// <summary>
		/// Sets the height of the pane.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPaneApi#setheight"/>
		/// </summary>
		/// <param name="height">The number of pixels to set as the height of the pane.</param>
		Task SetHeight(int height);

		/// <summary>
		/// Moves the pane to a new position.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPaneApi#moveto"/>
		/// </summary>
		/// <param name="paneIndex">The target index of the pane. Should be a number between 0 and the total number of panes - 1.</param>
		Task MoveTo(int paneIndex);

		/// <summary>
		/// Retrieves the index of the pane.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPaneApi#paneindex"/>
		/// </summary>
		/// <returns>The index of the pane. It is a number between 0 and the total number of panes - 1.</returns>
		Task<int> PaneIndex();

		/// <summary>
		/// Retrieves the array of series for the current pane.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPaneApi#getseries"/>
		/// </summary>
		/// <returns>An array of series.</returns>
		Task<ISeriesApi<H>[]> GetSeries();

		/// <summary>
		/// Returns the price scale with the given id.<br/>
		/// Throws if the price scale with the given id is not found in this pane.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPaneApi#pricescale"/>
		/// </summary>
		/// <param name="priceScaleId">ID of the price scale to find</param>
		Task<IPriceScaleApi> PriceScale(string priceScaleId);

		#region plugins

		/// <summary>
		/// Creates an image watermark.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/functions/createImageWatermark"/>
		/// </summary>
		/// <param name="url">Image URL.</param>
		/// <param name="options">Watermark options.</param>
		/// <returns>Image watermark wrapper.</returns>
		ValueTask<IImageWatermarkPluginApi<H>> CreateImageWatermark(string url, ImageWatermarkOptions options = null);

		/// <summary>
		/// Creates a text watermark.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/functions/createTextWatermark"/>
		/// </summary>
		/// <param name="options">Watermark options.</param>
		/// <returns>Text watermark wrapper.</returns>
		ValueTask<ITextWatermarkPluginApi<H>> CreateTextWatermark(TextWatermarkOptions options = null);

		#endregion
	}

	class PaneApi<H> : IPaneApi<H>
		where H : struct
	{
		public PaneApi(IJSRuntime jsRuntime, IChartApiBase<H> parent, IJSObjectReference jsObject)
		{
			_JsRuntime = jsRuntime;
			_Parent = parent;
			JsObjectReference = jsObject;
		}

		IJSRuntime _JsRuntime;
		IChartApiBase<H> _Parent;
		Dictionary<string, IPriceScaleApi> _PriceScales = [];

		public IJSObjectReference JsObjectReference { get; }

		public async Task<int> GetHeight()
			=> await JsModule.InvokeAsync<int>(_JsRuntime, JsObjectReference, "getHeight");

		public async Task SetHeight(int height)
			=> await JsModule.InvokeVoidAsync(_JsRuntime, JsObjectReference, "setHeight", false, height);

		public async Task MoveTo(int paneIndex)
			=> await JsModule.InvokeVoidAsync(_JsRuntime, JsObjectReference, "moveTo", false, paneIndex);

		public async Task<int> PaneIndex()
			=> await JsModule.InvokeAsync<int>(_JsRuntime, JsObjectReference, "paneIndex");

		public async Task<ISeriesApi<H>[]> GetSeries()
		{
			var seriesIds = await JsModule.GetPaneSeries(_JsRuntime, JsObjectReference);
			return _Parent.ResolveSeriesFromIds(seriesIds);
		}

		public async Task<IPriceScaleApi> PriceScale(string priceScaleId)
		{
			priceScaleId ??= string.Empty;
			if (_PriceScales.TryGetValue(priceScaleId, out var api))
				return api;

			var jsReference = await JsModule.InvokeAsync<IJSObjectReference>(_JsRuntime, JsObjectReference, "priceScale", false, priceScaleId);
			if (jsReference == null)
				return null;

			return _PriceScales[priceScaleId] = new PriceScaleApi(_JsRuntime, jsReference);
		}

		public ValueTask<IImageWatermarkPluginApi<H>> CreateImageWatermark(string url, ImageWatermarkOptions options = null)
			=> JsModule.CreateImageWatermark(_JsRuntime, this, url, options ?? new());

		public ValueTask<ITextWatermarkPluginApi<H>> CreateTextWatermark(TextWatermarkOptions options = null)
			=> JsModule.CreateTextWatermark(_JsRuntime, this, options ?? new());
	}
}
