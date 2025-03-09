using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Series;

namespace LightweightCharts.Blazor.Models.Events
{
	/// <summary>
	/// Series price at the mouse location.
	/// </summary>
	public class SeriesPrice<H>
		where H : struct
	{
		/// <summary>
		/// The series to which this price point belongs to.
		/// </summary>
		public ISeriesApi<H> SeriesApi { get; init; }

		/// <summary>
		/// Pricd data at current mouse point.
		/// </summary>
		public WhitespaceData<H> DataItem { get; init; }
	}

	/// <summary>
	/// Represents a mouse event.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/MouseEventParams"/>
	/// </summary>
	public class MouseEventParams<H>
		where H : struct
	{
		/// <summary>
		/// Time of the data at the location of the mouse event.<br/>
		/// The value will be null if the location of the event in the chart is outside the range of available data.
		/// </summary>
		public H? Time { get; init; }

		/// <summary>
		/// Logical index.
		/// </summary>
		public double? Logical { get; set; }

		/// <summary>
		/// Location of the event in the chart.<br/>
		/// The value will be undefined if the event is fired outside the chart, for example a mouse leave event.
		/// </summary>
		public Point Point { get; init; }

		/// <summary>
		/// The index of the Pane.
		/// </summary>
		int? PaneIndex { get; set; }

		/// <summary>
		/// Prices of all series at the location of the event in the chart.
		/// </summary>
		public SeriesPrice<H>[] SeriesPrices { get; init; }

		/// <summary>
		/// The ISeriesApi for the series at the point of the mouse event.
		/// </summary>
		public ISeriesApi<H> HoveredSeries { get; init; }

		/// <summary>
		/// The ID of the marker at the point of the mouse event.
		/// </summary>
		public string HoveredObjectId { get; init; }

		/// <summary>
		/// The underlying source mouse or touch event data, if available
		/// </summary>
		public TouchMouseEventData SourceEvent { get; set; }
	}
}
