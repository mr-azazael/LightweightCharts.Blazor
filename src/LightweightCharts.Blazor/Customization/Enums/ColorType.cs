using LightweightCharts.Blazor.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents a type of color.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/enums/ColorType#verticalgradient
	/// </summary>
	[JsonConverter(typeof(ColorTypeConverter))]
	public enum ColorType
	{
		/// <summary>
		/// Solid color.
		/// </summary>
		Solid = 0,

		/// <summary>
		/// Vertical gradient color.
		/// </summary>
		VerticalGradient = 1
	}

	internal class ColorTypeConverter : BaseEnumJsonConverter<ColorType>
	{
		protected override Dictionary<ColorType, string> GetEnumMapping() => new Dictionary<ColorType, string>
		{
			[ColorType.Solid] = "solid",
			[ColorType.VerticalGradient] = "gradient"
		};
	}
}
