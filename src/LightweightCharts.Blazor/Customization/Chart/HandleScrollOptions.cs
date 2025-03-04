using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents options for how the chart is scrolled by the mouse and touch gestures.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/HandleScrollOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<HandleScrollOptions>))]
	public class HandleScrollOptions : JsonOptionsObject
	{
		/// <summary>
		/// Enable scrolling with the mouse wheel.
		/// </summary>
		[JsonPropertyName("mouseWheel")]
		public bool MouseWheel
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Enable scrolling by holding down the left mouse button and moving the mouse.
		/// </summary>
		[JsonPropertyName("pressedMouseMove")]
		public bool PressedMouseMove
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Enable horizontal touch scrolling.<br/>
		/// When enabled the chart handles touch gestures that would normally scroll the webpage horizontally.
		/// </summary>
		[JsonPropertyName("horzTouchDrag")]
		public bool HorizontalTouchDrag
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Enable vertical touch scrolling.<br/>
		/// When enabled the chart handles touch gestures that would normally scroll the webpage vertically.
		/// </summary>
		[JsonPropertyName("vertTouchDrag")]
		public bool VerticalTouchDrag
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}
	}
}
