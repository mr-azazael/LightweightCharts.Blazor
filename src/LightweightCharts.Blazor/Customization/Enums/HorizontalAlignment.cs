using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents a horizontal alignment.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api#horzalign"/>
	/// </summary>
	[JsonConverter(typeof(HorizontalAlignmentConverter))]
	public enum HorizontalAlignment
	{
		Left,
		Center,
		Right
	}

	internal class HorizontalAlignmentConverter : BaseEnumJsonConverter<HorizontalAlignment>
	{
		protected override Dictionary<HorizontalAlignment, string> GetEnumMapping() => new Dictionary<HorizontalAlignment, string>
		{
			[HorizontalAlignment.Left] = "left",
			[HorizontalAlignment.Center] = "center",
			[HorizontalAlignment.Right] = "right",
		};
	}
}
