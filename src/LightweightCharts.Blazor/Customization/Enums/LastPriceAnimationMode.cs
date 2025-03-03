namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents the type of the last price animation for series such as area or line.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/enums/LastPriceAnimationMode"/>
	/// </summary>
	public enum LastPriceAnimationMode
	{
		/// <summary>
		/// Animation is always disabled.
		/// </summary>
		Disabled,

		/// <summary>
		/// Animation is always enabled.
		/// </summary>
		Continuous,

		/// <summary>
		/// Animation is active after new data.
		/// </summary>
		OnDataUpdate
	}
}
