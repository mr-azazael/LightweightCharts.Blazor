using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents the possible line styles.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/enums/LineStyle
	/// </summary>
	[JsonConverter(typeof(LineStyleConverter))]
	public enum LineStyle
	{
		/// <summary>
		/// A solid line.
		/// </summary>
		Solid = 0,

		/// <summary>
		/// A dotted line.
		/// </summary>
		Dotted = 1,

		/// <summary>
		/// A dashed line.
		/// </summary>
		Dashed = 2,

		/// <summary>
		/// A dashed line with bigger dashes.
		/// </summary>
		LargeDashed = 3,

		/// <summary>
		/// A dottled line with more space between dots.
		/// </summary>
		SparseDotted = 4
	}

	internal class LineStyleConverter : BaseEnumJsonConverter<LineStyle>
	{
		protected override Dictionary<LineStyle, string> GetEnumMapping() => new Dictionary<LineStyle, string>
		{
			[LineStyle.Solid] = "0",
			[LineStyle.Dotted] = "1",
			[LineStyle.Dashed] = "2",
			[LineStyle.LargeDashed] = "3",
			[LineStyle.SparseDotted] = "4"
		};
	}
}
