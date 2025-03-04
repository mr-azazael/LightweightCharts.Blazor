using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents style options for a candlestick series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CandlestickStyleOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<CandlestickStyleOptions>))]
	public class CandlestickStyleOptions : SeriesOptionsCommon
	{
		/// <summary>
		/// Color of rising candles.
		/// </summary>
		[JsonPropertyName("upColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color UpColor
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#26a69a"));
			set => SetValue(value);
		}

		/// <summary>
		/// Color of falling candles.
		/// </summary>
		[JsonPropertyName("downColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color DownColor
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#ef5350"));
			set => SetValue(value);
		}

		/// <summary>
		/// Enable high and low prices candle wicks.
		/// </summary>
		[JsonPropertyName("wickVisible")]
		public bool WickVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Enable candle borders.
		/// </summary>
		[JsonPropertyName("borderVisible")]
		public bool BorderVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Border color.
		/// </summary>
		[JsonPropertyName("borderColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BorderColor
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#378658"));
			set => SetValue(value);
		}

		/// <summary>
		/// Border color of rising candles.
		/// </summary>
		[JsonPropertyName("borderUpColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BorderUpColor
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#26a69a"));
			set => SetValue(value);
		}

		/// <summary>
		/// Border color of falling candles.
		/// </summary>
		[JsonPropertyName("borderDownColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BorderDownColor
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#ef5350"));
			set => SetValue(value);
		}

		/// <summary>
		/// Wick color.
		/// </summary>
		[JsonPropertyName("wickColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color WickColor
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#737375"));
			set => SetValue(value);
		}

		/// <summary>
		/// Wick color of rising candles.
		/// </summary>
		[JsonPropertyName("wickUpColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color WickUpColor
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#26a69a"));
			set => SetValue(value);
		}

		/// <summary>
		/// Wick color of falling candles.
		/// </summary>
		[JsonPropertyName("wickDownColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color WickDownColor
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#ef5350"));
			set => SetValue(value);
		}
	}
}
