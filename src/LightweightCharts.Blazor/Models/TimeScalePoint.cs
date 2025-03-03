using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models
{
	/// <summary>
	/// Represents a point on the time scale.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/TimeScalePoint"/>
	/// </summary>
	public class TimeScalePoint
	{
		/// <summary>
		/// Weight of the point.
		/// </summary>
		[JsonPropertyName("timeWeight")]
		public long TimeWeight { get; init; }

		/// <summary>
		/// Time of the point.
		/// </summary>
		[JsonPropertyName("time")]
		public long Time { get; init; }

		/// <summary>
		/// Original time for the point.
		/// </summary>
		[JsonPropertyName("originalTime")]
		public long OriginalTime { get; init; }
	}
}
