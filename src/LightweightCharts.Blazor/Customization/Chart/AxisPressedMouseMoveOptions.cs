using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents options for how the time and price axes react to mouse movements.<vr/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/AxisPressedMouseMoveOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<AxisPressedMouseMoveOptions>))]
	public class AxisPressedMouseMoveOptions : JsonOptionsObject
	{
		/// <summary>
		/// Enable scaling the time axis by holding down the left mouse button and moving the mouse.
		/// </summary>
		[JsonPropertyName("time")]
		public bool Time
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Enable scaling the price axis by holding down the left mouse button and moving the mouse.
		/// </summary>
		[JsonPropertyName("price")]
		public bool Price
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}
	}
}
