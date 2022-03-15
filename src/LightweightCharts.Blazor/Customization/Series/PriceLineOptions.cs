using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents a price line options.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceLineOptions
	/// </summary>
	public class PriceLineOptions : BaseModel
	{
		double _Price;
		Color _Color = Color.Transparent;
		int _LineWidth = 1;
		LineStyle _LineStyle = LineStyle.Solid;
		bool _LineVisible = true;
		bool _AxisLabelVisible = true;
		string _Title;

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
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
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
	}
}
