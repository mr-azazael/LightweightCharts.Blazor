namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	/// base interface for all data points
	/// </summary>
	public interface ISeriesData<H>
		where H : struct
	{
		/// <summary>
		/// Horizontal scale value.
		/// </summary>
		H Time { get; set; }
	}
}
