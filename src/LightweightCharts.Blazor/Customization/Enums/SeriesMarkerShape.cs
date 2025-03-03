using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents the shape of a series marker.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api#seriesmarkershape"/>
	/// </summary>
	[JsonConverter(typeof(SeriesMarkerShapeConverter))]
	public enum SeriesMarkerShape
	{
		Circle,
		Square,
		ArrowUp,
		ArrowDown
	}

	internal class SeriesMarkerShapeConverter : BaseEnumJsonConverter<SeriesMarkerShape>
	{
		protected override Dictionary<SeriesMarkerShape, string> GetEnumMapping() => new Dictionary<SeriesMarkerShape, string>
		{
			[SeriesMarkerShape.Circle] = "circle",
			[SeriesMarkerShape.Square] = "square",
			[SeriesMarkerShape.ArrowUp] = "arrowUp",
			[SeriesMarkerShape.ArrowDown] = "arrowDown"
		};
	}
}
