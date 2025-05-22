using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Models;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Charts
{
	/// <summary>
	/// Interface to control chart's price scale.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPriceScaleApi"/>
	/// </summary>
	public interface IPriceScaleApi : ICustomizableObject<PriceScaleOptions>
	{
		/// <summary>
		/// Returns a width of the price scale if it's visible or 0 if invisible.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPriceScaleApi#width"/>
		/// </summary>
		Task<double> Width();

		/// <summary>
		/// Returns the visible range of the price scale.
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPriceScaleApi#getVisibleRange"/>
		/// </summary>
		/// <returns>The visible range of the price scale, or null if the range is not set.</returns>
		Task<Range<double>> GetVisibleRange();

		/// <summary>
		/// Sets the visible range of the price scale.
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPriceScaleApi#setVisibleRange"/>
		/// </summary>
		/// <param name="range">The visible range to set, with `from` and `to` properties.</param>
		Task SetVisibleRange(Range<double> range);

		/// <summary>
		/// Sets the auto scale mode of the price scale.
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPriceScaleApi#setAutoScale"/>
		/// </summary>
		/// <param name="on">If true, enables auto scaling; if false, disables it.</param>
		Task SetAutoScale(bool on);
	}

	internal class PriceScaleApi : CustomizableObject<PriceScaleOptions>, IPriceScaleApi
	{
		public PriceScaleApi(IJSRuntime jsRuntime, IJSObjectReference jsObjectReference)
			: base(jsRuntime, jsObjectReference) { }

		public async Task<double> Width()
			=> await JsModule.InvokeAsync<double>(JsRuntime, JsObjectReference, "width");

		public async Task<Range<double>> GetVisibleRange()
			=> await JsModule.InvokeAsync<Range<double>>(JsRuntime, JsObjectReference, "getVisibleRange");

		public async Task SetVisibleRange(Range<double> range)
			=> await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "setVisibleRange", false, range);

		public async Task SetAutoScale(bool on)
			=> await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "setAutoScale", false, on);
	}
}
