using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Utilities;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure describing a crosshair line (vertical or horizontal).<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<CrosshairLineOption>))]
	public class CrosshairLineOption : JsonOptionsObject
	{
		/// <summary>
		/// Crosshair line color.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions#color"/>
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
		{
			get => Extensions.ParseColorCode("#758696");
			set => SetValue(value);
		}

		/// <summary>
		/// Crosshair line width in pixels.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions#width"/>
		/// </summary>
		[JsonPropertyName("width")]
		public double Width
		{
			get => GetValue(() => 1.0);
			set => SetValue(value);
		}

		/// <summary>
		/// Crosshair line style.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions#style"/>
		/// </summary>
		[JsonPropertyName("style")]
		public LineStyle Style
		{
			get => GetValue(() => LineStyle.LargeDashed);
			set => SetValue(value);
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
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// If true, a data label is shown on a relevant scale.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions#labelvisible"/>
		/// </summary>
		[JsonPropertyName("labelVisible")]
		public bool LabelVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Crosshair label background color.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairLineOptions#labelbackgroundcolor"/>
		/// </summary>
		[JsonPropertyName("labelBackgroundColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color LabelBackgroundColor
		{
			get => GetValue(() => Extensions.ParseColorCode("#4c525e"));
			set => SetValue(value);
		}
	}
}
