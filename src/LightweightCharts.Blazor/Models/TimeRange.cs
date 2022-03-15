using System;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Models
{
	/// <summary>
	/// Represents a Time range.
	/// https://tradingview.github.io/lightweight-charts/docs/api#timerange
	/// </summary>
	public class TimeRange : Range<long>
	{
		DateTime _FromTime;
		DateTime _ToTime;

		protected override void OnFromChanged(long obj)
		{
			_FromTime = DateTimeOffset.FromUnixTimeSeconds(_From).DateTime;
		}

		protected override void OnToChanged(long obj)
		{
			_ToTime = DateTimeOffset.FromUnixTimeSeconds(_To).DateTime;
		}

		/// <summary>
		/// Begining of the time range, as <see cref="DateTime"/>.
		/// </summary>
		[JsonIgnore]
		public DateTime FromTime
		{
			get => _FromTime;
			set => Extensions.SetValue(ref _FromTime, value, OnFromTimeChanged);
		}

		void OnFromTimeChanged(DateTime obj)
		{
			_From = new DateTimeOffset(_FromTime).ToUnixTimeSeconds();
		}

		/// <summary>
		/// End of the time range, as <see cref="DateTime"/>.
		/// </summary>
		[JsonIgnore]
		public DateTime ToTime
		{
			get => _ToTime;
			set => Extensions.SetValue(ref _ToTime, value, OnToTimeChanged);
		}

		void OnToTimeChanged(DateTime obj)
		{
			_To = new DateTimeOffset(_ToTime).ToUnixTimeSeconds();
		}
	}
}
