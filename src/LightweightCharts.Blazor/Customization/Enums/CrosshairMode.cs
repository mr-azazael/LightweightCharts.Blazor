namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents the crosshair mode.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/enums/CrosshairMode"/>
	/// </summary>
	public enum CrosshairMode
	{
		/// <summary>
		/// This mode allows crosshair to move freely on the chart.
		/// </summary>
		Normal,

		/// <summary>
		/// This mode sticks crosshair's horizontal line to the price value of a single-value series or to the close price of OHLC-based series.
		/// </summary>
		Magnet,

		/// <summary>
		/// This mode disables rendering of the crosshair.
		/// </summary>
		Hidden
	}
}
