using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Common class for <see cref="LayoutOptions.Background"/>.<br/>
	/// Types that extend this class: <see cref="SolidColor"/>, <see cref="VerticalGradientColor"/>.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api#background
	/// </summary>
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
	[JsonDerivedType(typeof(SolidColor), typeDiscriminator: "solid")]
	[JsonDerivedType(typeof(VerticalGradientColor), typeDiscriminator: "gradient")]
	public abstract class Background : BaseModel
	{
		/// <summary>
		/// Type of color.
		/// </summary>
		[JsonPropertyName("type")]
		public abstract ColorType Type { get; }
	}

	/// <summary>
	/// Represents a solid color.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/SolidColor
	/// </summary>
	public class SolidColor : Background
	{
		Color _Color = Color.Transparent;

		public override ColorType Type => ColorType.Solid;

		/// <summary>
		/// Color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
		{
			get => _Color;
			set => SetValue(value, ref _Color);
		}
	}

	/// <summary>
	/// Represents a vertical gradient of two colors.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/VerticalGradientColor
	/// </summary>
	public class VerticalGradientColor : Background
	{
		Color _TopColor = Color.Transparent;
		Color _BottomColor = Color.Transparent;

		public override ColorType Type => ColorType.VerticalGradient;

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
