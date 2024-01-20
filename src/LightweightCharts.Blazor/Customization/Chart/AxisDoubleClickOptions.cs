using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents options for how the time and price axes react to mouse double click.
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/AxisDoubleClickOptions
	/// </summary>
	public class AxisDoubleClickOptions : BaseModel
	{
		bool _Time = true;
		bool _Price = true;

		/// <summary>
		/// Enable resetting scaling the time axis by double-clicking the left mouse button.
		/// </summary>
		[JsonPropertyName("time")]
		public bool Time
		{
			get => _Time;
			set => SetValue(value, ref _Time);
		}

		/// <summary>
		/// Enable reseting scaling the price axis by by double-clicking the left mouse button.
		/// </summary>
		[JsonPropertyName("price")]
		public bool Price
		{
			get => _Price;
			set => SetValue(value, ref _Price);
		}
	}
}
