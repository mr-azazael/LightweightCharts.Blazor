using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents a price line options.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceLineOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<PriceLineOptions>))]
	public class PriceLineOptions : JsonOptionsObject
	{
		/// <summary>
		/// The optional ID of this price line.
		/// </summary>
		[JsonPropertyName("id")]
		public string Id
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		/// <summary>
		/// Price line's value.
		/// </summary>
		[JsonPropertyName("price")]
		public double Price
		{
			get => GetValue<double>();
			set => SetValue(value);
		}

		/// <summary>
		/// Price line's color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? Color
		{
			get => GetValue<Color?>();
			set => SetValue(value);
		}

		/// <summary>
		/// Price line's width in pixels.
		/// </summary>
		[JsonPropertyName("lineWidth")]
		public int LineWidth
		{
			get => GetValue(() => 1);
			set => SetValue(value);
		}

		/// <summary>
		/// Price line's style.
		/// </summary>
		[JsonPropertyName("lineStyle")]
		public LineStyle LineStyle
		{
			get => GetValue(() => LineStyle.Solid);
			set => SetValue(value);
		}

		/// <summary>
		/// Display line.
		/// </summary>
		[JsonPropertyName("lineVisible")]
		public bool LineVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Display the current price value in on the price scale.
		/// </summary>
		[JsonPropertyName("axisLabelVisible")]
		public bool AxisLabelVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Price line's on the chart pane.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		/// <summary>
		/// Background color for the axis label. Will default to the price line color if unspecified.
		/// </summary>
		[JsonPropertyName("axisLabelColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? AxisLabelColor
		{
			get => GetValue<Color?>();
			set => SetValue(value);
		}

		/// <summary>
		/// Text color for the axis label.
		/// </summary>
		[JsonPropertyName("axisLabelTextColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? AxisLabelTextColor
		{
			get => GetValue<Color?>();
			set => SetValue(value);
		}
	}
}
