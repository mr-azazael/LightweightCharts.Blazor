using System;

namespace LightweightCharts.Blazor.DataItems
{
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
