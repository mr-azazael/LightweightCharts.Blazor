using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents options for how the chart is scaled by the mouse and touch gestures.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/HandleScaleOptions
	/// </summary>
	public class HandleScaleOptions : BaseModel
	{
		AxisPressedMouseMoveOptions _AxisPressedMouseMove = new();
		bool _AxisDoubleClickReset = true;
		bool _MouseWheel = true;
		bool _Pinch = true;

		/// <summary>
		/// Enable scaling the price and/or time scales by holding down the left mouse button and moving the mouse.
		/// </summary>
		[JsonPropertyName("axisPressedMouseMove")]
		public AxisPressedMouseMoveOptions AxisPressedMouseMove
		{
			get => _AxisPressedMouseMove;
			set => SetValue(value, ref _AxisPressedMouseMove);
		}

		/// <summary>
		/// Enable resetting scaling by double-clicking the left mouse button.
		/// </summary>
		[JsonPropertyName("axisDoubleClickReset")]
		public bool AxisDoubleClickReset
		{
			get => _AxisDoubleClickReset;
			set => SetValue(value, ref _AxisDoubleClickReset);
		}

		/// <summary>
		/// Enable scaling with the mouse wheel.
		/// </summary>
		[JsonPropertyName("mouseWheel")]
		public bool MouseWheel
		{
			get => _MouseWheel;
			set => SetValue(value, ref _MouseWheel);
		}

		/// <summary>		
		/// Enable scaling with pinch/zoom gestures.
		/// </summary>
		[JsonPropertyName("pinch")]
		public bool Pinch
		{
			get => _Pinch;
			set => SetValue(value, ref _Pinch);
		}
	}
}
