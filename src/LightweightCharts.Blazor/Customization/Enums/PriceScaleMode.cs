using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents the price scale mode.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/enums/PriceScaleMode
	/// </summary>
	[JsonConverter(typeof(PriceScaleModeConverter))]
	public enum PriceScaleMode
	{
		/// <summary>
		/// Price scale shows prices. Price range changes linearly.
		/// </summary>
		Normal = 0,

		/// <summary>
		/// Price scale shows prices. Price range changes logarithmically.
		/// </summary>
		Logarithmic = 1,

		/// <summary>
		/// Price scale shows percentage values according the first visible value of the price scale. The first visible value is 0% in this mode.
		/// </summary>
		Percentage = 2,

		/// <summary>
		/// The same as percentage mode, but the first value is moved to 100.
		/// </summary>
		Indexed = 3
	}

	internal class PriceScaleModeConverter : BaseEnumJsonConverter<PriceScaleMode>
	{
		protected override Dictionary<PriceScaleMode, string> GetEnumMapping() => new Dictionary<PriceScaleMode, string>
		{
			[PriceScaleMode.Normal] = "0",
			[PriceScaleMode.Logarithmic] = "1",
			[PriceScaleMode.Percentage] = "2",
			[PriceScaleMode.Indexed] = "3"
		};
	}
}
