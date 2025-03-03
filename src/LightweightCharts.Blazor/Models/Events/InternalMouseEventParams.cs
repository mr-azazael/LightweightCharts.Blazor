using LightweightCharts.Blazor.Customization.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models.Events
{
	/// <summary>
	/// Wrapper
	/// </summary>
	internal class InternalSeriesPrice
	{
		[JsonPropertyName("seriesId")]
		public string SeriesId { get; set; }

		[JsonPropertyName("seriesType")]
		public SeriesType SeriesType { get; set; }

		[JsonPropertyName("dataItem")]
		public JsonDocument DataItem { get; set; }
	}

	/// <summary>
	/// Represents a mouse event.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/MouseEventParams"/>
	/// </summary>
	internal class InternalMouseEventParams
	{
		/// <summary>
		/// Time of the data at the location of the mouse event.<br/>
		/// The value will be null if the location of the event in the chart is outside the range of available data.
		/// </summary>
		[JsonPropertyName("time")]
		public long? Time { get; set; }

		/// <summary>
		/// Logical index.
		/// </summary>
		[JsonPropertyName("logical")]
		public double? Logical { get; set; }

		/// <summary>
		/// Location of the event in the chart.<br/>
		/// The value will be undefined if the event is fired outside the chart, for example a mouse leave event.
		/// </summary>
		[JsonPropertyName("point")]
		public Point Point { get; set; }

		/// <summary>
		/// The ISeriesApi for the series at the point of the mouse event.
		/// </summary>
		[JsonPropertyName("hoveredSeries")]
		public string HoveredSeriesId { get; set; }

		/// <summary>
		/// Data of all series at the location of the event in the chart.
		/// </summary>
		[JsonPropertyName("seriesData")]
		public InternalSeriesPrice[] SeriesData { get; set; }

		/// <summary>
		/// The ID of the marker at the point of the mouse event.
		/// </summary>
		[JsonPropertyName("hoveredObjectId")]
		public string HoveredObjectId { get; set; }

		/// <summary>
		/// The underlying source mouse or touch event data, if available
		/// </summary>
		[JsonPropertyName("sourceEvent")]
		public TouchMouseEventData SourceEvent { get; set; }
	}
}
