using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Series;
using Microsoft.JSInterop;

namespace LightweightCharts.Blazor.Series
{
	internal class PriceLine : CustomizableObject<PriceLineOptions>, IPriceLine
	{
		public ISeriesApi Parent { get; }

		internal PriceLine(IJSObjectReference jsObject, ISeriesApi parent) : base(jsObject)
		{
			Parent = parent;
		}
	}
}
