using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization
{
	/// <summary>
	/// Defines margins of the price scale.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceScaleMargins"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<PriceScaleMargins>))]
	public class PriceScaleMargins : JsonOptionsObject
	{
		/// <summary>
		/// Bottom margin in percentages. Must be greater or equal to 0 and less than 1.
		/// </summary>
		[JsonPropertyName("bottom")]
		public double Bottom
		{
			get => GetValue<double>();
			set => SetValue(value);
		}

		/// <summary>
		/// Top margin in percentages. Must be greater or equal to 0 and less than 1.
		/// </summary>
		[JsonPropertyName("top")]
		public double Top
		{
			get => GetValue<double>();
			set => SetValue(value);
		}
	}
}
