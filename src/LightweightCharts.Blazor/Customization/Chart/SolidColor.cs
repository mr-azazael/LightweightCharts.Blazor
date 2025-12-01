using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents a solid color.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/SolidColor"/>
	/// </summary>
	public class SolidColor : Background
	{
		/// <summary>
		/// <inheritdoc/>
		/// </summary>
		[JsonIgnore]
        public override ColorType Type
			=> ColorType.Solid;

		/// <summary>
		/// Color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color { get; set; } = Color.Transparent;
	}
}
