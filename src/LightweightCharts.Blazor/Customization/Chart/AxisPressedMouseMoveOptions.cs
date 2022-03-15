using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents options for how the time and price axes react to mouse movements.<vr/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/AxisPressedMouseMoveOptions
	/// </summary>
	public class AxisPressedMouseMoveOptions : BaseModel
	{
		bool _Time = true;
		bool _Price = true;

		/// <summary>
		/// Enable scaling the time axis by holding down the left mouse button and moving the mouse.
		/// </summary>
		[JsonPropertyName("time")]
		public bool Time
		{
			get => _Time;
			set => SetValue(value, ref _Time);
		}

		/// <summary>
		/// Enable scaling the price axis by holding down the left mouse button and moving the mouse.
		/// </summary>
		[JsonPropertyName("price")]
		public bool Price
		{
			get => _Price;
			set => SetValue(value, ref _Price);
		}
	}
}
