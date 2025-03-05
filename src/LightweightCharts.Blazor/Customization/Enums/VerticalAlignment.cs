using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents a vertical alignment.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api#vertalign"/>
	/// </summary>
	[JsonConverter(typeof(VerticalAlignmentConverter))]
	public enum VerticalAlignment
	{
		/// <summary>
		/// top
		/// </summary>
		Top,

		/// <summary>
		/// top
		/// </summary>
		Center,

		/// <summary>
		/// top
		/// </summary>
		Bottom
	}

	internal class VerticalAlignmentConverter : BaseEnumJsonConverter<VerticalAlignment>
	{
		protected override Dictionary<VerticalAlignment, string> GetEnumMapping() => new Dictionary<VerticalAlignment, string>
		{
			[VerticalAlignment.Top] = "top",
			[VerticalAlignment.Center] = "center",
			[VerticalAlignment.Bottom] = "bottom",
		};
	}
}
