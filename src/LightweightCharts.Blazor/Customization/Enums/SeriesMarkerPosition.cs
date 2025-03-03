using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents the position of a series marker relative to a bar.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api#seriesmarkerposition"/>
	/// </summary>
	[JsonConverter(typeof(SeriesMarkerPositionConverter))]
	public enum SeriesMarkerPosition
	{
		/// <summary>
		/// Marker is above.
		/// </summary>
		AboveBar,

		/// <summary>
		/// Marker is below.
		/// </summary>
		BelowBar,

		/// <summary>
		/// Marker is centered on the bar.
		/// </summary>
		InBar
	}

	internal class SeriesMarkerPositionConverter : BaseEnumJsonConverter<SeriesMarkerPosition>
	{
		protected override Dictionary<SeriesMarkerPosition, string> GetEnumMapping() => new Dictionary<SeriesMarkerPosition, string>
		{
			[SeriesMarkerPosition.AboveBar] = "aboveBar",
			[SeriesMarkerPosition.BelowBar] = "belowBar",
			[SeriesMarkerPosition.InBar] = "inBar"
		};
	}
}
