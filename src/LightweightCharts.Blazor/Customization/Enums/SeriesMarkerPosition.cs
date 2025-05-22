using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents the position of a series marker relative to a bar.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/type-aliases/SeriesMarkerBarPosition"/>
	/// </summary>
	[JsonConverter(typeof(SeriesMarkerBarPositionConverter))]
	public enum SeriesMarkerBarPosition
	{
		/// <summary>
		/// Marker is above bar.
		/// </summary>
		AboveBar,

		/// <summary>
		/// Marker is below bar.
		/// </summary>
		BelowBar,

		/// <summary>
		/// Marker is centered on the bar.
		/// </summary>
		InBar
	}

	/// <summary>
	/// Represents the position of a series marker relative to a price.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/type-aliases/SeriesMarkerPricePosition"/>
	/// </summary>
	[JsonConverter(typeof(SeriesMarkerPricePositionConverter))]
	public enum SeriesMarkerPricePosition
	{
		/// <summary>
		/// Marker is at price top.
		/// </summary>
		AtPriceTop,

		/// <summary>
		/// Marker is at price bottom.
		/// </summary>
		AtPriceBottom,

		/// <summary>
		/// Marker is at price middle.
		/// </summary>
		AtPriceMiddle
	}

	internal class SeriesMarkerBarPositionConverter : BaseEnumJsonConverter<SeriesMarkerBarPosition>
	{
		protected override Dictionary<SeriesMarkerBarPosition, string> GetEnumMapping() => new Dictionary<SeriesMarkerBarPosition, string>
		{
			[SeriesMarkerBarPosition.AboveBar] = "aboveBar",
			[SeriesMarkerBarPosition.BelowBar] = "belowBar",
			[SeriesMarkerBarPosition.InBar] = "inBar"
		};
	}

	internal class SeriesMarkerPricePositionConverter : BaseEnumJsonConverter<SeriesMarkerPricePosition>
	{
		protected override Dictionary<SeriesMarkerPricePosition, string> GetEnumMapping() => new Dictionary<SeriesMarkerPricePosition, string>
		{
			[SeriesMarkerPricePosition.AtPriceTop] = "atPriceTop",
			[SeriesMarkerPricePosition.AtPriceBottom] = "atPriceBottom",
			[SeriesMarkerPricePosition.AtPriceMiddle] = "atPriceMiddle"
		};
	}
}
