using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
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
	}

	internal class PriceScaleApi : CustomizableObject<PriceScaleOptions>, IPriceScaleApi
	{
		public PriceScaleApi(IJSObjectReference jsObjectReference)
			: base(jsObjectReference) { }

		public async Task<double> Width()
			=> await JsObjectReference.InvokeAsync<double>("width");
	}
}
