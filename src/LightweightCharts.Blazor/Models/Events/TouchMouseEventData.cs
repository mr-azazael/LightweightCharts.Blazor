using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models.Events
{
	/// <summary>
	/// The TouchMouseEventData interface represents events that occur due to the user interacting with a pointing device (such as a mouse).<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/TouchMouseEventData"/>
	/// </summary>
	public class TouchMouseEventData
	{
		/// <summary>
		/// The X coordinate of the mouse pointer in local (DOM content) coordinates.
		/// </summary>
		[JsonPropertyName("clientX")]
		public double ClientX { get; init; }

		/// <summary>
		/// The X coordinate of the mouse pointer in local (DOM content) coordinates.
		/// </summary>
		[JsonPropertyName("clientY")]
		public double ClientY { get; init; }

		/// <summary>
		/// The X coordinate of the mouse pointer relative to the whole document.
		/// </summary>
		[JsonPropertyName("pageX")]
		public double PageX { get; init; }

		/// <summary>
		/// The Y coordinate of the mouse pointer relative to the whole document.
		/// </summary>
		[JsonPropertyName("pageY")]
		public double PageY { get; init; }

		/// <summary>
		/// The X coordinate of the mouse pointer in global (screen) coordinates.
		/// </summary>
		[JsonPropertyName("screenX")]
		public double ScreenX { get; init; }

		/// <summary>
		/// The Y coordinate of the mouse pointer in global (screen) coordinates.
		/// </summary>
		[JsonPropertyName("screenY")]
		public double ScreenY { get; init; }

		/// <summary>
		/// The X coordinate of the mouse pointer relative to the chart / price axis / time axis canvas element.
		/// </summary>
		[JsonPropertyName("localX")]
		public double LocalX { get; init; }

		/// <summary>
		/// The Y coordinate of the mouse pointer relative to the chart / price axis / time axis canvas element.
		/// </summary>
		[JsonPropertyName("localY")]
		public double LocalY { get; init; }

		/// <summary>
		/// Returns a boolean value that is true if the Ctrl key was active when the key event was generated.
		/// </summary>
		[JsonPropertyName("ctrlKey")]
		public bool CtrlKey { get; init; }

		/// <summary>
		/// Returns a boolean value that is true if the Alt (Option or ⌥ on macOS) key was active when the key event was generated.
		/// </summary>
		[JsonPropertyName("altKey")]
		public bool AltKey { get; init; }

		/// <summary>
		/// Returns a boolean value that is true if the Shift key was active when the key event was generated.
		/// </summary>
		[JsonPropertyName("shiftKey")]
		public bool ShiftKey { get; init; }

		/// <summary>
		/// Returns a boolean value that is true if the Meta key (on Mac keyboards, the ⌘ Command key; on Windows keyboards, the Windows key (⊞)) was active when the key event was generated.
		/// </summary>
		[JsonPropertyName("metaKey")]
		public bool MetaKey { get; init; }
	}
}
