using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents the crosshair mode.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/enums/CrosshairMode
	/// </summary>
	[JsonConverter(typeof(CrosshairModeConverter))]
	public enum CrosshairMode
	{
		/// <summary>
		/// This mode allows crosshair to move freely on the chart.
		/// </summary>
		Normal = 0,

		/// <summary>
		/// This mode sticks crosshair's horizontal line to the price value of a single-value series or to the close price of OHLC-based series.
		/// </summary>
		Magnet
	}

	internal class CrosshairModeConverter : BaseEnumJsonConverter<CrosshairMode>
	{
		protected override Dictionary<CrosshairMode, string> GetEnumMapping() => new Dictionary<CrosshairMode, string>
		{
			[CrosshairMode.Normal] = "0",
			[CrosshairMode.Magnet] = "1"
		};
	}
}
