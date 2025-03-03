using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure describing a crosshair line (vertical or horizontal).<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions"/>
	/// </summary>
	public class CrosshairLineOption : BaseModel
	{
		Color _Color = Extensions.ParseHtmlCode("#758696");
		double _Width = 1;
		LineStyle _Style = LineStyle.LargeDashed;
		bool _Visible = true;
		bool _LabelVisible = true;
		Color _LabelBackgroundColor = Extensions.ParseHtmlCode("#4c525e");

		/// <summary>
		/// Crosshair line color.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions#color"/>
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
		{
			get => _Color;
			set => SetValue(value, ref _Color);
		}

		/// <summary>
		/// Crosshair line width in pixels.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions#width"/>
		/// </summary>
		[JsonPropertyName("width")]
		public double Width
		{
			get => _Width;
			set => SetValue(value, ref _Width);
		}

		/// <summary>
		/// Crosshair line style.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions#style"/>
		/// </summary>
		[JsonPropertyName("style")]
		public LineStyle Style
		{
			get => _Style;
			set => SetValue(value, ref _Style);
		}

		/// <summary>
		/// Display the crosshair line.
		/// Note that disabling crosshair lines does not disable crosshair marker on Line and Area series.<br/>
		/// It can be disabled by using crosshairMarkerVisible option of a relevant series. See the link for more info.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions#visible"/>
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => _Visible;
			set => SetValue(value, ref _Visible);
		}

		/// <summary>
		/// If true, a data label is shown on a relevant scale.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions#labelvisible"/>
		/// </summary>
		[JsonPropertyName("labelVisible")]
		public bool LabelVisible
		{
			get => _LabelVisible;
			set => SetValue(value, ref _LabelVisible);
		}

		/// <summary>
		/// Crosshair label background color.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions#labelbackgroundcolor"/>
		/// </summary>
		[JsonPropertyName("labelBackgroundColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color LabelBackgroundColor
		{
			get => _LabelBackgroundColor;
			set => SetValue(value, ref _LabelBackgroundColor);
		}
	}
}
