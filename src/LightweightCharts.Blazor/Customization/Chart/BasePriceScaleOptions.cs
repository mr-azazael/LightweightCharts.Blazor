using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure that describes price scale options for all types of price scales.<br/>
	/// Visible and AutoScale are implemented in <see cref="PriceScaleOptions"/>.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceScaleOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<BasePriceScaleOptions>))]
	public class BasePriceScaleOptions : JsonOptionsObject
	{
		/// <summary>
		/// Price scale mode.
		/// </summary>
		[JsonPropertyName("mode")]
		public PriceScaleMode Mode
		{
			get => GetValue(() => PriceScaleMode.Normal);
			set => SetValue(value);
		}

		/// <summary>
		/// Invert the price scale, so that a upwards trend is shown as a downwards trend and vice versa.<br/>
		/// Affects both the price scale and the data on the chart.
		/// </summary>
		[JsonPropertyName("invertScale")]
		public bool InvertScale
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Align price scale labels to prevent them from overlapping.
		/// </summary>
		[JsonPropertyName("alignLabels")]
		public bool AlignLabels
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Price scale margins.
		/// </summary>
		[JsonPropertyName("scaleMargins")]
		public PriceScaleMargins ScaleMargins
		{
			get => GetValue(() => new PriceScaleMargins());
			set => SetValue(value);
		}

		/// <summary>
		/// Set true to draw a border between the price scale and the chart area.
		/// </summary>
		[JsonPropertyName("borderVisible")]
		public bool BorderVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Price scale border color.
		/// </summary>
		[JsonPropertyName("borderColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BorderColor
		{
			get => GetValue(() => Extensions.ParseColorCode("#2B2B43"));
			set => SetValue(value);
		}

		/// <summary>
		/// Show top and bottom corner labels only if entire text is visible.
		/// </summary>
		[JsonPropertyName("entireTextOnly")]
		public bool EntireTextOnly
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Draw small horizontal line on price axis labels.
		/// </summary>
		[JsonPropertyName("ticksVisible")]
		public bool TicksVisible
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}
	}
}
