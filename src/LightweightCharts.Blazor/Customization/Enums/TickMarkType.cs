namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Represents the type of a tick mark on the time axis.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/enumerations/TickMarkType"/>
	/// </summary>
	public enum TickMarkType
	{
		/// <summary>
		/// The start of the year (e.g. it's the first tick mark in a year).
		/// </summary>
		Year,

		/// <summary>
		/// The start of the month (e.g. it's the first tick mark in a month).
		/// </summary>
		Month,

		/// <summary>
		/// A day of the month.
		/// </summary>
		DayOfMonth,

		/// <summary>
		/// A time without seconds.
		/// </summary>
		Time,

		/// <summary>
		/// A time with seconds.
		/// </summary>
		TimeWithSeconds
	}
}
