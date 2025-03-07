using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents series value formatting options.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceFormatCustom"/>
	/// </summary>
	public class PriceFormatCustom
	{
		/// <summary>
		/// The custom price format.
		/// </summary>
		[JsonPropertyName("type")]
		public string Type
			=> "custom";

		/// <summary>
		/// Override price formatting behaviour. Can be used for cases that can't be covered with built-in price formats.
		/// </summary>
		[JsonPropertyName("formatter")]
		public JsDelegate Formatter { get; set; }

		/// <summary>
		/// The minimum possible step size for price value movement.
		/// </summary>
		[JsonPropertyName("minMove")]
		public float MinMove { get; set; } = 0.01f;
	}
}
