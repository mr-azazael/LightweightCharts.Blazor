using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure that describes price scale options for all types of price scales.<br/>
	/// Visible and AutoScale are implemented in <see cref="PriceScaleOptions"/>.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceScaleOptions
	/// </summary>
	public class BasePriceScaleOptions : BaseModel
	{
		PriceScaleMode _Mode = PriceScaleMode.Normal;
		bool _InvertScale;
		bool _AlignLabels = true;
		bool _BorderVisible = true;
		Color _BorderColor = Extensions.ParseHtmlCode("#2B2B43");
		PriceScaleMargins _ScaleMargins = new();
		bool _EntireTextOnly;
		bool _TicksVisible;

		/// <summary>
		/// Price scale mode.
		/// </summary>
		[JsonPropertyName("mode")]
		public PriceScaleMode Mode
		{
			get => _Mode;
			set => SetValue(value, ref _Mode);
		}

		/// <summary>
		/// Invert the price scale, so that a upwards trend is shown as a downwards trend and vice versa.<br/>
		/// Affects both the price scale and the data on the chart.
		/// </summary>
		[JsonPropertyName("invertScale")]
		public bool InvertScale
		{
			get => _InvertScale;
			set => SetValue(value, ref _InvertScale);
		}

		/// <summary>
		/// Align price scale labels to prevent them from overlapping.
		/// </summary>
		[JsonPropertyName("alignLabels")]
		public bool AlignLabels
		{
			get => _AlignLabels;
			set => SetValue(value, ref _AlignLabels);
		}

		/// <summary>
		/// Price scale margins.
		/// </summary>
		[JsonPropertyName("scaleMargins")]
		public PriceScaleMargins ScaleMargins
		{
			get => _ScaleMargins;
			set => SetValue(value, ref _ScaleMargins);
		}

		/// <summary>
		/// Set true to draw a border between the price scale and the chart area.
		/// </summary>
		[JsonPropertyName("borderVisible")]
		public bool BorderVisible
		{
			get => _BorderVisible;
			set => SetValue(value, ref _BorderVisible);
		}

		/// <summary>
		/// Price scale border color.
		/// </summary>
		[JsonPropertyName("borderColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BorderColor
		{
			get => _BorderColor;
			set => SetValue(value, ref _BorderColor);
		}

		/// <summary>
		/// Show top and bottom corner labels only if entire text is visible.
		/// </summary>
		[JsonPropertyName("entireTextOnly")]
		public bool EntireTextOnly
		{
			get => _EntireTextOnly;
			set => SetValue(value, ref _EntireTextOnly);
		}

		/// <summary>
		/// Draw small horizontal line on price axis labels.
		/// </summary>
		[JsonPropertyName("ticksVisible")]
		public bool TicksVisible
		{
			get => _TicksVisible;
			set => SetValue(value, ref _TicksVisible);
		}
	}
}
