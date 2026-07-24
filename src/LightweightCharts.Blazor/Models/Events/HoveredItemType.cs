using LightweightCharts.Blazor.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models.Events;

/// <summary>
/// <see href="https://tradingview.github.io/lightweight-charts/docs/next/api/type-aliases/HoveredItemType"/>
/// </summary>
[JsonConverter(typeof(HoveredItemTypeConverter))]
public enum HoveredItemType
{
	/// <summary>
	/// 
	/// </summary>
	SeriesPoint,

	/// <summary>
	/// 
	/// </summary>
	SeriesLine,

	/// <summary>
	/// 
	/// </summary>
	SeriesRange,

	/// <summary>
	/// 
	/// </summary>
	Marker,

	/// <summary>
	/// 
	/// </summary>
	PriceLine,

	/// <summary>
	/// 
	/// </summary>
	Primitive,

	/// <summary>
	/// 
	/// </summary>
	Custom
}

internal class HoveredItemTypeConverter : BaseEnumJsonConverter<HoveredItemType>
{
	protected override Dictionary<HoveredItemType, string> GetEnumMapping() => new()
	{
		[HoveredItemType.SeriesPoint] = "series-point",
		[HoveredItemType.SeriesLine] = "series-line",
		[HoveredItemType.SeriesRange] = "series-range",
		[HoveredItemType.Marker] = "marker",
		[HoveredItemType.PriceLine] = "price-line",
		[HoveredItemType.Primitive] = "primitive",
		[HoveredItemType.Custom] = "custom"
	};
}
