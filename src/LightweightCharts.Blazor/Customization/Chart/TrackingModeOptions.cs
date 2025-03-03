using LightweightCharts.Blazor.Customization.Enums;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represent options for the tracking mode's behavior.<br/>
	/// Mobile users will not have the ability to see the values/dates like they do on desktop.<br/>
	/// To see it, they should enter the tracking mode. The tracking mode will deactivate the scrolling and make it possible to check values and dates.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/TrackingModeOptions"/>
	/// </summary>
	public class TrackingModeOptions : BaseModel
	{
		TrackingModeExitMode _ExitMode = TrackingModeExitMode.OnNextTap;

		/// <summary>
		/// Determine how to exit the tracking mode.<br/>
		/// By default, mobile users will long press to deactivate the scroll and have the ability to check values and dates.<br/>
		/// Another press is required to activate the scroll, be able to move left/right, zoom, etc.<br/>
		/// </summary>
		[JsonPropertyName("exitMode")]
		public TrackingModeExitMode ExitMode
		{
			get => _ExitMode;
			set => SetValue(value, ref _ExitMode);
		}
	}
}
