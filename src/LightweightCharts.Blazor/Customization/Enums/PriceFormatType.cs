using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceFormatBuiltIn#type
	/// </summary>
	[JsonConverter(typeof(PriceFormatTypeConverter))]
	public enum PriceFormatType
	{
		/// <summary>
		/// <see cref="Price"/> format, is the most common choice; it allows customization of precision and rounding of prices.
		/// </summary>
		Price,

		/// <summary>
		/// <see cref="Volumne"/> format uses abbreviation for formatting prices like 1.2K or 12.67M.
		/// </summary>
		Volumne,

		/// <summary>
		/// <see cref="Percent"/> uses % sign at the end of prices.
		/// </summary>
		Percent
	}

	internal class PriceFormatTypeConverter : BaseEnumJsonConverter<PriceFormatType>
	{
		protected override Dictionary<PriceFormatType, string> GetEnumMapping() => new Dictionary<PriceFormatType, string>
		{
			[PriceFormatType.Price] = "price",
			[PriceFormatType.Volumne] = "volume",
			[PriceFormatType.Percent] = "percent"
		};
	}
}
