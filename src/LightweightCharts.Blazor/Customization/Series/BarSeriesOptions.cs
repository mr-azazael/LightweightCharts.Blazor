using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents style options for a bar series.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/BarStyleOptions
	/// </summary>
	public class BarSeriesOptions : SeriesOptionsCommon
	{
		bool _ThinBars = true;
		Color _UpColor = Extensions.ParseHtmlCode("#26a69a");
		Color _DownColor = Extensions.ParseHtmlCode("#ef5350");
		bool _OpenVisible = true;

		/// <summary>
		/// Show bars as sticks.
		/// </summary>
		[JsonPropertyName("thinBars")]
		public bool ThinBars
		{
			get => _ThinBars;
			set => SetValue(value, ref _ThinBars);
		}

		/// <summary>
		/// Color of rising bars.
		/// </summary>
		[JsonPropertyName("upColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color UpColor
		{
			get => _UpColor;
			set => SetValue(value, ref _UpColor);
		}

		/// <summary>
		/// Color of falling bars.
		/// </summary>
		[JsonPropertyName("downColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color DownColor
		{
			get => _DownColor;
			set => SetValue(value, ref _DownColor);
		}

		/// <summary>
		/// Show open lines on bars.
		/// </summary>
		[JsonPropertyName("openVisible")]
		public bool OpenVisible
		{
			get => _OpenVisible;
			set => SetValue(value, ref _OpenVisible);
		}
	}
}
