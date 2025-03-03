using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents style options for a candlestick series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CandlestickStyleOptions"/>
	/// </summary>
	public class CandlestickStyleOptions : SeriesOptionsCommon
	{
		Color _UpColor = Extensions.ParseHtmlCode("#26a69a");
		Color _DownColor = Extensions.ParseHtmlCode("#ef5350");
		bool _WickVisible = true;
		bool _BorderVisible = true;
		Color _BorderColor = Extensions.ParseHtmlCode("#378658");
		Color _BorderUpColor = Extensions.ParseHtmlCode("#26a69a");
		Color _BorderDownColor = Extensions.ParseHtmlCode("#ef5350");
		Color _WickColor = Extensions.ParseHtmlCode("#737375");
		Color _WickUpColor = Extensions.ParseHtmlCode("#26a69a");
		Color _WickDownColor = Extensions.ParseHtmlCode("#ef5350");

		/// <summary>
		/// Color of rising candles.
		/// </summary>
		[JsonPropertyName("upColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color UpColor
		{
			get => _UpColor;
			set => SetValue(value, ref _UpColor);
		}

		/// <summary>
		/// Color of falling candles.
		/// </summary>
		[JsonPropertyName("downColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color DownColor
		{
			get => _DownColor;
			set => SetValue(value, ref _DownColor);
		}

		/// <summary>
		/// Enable high and low prices candle wicks.
		/// </summary>
		[JsonPropertyName("wickVisible")]
		public bool WickVisible
		{
			get => _WickVisible;
			set => SetValue(value, ref _WickVisible);
		}

		/// <summary>
		/// Enable candle borders.
		/// </summary>
		[JsonPropertyName("borderVisible")]
		public bool BorderVisible
		{
			get => _BorderVisible;
			set => SetValue(value, ref _BorderVisible);
		}

		/// <summary>
		/// Border color.
		/// </summary>
		[JsonPropertyName("borderColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BorderColor
		{
			get => _BorderColor;
			set => SetValue(value, ref _BorderColor);
		}

		/// <summary>
		/// Border color of rising candles.
		/// </summary>
		[JsonPropertyName("borderUpColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BorderUpColor
		{
			get => _BorderUpColor;
			set => SetValue(value, ref _BorderUpColor);
		}

		/// <summary>
		/// Border color of falling candles.
		/// </summary>
		[JsonPropertyName("borderDownColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BorderDownColor
		{
			get => _BorderDownColor;
			set => SetValue(value, ref _BorderDownColor);
		}

		/// <summary>
		/// Wick color.
		/// </summary>
		[JsonPropertyName("wickColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color WickColor
		{
			get => _WickColor;
			set => SetValue(value, ref _WickColor);
		}

		/// <summary>
		/// Wick color of rising candles.
		/// </summary>
		[JsonPropertyName("wickUpColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color WickUpColor
		{
			get => _WickUpColor;
			set => SetValue(value, ref _WickUpColor);
		}

		/// <summary>
		/// Wick color of falling candles.
		/// </summary>
		[JsonPropertyName("wickDownColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color WickDownColor
		{
			get => _WickDownColor;
			set => SetValue(value, ref _WickDownColor);
		}
	}
}
