using LightweightCharts.Blazor.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models.Events;

/// <summary>
/// The kind of source that owns the hovered target.<br/>
/// <see href="https://tradingview.github.io/lightweight-charts/docs/next/api/interfaces/HoveredInfo#sourcekind"/>
/// </summary>
[JsonConverter(typeof(HoveredInfoSourceKindConverter))]
public enum HoveredInfoSourceKind
{
	/// <summary>
	/// 
	/// </summary>
	Series,

	/// <summary>
	/// 
	/// </summary>
	SeriesPrimitive,

	/// <summary>
	/// 
	/// </summary>
	PanePrimitive
}

internal class HoveredInfoSourceKindConverter : BaseEnumJsonConverter<HoveredInfoSourceKind>
{
	protected override Dictionary<HoveredInfoSourceKind, string> GetEnumMapping() => new()
	{
		[HoveredInfoSourceKind.Series] = "series",
		[HoveredInfoSourceKind.SeriesPrimitive] = "series-primitive",
		[HoveredInfoSourceKind.PanePrimitive] = "pane-primitive"
	};
}
