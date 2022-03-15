using System.Collections.Generic;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;

namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents the type of a tick mark on the time axis.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/enums/TickMarkType
	/// </summary>
	[JsonConverter(typeof(TickMarkTypeConverter))]
	public enum TickMarkType
	{
		/// <summary>
		/// The start of the year (e.g. it's the first tick mark in a year).
		/// </summary>
		Year = 0,

		/// <summary>
		/// The start of the month (e.g. it's the first tick mark in a month).
		/// </summary>
		Month = 1,

		/// <summary>
		/// The tick mark represents a day of the month.
		/// </summary>
		DayOfMonth = 2,

		/// <summary>
		/// A time without seconds.
		/// </summary>
		Time = 3,

		/// <summary>
		/// A time with seconds.
		/// </summary>
		TimeWithSeconds = 4
	}

	internal class TickMarkTypeConverter : BaseEnumJsonConverter<TickMarkType>
	{
		protected override Dictionary<TickMarkType, string> GetEnumMapping() => new Dictionary<TickMarkType, string>
		{
			[TickMarkType.Year] = "0",
			[TickMarkType.Month] = "1",
			[TickMarkType.DayOfMonth] = "2",
			[TickMarkType.Time] = "3",
			[TickMarkType.TimeWithSeconds] = "4",
		};
	}
}
