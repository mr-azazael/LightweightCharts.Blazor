namespace LightweightCharts.Blazor.Models.Events
{
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
		public double? Logical { get; init; }

		/// <summary>
		/// Location of the event in the chart.<br/>
		/// The value will be undefined if the event is fired outside the chart, for example a mouse leave event.
		/// </summary>
		public Point Point { get; init; }

		/// <summary>
		/// The index of the Pane.
		/// </summary>
		public int? PaneIndex { get; init; }

		/// <summary>
		/// Prices of all series at the location of the event in the chart.
		/// </summary>
		public SeriesData<H>[] SeriesData { get; init; }

		/// <summary>
		/// Rich information about the hovered item and its owner.
		/// </summary>
		public HoveredInfo<H> HoveredInfo { get; init; }

		/// <summary>
		/// The underlying source mouse or touch event data, if available
		/// </summary>
		public TouchMouseEventData SourceEvent { get; init; }
	}
}
