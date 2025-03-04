using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents style options for a histogram series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/HistogramStyleOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<HistogramStyleOptions>))]
	public class HistogramStyleOptions : SeriesOptionsCommon
	{
		/// <summary>
		/// Column color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#26a69a"));
			set => SetValue(value);
		}

		/// <summary>
		/// Initial level of histogram columns.
		/// </summary>
		[JsonPropertyName("base")]
		public int Base
		{
			get => GetValue<int>();
			set => SetValue(value);
		}
	}
}
