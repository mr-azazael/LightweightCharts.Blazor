using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization
{
	/// <summary>
	/// Defines margins of the price scale.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceScaleMargins
	/// </summary>
	public class PriceScaleMargins : BaseModel
	{
		double _Bottom;
		double _Top;

		/// <summary>
		/// Bottom margin in percentages. Must be greater or equal to 0 and less than 1.
		/// </summary>
		[JsonPropertyName("bottom")]
		public double Bottom
		{
			get => _Bottom;
			set => SetValue(value, ref _Bottom);
		}

		/// <summary>
		/// Top margin in percentages. Must be greater or equal to 0 and less than 1.
		/// </summary>
		[JsonPropertyName("top")]
		public double Top
		{
			get => _Top;
			set => SetValue(value, ref _Top);
		}
	}
}
