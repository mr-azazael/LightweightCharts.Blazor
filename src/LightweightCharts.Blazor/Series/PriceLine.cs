using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Series;
using Microsoft.JSInterop;

namespace LightweightCharts.Blazor.Series
{
	/// <summary>
	/// Represents the interface for interacting with price lines.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPriceLine
	/// </summary>
	public interface IPriceLine : ICustomizableObject<PriceLineOptions>
	{
		/// <summary>
		/// The series that contains this series.
		/// </summary>
		public ISeriesApi Parent { get; }
	}

	internal class PriceLine : CustomizableObject<PriceLineOptions>, IPriceLine
	{
		public ISeriesApi Parent { get; }

		internal PriceLine(IJSObjectReference jsObject, ISeriesApi parent) : base(jsObject)
		{
			Parent = parent;
		}
	}
}
