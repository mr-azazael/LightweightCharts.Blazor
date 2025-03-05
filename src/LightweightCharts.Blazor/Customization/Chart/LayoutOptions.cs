using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents layout options.
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/LayoutOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<LayoutOptions>))]
	public class LayoutOptions : JsonOptionsObject
	{
		/// <summary>
		/// Chart and scales background color.
		/// </summary>
		[JsonPropertyName("background")]
		public Background Background
		{
			get => GetValue<Background>(() => new SolidColor { Color = Color.White });
			set => SetValue(value);
		}

		/// <summary>
		/// Color of text on the scales.
		/// </summary>
		[JsonPropertyName("textColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TextColor
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#191919"));
			set => SetValue(value);
		}

		/// <summary>
		/// Font size of text on scales in pixels.
		/// </summary>
		[JsonPropertyName("fontSize")]
		public int FontSize
		{
			get => GetValue(() => 12);
			set => SetValue(value);
		}

		/// <summary>
		/// Font family of text on the scales.
		/// </summary>
		[JsonPropertyName("fontFamily")]
		public string FontFamily
		{
			get => GetValue(() => "-apple-system, BlinkMacSystemFont, 'Trebuchet MS', Roboto, Ubuntu, sans-serif");
			set => SetValue(value);
		}
	}
}
