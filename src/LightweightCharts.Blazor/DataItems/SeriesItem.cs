using System;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	/// base interface for all <see cref="DateTime"/> data points
	/// </summary>
	public interface ISeriesData
	{
		/// <summary>
		/// The time of the data in unix format.
		/// </summary>
		long UnixTime { get; }

		/// <summary>
		/// The time of the data as a <see cref="DateTime"/>.
		/// </summary>
		DateTime Time { get; }
	}
}
