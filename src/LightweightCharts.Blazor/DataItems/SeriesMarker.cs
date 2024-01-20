using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	/// Represents a series marker.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/SeriesMarker
	/// </summary>
	public class SeriesMarker : WhitespaceData
	{
		/// <summary>
		/// The position of the marker.
		/// </summary>
		[JsonPropertyName("position")]
		public SeriesMarkerPosition Position { get; set; }

		/// <summary>
		/// The shape of the marker.
		/// </summary>
		[JsonPropertyName("shape")]
		public SeriesMarkerShape Shape { get; set; }

		/// <summary>
		/// The optional size of the marker.
		/// </summary>
		[JsonPropertyName("size")]
		public double Size { get; set; } = 1;

		/// <summary>
		/// The color of the marker.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color { get; set; }

		/// <summary>
		/// The ID of the marker.
		/// </summary>
		[JsonPropertyName("id")]
		public string Id { get; set; }

		/// <summary>
		/// The optional text of the marker.
		/// </summary>
		[JsonPropertyName("text")]
		public string Text { get; set; }
	}
}
