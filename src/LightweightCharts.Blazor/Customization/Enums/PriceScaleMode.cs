namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents the price scale mode.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/enums/PriceScaleMode"/>
	/// </summary>
	public enum PriceScaleMode
	{
		/// <summary>
		/// Price scale shows prices. Price range changes linearly.
		/// </summary>
		Normal,

		/// <summary>
		/// Price scale shows prices. Price range changes logarithmically.
		/// </summary>
		Logarithmic,

		/// <summary>
		/// Price scale shows percentage values according the first visible value of the price scale. The first visible value is 0% in this mode.
		/// </summary>
		Percentage,

		/// <summary>
		/// The same as percentage mode, but the first value is moved to 100.
		/// </summary>
		Indexed
	}
}
