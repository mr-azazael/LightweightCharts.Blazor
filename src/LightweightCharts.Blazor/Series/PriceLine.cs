using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Series;
using Microsoft.JSInterop;

namespace LightweightCharts.Blazor.Series
{
	internal class PriceLine : CustomizableObject<PriceLineOptions>, IPriceLine
	{
		public ISeriesApi Parent { get; }

		internal PriceLine(IJSRuntime jsRuntime, IJSObjectReference jsObject, ISeriesApi parent) : base(jsRuntime, jsObject)
		{
			Parent = parent;
		}
	}
}
