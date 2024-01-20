namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents the source of data to be used for the horizontal price line.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/enums/PriceLineSource
	/// </summary>
	public enum PriceLineSource
	{
		/// <summary>
		/// Use the last bar data.
		/// </summary>
		LastBar = 0,

		/// <summary>
		/// Use the last visible data of the chart viewport.
		/// </summary>
		LastVisible = 1
	}
}
