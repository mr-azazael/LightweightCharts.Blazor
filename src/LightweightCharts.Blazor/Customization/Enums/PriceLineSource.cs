using System.Collections.Generic;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents the source of data to be used for the horizontal price line.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/enums/PriceLineSource
	/// </summary>
	public enum PriceLineSource
	{
		/// <summary>
		/// Use the last bar data.
		/// </summary>
		LastBar = 0,

		/// <summary>
		/// Use the last visible data of the chart viewport.
		/// </summary>
		LastVisible = 1
	}

	internal class PriceLineSourceConverter : BaseEnumJsonConverter<PriceLineSource>
	{
		protected override Dictionary<PriceLineSource, string> GetEnumMapping() => new Dictionary<PriceLineSource, string>
		{
			[PriceLineSource.LastBar] = "0",
			[PriceLineSource.LastVisible] = "1",
		};
	}
}
