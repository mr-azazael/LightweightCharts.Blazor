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
