using LightweightCharts.Blazor.Converters;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents a type of color.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/enums/ColorType"/>
	/// </summary>
	[JsonConverter(typeof(ColorTypeConverter))]
	public enum ColorType
	{
		/// <summary>
		/// Solid color.
		/// </summary>
		Solid,

		/// <summary>
		/// Vertical gradient color.
		/// </summary>
		VerticalGradient
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
