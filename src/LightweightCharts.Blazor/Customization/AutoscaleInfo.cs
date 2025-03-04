using LightweightCharts.Blazor.Models;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization
{
	/// <summary>
	/// Represents information used to update a price scale.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/AutoscaleInfo"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<AutoscaleInfo>))]
	public class AutoscaleInfo : JsonOptionsObject
	{
		/// <summary>
		/// Price range.
		/// </summary>
		[JsonPropertyName("priceRange")]
		public PriceRange PriceRange
		{
			get => GetValue(() => new PriceRange());
			set => SetValue(value);
		}

		/// <summary>
		/// Scale margins.
		/// </summary>
		[JsonPropertyName("margins")]
		public AutoScaleMargins Margins
		{
			get => GetValue(() => new AutoScaleMargins());
			set => SetValue(value);
		}
	}
}
