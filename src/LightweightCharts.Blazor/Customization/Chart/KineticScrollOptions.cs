using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents options for enabling or disabling kinetic scrolling with mouse and touch gestures.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/KineticScrollOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<KineticScrollOptions>))]
	public class KineticScrollOptions : JsonOptionsObject
	{
		/// <summary>
		/// Enable kinetic scroll with touch gestures.
		/// </summary>
		[JsonPropertyName("touch")]
		public bool Touch
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Enable kinetic scroll with the mouse.
		/// </summary>
		[JsonPropertyName("mouse")]
		public bool Mouse
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}
	}
}
