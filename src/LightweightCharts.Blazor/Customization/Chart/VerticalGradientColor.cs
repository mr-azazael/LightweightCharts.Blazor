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
		Color _TopColor = Color.Transparent;
		Color _BottomColor = Color.Transparent;

		public override ColorType Type 
			=> ColorType.VerticalGradient;

		/// <summary>
		/// Top color.
		/// </summary>
		[JsonPropertyName("topColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TopColor
		{
			get => _TopColor;
			set => SetValue(value, ref _TopColor);
		}

		/// <summary>
		/// Bottom color.
		/// </summary>
		[JsonPropertyName("bottomColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BottomColor
		{
			get => _BottomColor;
			set => SetValue(value, ref _BottomColor);
		}
	}
}
