using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents style options for a bar series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/BarStyleOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<BarStyleOptions>))]
	public class BarStyleOptions : SeriesOptionsCommon
	{
		/// <summary>
		/// Color of rising bars.
		/// </summary>
		[JsonPropertyName("upColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color UpColor
		{
			get => GetValue(() => Extensions.ParseColorCode("#26a69a"));
			set => SetValue(value);
		}

		/// <summary>
		/// Color of falling bars.
		/// </summary>
		[JsonPropertyName("downColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color DownColor
		{
			get => GetValue(() => Extensions.ParseColorCode("#ef5350"));
			set => SetValue(value);
		}

		/// <summary>
		/// Show open lines on bars.
		/// </summary>
		[JsonPropertyName("openVisible")]
		public bool OpenVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Show bars as sticks.
		/// </summary>
		[JsonPropertyName("thinBars")]
		public bool ThinBars
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}
	}
}
