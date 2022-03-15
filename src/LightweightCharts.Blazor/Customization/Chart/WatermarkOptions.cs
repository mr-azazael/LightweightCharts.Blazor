using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Watermark options.
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/WatermarkOptions
	/// </summary>
	public class WatermarkOptions : BaseModel
	{
		Color _Color = Color.Transparent;
		bool _Visible;
		string _Text;
		int _FontSize = 48;
		string _FontFamily = "Trebuchet MS";
		string _FontStyle;
		HorizontalAlignment _HorizontalAlignment = Chart.HorizontalAlignment.Center;
		VerticalAlignment _VerticalAlignment = Chart.VerticalAlignment.Center;

		/// <summary>
		/// Watermark color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
		{
			get => _Color;
			set => SetValue(value, ref _Color);
		}

		/// <summary>
		/// Display the watermark.
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => _Visible;
			set => SetValue(value, ref _Visible);
		}

		/// <summary>
		/// Text of the watermark. Word wrapping is not supported.
		/// </summary>
		[JsonPropertyName("text")]
		public string Text
		{
			get => _Text;
			set => SetValue(value, ref _Text);
		}

		/// <summary>
		/// Font size in pixels.
		/// </summary>
		[JsonPropertyName("fontSize")]
		public int FontSize
		{
			get => _FontSize;
			set => SetValue(value, ref _FontSize);
		}

		/// <summary>
		/// Font family.
		/// </summary>
		[JsonPropertyName("fontFamily")]
		public string FontFamily
		{
			get => _FontFamily;
			set => SetValue(value, ref _FontFamily);
		}

		/// <summary>
		/// Font style.
		/// </summary>
		[JsonPropertyName("fontStyle")]
		public string FontStyle
		{
			get => _FontStyle;
			set => SetValue(value, ref _FontStyle);
		}

		/// <summary>
		/// Horizontal alignment inside the chart area.
		/// </summary>
		[JsonPropertyName("horzAlign")]
		public HorizontalAlignment HorizontalAlignment
		{
			get => _HorizontalAlignment;
			set => SetValue(value, ref _HorizontalAlignment);
		}

		/// <summary>
		/// Vertical alignment inside the chart area.
		/// </summary>
		[JsonPropertyName("vertAlign")]
		public VerticalAlignment VerticalAlignment
		{
			get => _VerticalAlignment;
			set => SetValue(value, ref _VerticalAlignment);
		}
	}
}
