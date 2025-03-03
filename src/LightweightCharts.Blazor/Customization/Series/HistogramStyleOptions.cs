using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents style options for a histogram series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/HistogramStyleOptions"/>
	/// </summary>
	public class HistogramStyleOptions : SeriesOptionsCommon
	{
		Color _Color = Extensions.ParseHtmlCode("#26a69a");
		int _Base;

		/// <summary>
		/// Column color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
		{
			get => _Color;
			set => SetValue(value, ref _Color);
		}

		/// <summary>
		/// Initial level of histogram columns.
		/// </summary>
		[JsonPropertyName("base")]
		public int Base
		{
			get => _Base;
			set => SetValue(value, ref _Base);
		}
	}
}
