namespace LightweightCharts.Blazor.Models.Events;

/// <summary>
/// Represents rich information about the hovered chart object.<br/>
/// <see href="https://tradingview.github.io/lightweight-charts/docs/next/api/interfaces/HoveredInfo"/>
/// </summary>
public class InternalHoveredInfo
{
	/// <summary>
	/// The semantic kind of hovered item.
	/// Prefer this when you want to know what kind of geometry the cursor is over.
	/// </summary>
	public HoveredItemType Type { get; init; }

	/// <summary>
	/// The kind of source that owns the hovered target.
	/// Prefer this when you want ownership information about the hovered object.
	/// </summary>
	public HoveredInfoSourceKind SourceKind { get; init; }

	/// <summary>
	/// The kind of hovered target object.
	/// </summary>
	public HoveredInfoObjectKind ObjectKind { get; init; }

	/// <summary>
	/// The series that owns the hovered item, if any.
	/// </summary>
	public string Series { get; init; }

	/// <summary>
	/// The object id associated with the hovered item, if any.
	/// </summary>
	public string ObjectId { get; init; }

	/// <summary>
	/// The pane index where the hover was resolved.
	/// </summary>
	public int? PaneIndex { get; init; }
}
