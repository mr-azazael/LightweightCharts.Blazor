using LightweightCharts.Blazor.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// The visual stacking order for the markers within the chart.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/next/api/type-aliases/SeriesMarkerZOrder"/>
	/// </summary>
	[JsonConverter(typeof(SeriesMarkerZOrderConverter))]
	public enum SeriesMarkerZOrder
	{
		/// <summary>
		/// Markers are drawn on the topmost primitive layer, above all series and (most) other primitives.
		/// </summary>
		Top,

		/// <summary>
		/// Markers are drawn above all series but below primitives that use the 'top' zOrder layer.
		/// </summary>
		AboveSeries,

		/// <summary>
		/// Markers are drawn together with the series they belong to. They can appear below other series depending on the series stacking order.
		/// </summary>
		Normal
	}

	internal class SeriesMarkerZOrderConverter : BaseEnumJsonConverter<SeriesMarkerZOrder>
	{
		protected override Dictionary<SeriesMarkerZOrder, string> GetEnumMapping() => new Dictionary<SeriesMarkerZOrder, string>
		{
			[SeriesMarkerZOrder.Top] = "top",
			[SeriesMarkerZOrder.AboveSeries] = "aboveSeries",
			[SeriesMarkerZOrder.Normal] = "normal"
		};
	}
}
