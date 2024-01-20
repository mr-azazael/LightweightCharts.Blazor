using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Series;

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
}
