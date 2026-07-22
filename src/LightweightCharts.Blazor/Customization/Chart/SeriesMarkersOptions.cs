using LightweightCharts.Blazor.Customization.Enums;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/next/api/interfaces/SeriesMarkersOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<SeriesMarkersOptions>))]
	public class SeriesMarkersOptions : JsonOptionsObject
	{
		/// <summary>
		/// Specifies whether the auto-scaling calculation should expand to include the size of markers.<br/>
		/// When true, the auto-scale feature will adjust the price scale's range to ensure series markers are fully visible and not cropped by the chart's edges.<br/>
		/// When false, the scale will only fit the series data points, which may cause markers to be partially hidden.<br/>
		/// Note: This option only has an effect when auto-scaling is enabled for the price scale.
		/// </summary>
		[JsonPropertyName("autoScale")]
		public bool AutoScale
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Defines the stacking order of the markers relative to the series and other primitives.
		/// </summary>
		[JsonPropertyName("SeriesMarkerZOrder")]
		public SeriesMarkerZOrder ZOrder
		{
			get => GetValue(() => SeriesMarkerZOrder.Normal);
			set => SetValue(value);
		}
	}
}
