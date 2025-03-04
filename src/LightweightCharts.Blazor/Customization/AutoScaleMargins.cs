using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization
{
	/// <summary>
	/// Represents the margin used when updating a price scale.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/AutoScaleMargins"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<AutoScaleMargins>))]
	public class AutoScaleMargins : JsonOptionsObject
	{
		/// <summary>
		/// The number of pixels for bottom margin.
		/// </summary>
		[JsonPropertyName("below")]
		public double Below
		{
			get => GetValue<double>();
			set => SetValue(value);
		}

		/// <summary>
		/// The number of pixels for top margin.
		/// </summary>
		[JsonPropertyName("above")]
		public double Above
		{
			get => GetValue<double>();
			set => SetValue(value);
		}
	}
}
