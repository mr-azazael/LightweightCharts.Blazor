using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents options for how the chart is scrolled by the mouse and touch gestures.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/HandleScrollOptions
	/// </summary>
	public class HandleScrollOptions : BaseModel
	{
		bool _MouseWheel = true;
		bool _PressedMouseMove = true;
		bool _HorizontalTouchDrag = true;
		bool _VerticalTouchDrag = true;

		/// <summary>
		/// Enable scrolling with the mouse wheel.
		/// </summary>
		[JsonPropertyName("mouseWheel")]
		public bool MouseWheel
		{
			get => _MouseWheel;
			set => SetValue(value, ref _MouseWheel);
		}

		/// <summary>
		/// Enable scrolling by holding down the left mouse button and moving the mouse.
		/// </summary>
		[JsonPropertyName("pressedMouseMove")]
		public bool PressedMouseMove
		{
			get => _PressedMouseMove;
			set => SetValue(value, ref _PressedMouseMove);
		}

		/// <summary>
		/// Enable horizontal touch scrolling.<br/>
		/// When enabled the chart handles touch gestures that would normally scroll the webpage horizontally.
		/// </summary>
		[JsonPropertyName("horzTouchDrag")]
		public bool HorizontalTouchDrag
		{
			get => _HorizontalTouchDrag;
			set => SetValue(value, ref _HorizontalTouchDrag);
		}

		/// <summary>
		/// Enable vertical touch scrolling.<br/>
		/// When enabled the chart handles touch gestures that would normally scroll the webpage vertically.
		/// </summary>
		[JsonPropertyName("vertTouchDrag")]
		public bool VerticalTouchDrag
		{
			get => _VerticalTouchDrag;
			set => SetValue(value, ref _VerticalTouchDrag);
		}
	}
}
