using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents options for enabling or disabling kinetic scrolling with mouse and touch gestures.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/KineticScrollOptions
	/// </summary>
	public class KineticScrollOptions : BaseModel
	{
		bool _Touch = true;
		bool _Mouse = false;

		/// <summary>
		/// Enable kinetic scroll with touch gestures.
		/// </summary>
		[JsonPropertyName("touch")]
		public bool Touch
		{
			get => _Touch;
			set => SetValue(value, ref _Touch);
		}

		/// <summary>
		/// Enable kinetic scroll with the mouse.
		/// </summary>
		[JsonPropertyName("mouse")]
		public bool Mouse
		{
			get => _Mouse;
			set => SetValue(value, ref _Mouse);
		}
	}
}
