using LightweightCharts.Blazor.Customization.Enums;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models.Events;

/// <summary>
/// Wrapper
/// </summary>
public class InternalSeriesPrice
{
	/// <summary>
	/// Series Id
	/// </summary>
	[JsonPropertyName("seriesId")]
	public string SeriesId { get; init; }

	/// <summary>
	/// Series type
	/// </summary>
	[JsonPropertyName("seriesType")]
	public SeriesType SeriesType { get; init; }

	/// <summary>
	/// Json serialization of a series data item
	/// </summary>
	[JsonPropertyName("dataItem")]
	public JsonDocument DataItem { get; init; }
}
