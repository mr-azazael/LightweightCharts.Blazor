using LightweightCharts.Blazor.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Enums;

/// <summary>
/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/HorzScaleOptions#precomputeconflationpriority
/// </summary>
[JsonConverter(typeof(PrecomputeConflationPriorityConverter))]
public enum PrecomputeConflationPriority
{
	/// <summary>
	/// Lowest priority, tasks run only when the browser is idle
	/// </summary>
	Background,

	/// <summary>
	/// Medium priority, tasks run when they might affect visible content
	/// </summary>
	UserVisible,

	/// <summary>
	/// Highest priority, tasks run immediately and may block user interaction
	/// </summary>
	UserBlocking
}

internal class PrecomputeConflationPriorityConverter : BaseEnumJsonConverter<PrecomputeConflationPriority>
{
	protected override Dictionary<PrecomputeConflationPriority, string> GetEnumMapping() => new()
	{
		[PrecomputeConflationPriority.Background] = "background",
		[PrecomputeConflationPriority.UserVisible] = "user-visible",
		[PrecomputeConflationPriority.UserBlocking] = "user-blocking"
	};
}
