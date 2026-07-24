using LightweightCharts.Blazor.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models.Events;

/// <summary>
/// The kind of hovered target object.<br/>
/// <see href="https://tradingview.github.io/lightweight-charts/docs/next/api/interfaces/HoveredInfo#objectkind"/>
/// </summary>
[JsonConverter(typeof(HoveredInfoObjectKindConverter))]
public enum HoveredInfoObjectKind
{
	/// <summary>
	/// 
	/// </summary>
	Primitive,

	/// <summary>
	/// 
	/// </summary>
	Series,

	/// <summary>
	/// 
	/// </summary>
	CustomObject,

	/// <summary>
	/// 
	/// </summary>
	CustomPriceLine,

	/// <summary>
	/// 
	/// </summary>
	SeriesMarker
}

internal class HoveredInfoObjectKindConverter : BaseEnumJsonConverter<HoveredInfoObjectKind>
{
	protected override Dictionary<HoveredInfoObjectKind, string> GetEnumMapping() => new()
	{
		[HoveredInfoObjectKind.Primitive] = "primitive",
		[HoveredInfoObjectKind.Series] = "series",
		[HoveredInfoObjectKind.CustomObject] = "custom-object",
		[HoveredInfoObjectKind.CustomPriceLine] = "custom-price-line",
		[HoveredInfoObjectKind.SeriesMarker] = "series-marker"
	};
}
