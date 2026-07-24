using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Series;

namespace LightweightCharts.Blazor.Models.Events;

/// <summary>
/// Series price at the mouse location.
/// </summary>
public class SeriesData<H>
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
