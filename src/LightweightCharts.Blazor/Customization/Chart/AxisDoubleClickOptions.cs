using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents options for how the time and price axes react to mouse double click.
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/AxisDoubleClickOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<AxisDoubleClickOptions>))]
	public class AxisDoubleClickOptions : JsonOptionsObject
	{
		/// <summary>
		/// Enable resetting scaling the time axis by double-clicking the left mouse button.
		/// </summary>
		[JsonPropertyName("time")]
		public bool Time
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Enable reseting scaling the price axis by by double-clicking the left mouse button.
		/// </summary>
		[JsonPropertyName("price")]
		public bool Price
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}
	}
}
