using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents a vertical gradient of two colors.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/VerticalGradientColor"/>
	/// </summary>
	public class VerticalGradientColor : Background
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		public override ColorType Type
			=> ColorType.VerticalGradient;

		/// <summary>
		/// Top color.
		/// </summary>
		[JsonPropertyName("topColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TopColor { get; set; } = Color.Transparent;

		/// <summary>
		/// Bottom color.
		/// </summary>
		[JsonPropertyName("bottomColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BottomColor { get; set; } = Color.Transparent;
	}
}
