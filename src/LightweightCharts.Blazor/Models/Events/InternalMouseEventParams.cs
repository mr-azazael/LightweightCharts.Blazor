using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models.Events
{
	/// <summary>
	/// Represents a mouse event.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/MouseEventParams"/>
	/// </summary>
	public class InternalMouseEventParams<H>
		where H : struct
	{
		/// <summary>
		/// Time of the data at the location of the mouse event.<br/>
		/// The value will be null if the location of the event in the chart is outside the range of available data.
		/// </summary>
		[JsonPropertyName("time")]
		public H? Time { get; init; }

		/// <summary>
		/// Logical index.
		/// </summary>
		[JsonPropertyName("logical")]
		public double? Logical { get; init; }

		/// <summary>
		/// Location of the event in the chart.<br/>
		/// The value will be undefined if the event is fired outside the chart, for example a mouse leave event.
		/// </summary>
		[JsonPropertyName("point")]
		public Point Point { get; init; }

		/// <summary>
		/// The index of the Pane.
		/// </summary>
		[JsonPropertyName("paneIndex")]
		public int? PaneIndex { get; init; }

		/// <summary>
		/// Data of all series at the location of the event in the chart.
		/// </summary>
		[JsonPropertyName("seriesData")]
		public InternalSeriesPrice[] SeriesData { get; init; }

		/// <summary>
		/// Data of all series at the location of the event in the chart.
		/// </summary>
		[JsonPropertyName("hoveredInfo")]
		public InternalHoveredInfo HoveredInfo { get; init; }

		/// <summary>
		/// The underlying source mouse or touch event data, if available
		/// </summary>
		[JsonPropertyName("sourceEvent")]
		public TouchMouseEventData SourceEvent { get; init; }
	}
}
