namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Determine how to exit the tracking mode.<br/>
	/// By default, mobile users will long press to deactivate the scroll and have the ability to check values and dates.<br/>
	/// Another press is required to activate the scroll, be able to move left/right, zoom, etc.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/enums/TrackingModeExitMode"/>
	/// </summary>
	public enum TrackingModeExitMode
	{
		/// <summary>
		/// Tracking Mode will be deactivated on touch end event.
		/// </summary>
		OnTouchEnd,

		/// <summary>
		/// Tracking Mode will be deactivated on the next tap event.
		/// </summary>
		OnNextTap
	}
}
