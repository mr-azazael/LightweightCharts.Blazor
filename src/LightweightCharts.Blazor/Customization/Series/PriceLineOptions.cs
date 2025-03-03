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
	public class PriceLineOptions : BaseModel
	{
		string _Id;
		double _Price;
		Color? _Color;
		int _LineWidth = 1;
		LineStyle _LineStyle = LineStyle.Solid;
		bool _LineVisible = true;
		bool _AxisLabelVisible = true;
		string _Title;
		Color? _AxisLabelColor;
		Color? _AxisLabelTextColor;

		/// <summary>
		/// The optional ID of this price line.
		/// </summary>
		[JsonPropertyName("id")]
		public string Id
		{
			get => _Id;
			set => SetValue(value, ref _Id);
		}

		/// <summary>
		/// Price line's value.
		/// </summary>
		[JsonPropertyName("price")]
		public double Price
		{
			get => _Price;
			set => SetValue(value, ref _Price);
		}

		/// <summary>
		/// Price line's color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? Color
		{
			get => _Color;
			set => SetValue(value, ref _Color);
		}

		/// <summary>
		/// Price line's width in pixels.
		/// </summary>
		[JsonPropertyName("lineWidth")]
		public int LineWidth
		{
			get => _LineWidth;
			set => SetValue(value, ref _LineWidth);
		}

		/// <summary>
		/// Price line's style.
		/// </summary>
		[JsonPropertyName("lineStyle")]
		public LineStyle LineStyle
		{
			get => _LineStyle;
			set => SetValue(value, ref _LineStyle);
		}

		/// <summary>
		/// Display line.
		/// </summary>
		[JsonPropertyName("lineVisible")]
		public bool LineVisible
		{
			get => _LineVisible;
			set => SetValue(value, ref _LineVisible);
		}

		/// <summary>
		/// Display the current price value in on the price scale.
		/// </summary>
		[JsonPropertyName("axisLabelVisible")]
		public bool AxisLabelVisible
		{
			get => _AxisLabelVisible;
			set => SetValue(value, ref _AxisLabelVisible);
		}

		/// <summary>
		/// Price line's on the chart pane.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title
		{
			get => _Title;
			set => SetValue(value, ref _Title);
		}

		/// <summary>
		/// Background color for the axis label. Will default to the price line color if unspecified.
		/// </summary>
		[JsonPropertyName("axisLabelColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? AxisLabelColor
		{
			get => _AxisLabelColor;
			set => SetValue(value, ref _AxisLabelColor);
		}

		/// <summary>
		/// Text color for the axis label.
		/// </summary>
		[JsonPropertyName("axisLabelTextColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? AxisLabelTextColor
		{
			get => _AxisLabelTextColor;
			set => SetValue(value, ref _AxisLabelTextColor);
		}
	}
}
