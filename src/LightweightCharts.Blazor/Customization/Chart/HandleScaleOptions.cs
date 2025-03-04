using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents options for how the chart is scaled by the mouse and touch gestures.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/HandleScaleOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<HandleScaleOptions>))]
	public class HandleScaleOptions : JsonOptionsObject
	{
		/// <summary>
		/// Enable scaling with the mouse wheel.
		/// </summary>
		[JsonPropertyName("mouseWheel")]
		public bool MouseWheel
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Enable scaling with pinch/zoom gestures.
		/// </summary>
		[JsonPropertyName("pinch")]
		public bool Pinch
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Enable scaling the price and/or time scales by holding down the left mouse button and moving the mouse.
		/// </summary>
		[JsonPropertyName("axisPressedMouseMove")]
		public AxisPressedMouseMoveOptions AxisPressedMouseMove
		{
			get => GetValue(() => new AxisPressedMouseMoveOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Enable resetting scaling by double-clicking the left mouse button.
		/// </summary>
		[JsonPropertyName("axisDoubleClickReset")]
		public AxisDoubleClickOptions AxisDoubleClickReset
		{
			get => GetValue(() => new AxisDoubleClickOptions());
			set => SetValue(value);
		}
	}
}
