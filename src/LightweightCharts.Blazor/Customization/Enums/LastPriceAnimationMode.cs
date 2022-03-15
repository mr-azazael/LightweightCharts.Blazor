using LightweightCharts.Blazor.Converters;
using System.Collections.Generic;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents the type of the last price animation for series such as area or line.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/enums/LastPriceAnimationMode
	/// </summary>
	public enum LastPriceAnimationMode
	{
		/// <summary>
		/// Animation is always disabled.
		/// </summary>
		Disabled = 0,

		/// <summary>
		/// Animation is always enabled.
		/// </summary>
		Continuous = 1,

		/// <summary>
		/// Animation is active after new data.
		/// </summary>
		OnDataUpdate = 2
	}

	internal class LastPriceAnimationModeConverter : BaseEnumJsonConverter<LastPriceAnimationMode>
	{
		protected override Dictionary<LastPriceAnimationMode, string> GetEnumMapping() => new Dictionary<LastPriceAnimationMode, string>
		{
			[LastPriceAnimationMode.Disabled] = "0",
			[LastPriceAnimationMode.Continuous] = "1",
			[LastPriceAnimationMode.OnDataUpdate] = "2"
		};
	}
}
