using LightweightCharts.Blazor.Converters;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Configuration options for the UpDownMarkers plugin.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/UpDownMarkersPluginOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<UpDownMarkersPluginOptions>))]
	public class UpDownMarkersPluginOptions : JsonOptionsObject
	{
		/// <summary>
		/// The color used for markers indicating a positive price change.<br/>
		/// This color will be applied to markers shown above data points where the price has increased.
		/// </summary>
		[JsonPropertyName("positiveColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? PositiveColor
		{
			get => GetValue<Color?>();
			set => SetValue(value);
		}

		/// <summary>
		/// The color used for markers indicating a negative price change.<br/>
		/// This color will be applied to markers shown below data points where the price has decreased.
		/// </summary>
		[JsonPropertyName("negativeColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? NegativeColor
		{
			get => GetValue<Color?>();
			set => SetValue(value);
		}

		/// <summary>
		/// The duration (in milliseconds) for which update markers remain visible on the chart.<br/>
		/// After this duration, the markers will automatically disappear.<br/>
		/// Set to 0 for markers to remain indefinitely until the next update.
		/// </summary>
		[JsonPropertyName("updateVisibilityDuration")]
		public int UpdateVisibilityDuration
		{
			get => GetValue<int>();
			set => SetValue(value);
		}
	}
}
