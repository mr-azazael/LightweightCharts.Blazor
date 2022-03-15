using LightweightCharts.Blazor.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Determine how to exit the tracking mode.<br/>
	/// By default, mobile users will long press to deactivate the scroll and have the ability to check values and dates.<br/>
	/// Another press is required to activate the scroll, be able to move left/right, zoom, etc.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/enums/TrackingModeExitMode
	/// </summary>
	[JsonConverter(typeof(TrackingModeExitModeConverter))]
	public enum TrackingModeExitMode
	{
		/// <summary>
		/// Tracking Mode will be deactivated on touch end event.
		/// </summary>
		OnTouchEnd = 0,

		/// <summary>
		/// Tracking Mode will be deactivated on the next tap event.
		/// </summary>
		OnNextTap = 1
	}

	internal class TrackingModeExitModeConverter : BaseEnumJsonConverter<TrackingModeExitMode>
	{
		protected override Dictionary<TrackingModeExitMode, string> GetEnumMapping() => new Dictionary<TrackingModeExitMode, string>
		{
			[TrackingModeExitMode.OnTouchEnd] = "0",
			[TrackingModeExitMode.OnNextTap] = "1"
		};
	}
}
