using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents layout options.
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/LayoutOptions
	/// </summary>
	public class LayoutOptions : BaseModel
	{
		Background _Background = new SolidColor { Color = Color.White };
		Color _TextColor = Extensions.ParseHtmlCode("#191919");
		int _FontSize = 11;
		string _FontFamily = "Trebuchet MS";

		/// <summary>
		/// Chart and scales background color.
		/// </summary>
		[JsonPropertyName("background")]
		public Background Background
		{
			get => _Background;
			set => SetValue(value, ref _Background);
		}

		/// <summary>
		/// Color of text on the scales.
		/// </summary>
		[JsonPropertyName("textColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TextColor
		{
			get => _TextColor;
			set => SetValue(value, ref _TextColor);
		}

		/// <summary>
		/// Font size of text on scales in pixels.
		/// </summary>
		[JsonPropertyName("fontSize")]
		public int FontSize
		{
			get => _FontSize;
			set => SetValue(value, ref _FontSize);
		}

		/// <summary>
		/// Font family of text on the scales.
		/// </summary>
		[JsonPropertyName("fontFamily")]
		public string FontFamily
		{
			get => _FontFamily;
			set => SetValue(value, ref _FontFamily);
		}
	}
}
