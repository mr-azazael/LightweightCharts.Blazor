using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models.Events
{
	/// <summary>
	/// Represents a point on the chart.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/Point
	/// </summary>
	public class Point
	{
		/// <summary>
		/// The x coordinate.
		/// </summary>
		[JsonPropertyName("x")]
		public double X { get; set; }

		/// <summary>
		/// The y coordinate.
		/// </summary>
		[JsonPropertyName("y")]
		public double Y { get; set; }
	}
}
