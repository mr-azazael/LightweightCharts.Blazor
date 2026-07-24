using LightweightCharts.Blazor.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Enums;

/// <summary>
/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ChartOptionsBase#defaultvisiblepricescaleid"/>
/// </summary>
[JsonConverter(typeof(PriceScaleIdConverter))]
public enum PriceScaleId
{
	/// <summary>
	/// 
	/// </summary>
	Right,

	/// <summary>
	/// 
	/// </summary>
	Left
}

internal class PriceScaleIdConverter : BaseEnumJsonConverter<PriceScaleId>
{
	protected override Dictionary<PriceScaleId, string> GetEnumMapping() => new()
	{
		[PriceScaleId.Right] = "right",
		[PriceScaleId.Left] = "left"
	};
}
