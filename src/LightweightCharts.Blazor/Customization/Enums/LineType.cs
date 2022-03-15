using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents the possible line types.
	/// https://tradingview.github.io/lightweight-charts/docs/api/enums/LineType
	/// </summary>
	[JsonConverter(typeof(LineTypeConverter))]
	public enum LineType
	{
		/// <summary>
		/// A line.
		/// </summary>
		Simple = 0,

		/// <summary>
		/// A stepped line.
		/// </summary>
		WithSteps = 1
	}

	internal class LineTypeConverter : BaseEnumJsonConverter<LineType>
	{
		protected override Dictionary<LineType, string> GetEnumMapping() => new Dictionary<LineType, string>
		{
			[LineType.Simple] = "0",
			[LineType.WithSteps] = "1"
		};
	}
}
