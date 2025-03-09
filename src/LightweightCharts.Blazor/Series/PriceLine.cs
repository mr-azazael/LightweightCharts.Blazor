using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Series;
using Microsoft.JSInterop;

namespace LightweightCharts.Blazor.Series
{
	internal class PriceLine<H> : CustomizableObject<PriceLineOptions>, IPriceLine<H>
		where H : struct
	{
		public ISeriesApi<H> Parent { get; }

		internal PriceLine(IJSRuntime jsRuntime, IJSObjectReference jsObject, ISeriesApi<H> parent) : base(jsRuntime, jsObject)
		{
			Parent = parent;
		}
	}
}
