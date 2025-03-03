using System;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	///	Represents a whitespace data item, which is a data point without a value.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/WhitespaceData"/>
	/// </summary>
	public class WhitespaceData : ISeriesData
	{
		long _UnixTime;
		DateTime _Time;

		/// <summary>
		/// The time of the data in unix format.
		/// </summary>
		[JsonPropertyName("time")]
		public long UnixTime
		{
			get => _UnixTime;
			set => Extensions.SetValue(ref _UnixTime, value, OnUnixTimeChanged);
		}

		void OnUnixTimeChanged(long obj)
		{
			_Time = DateTimeOffset.FromUnixTimeSeconds(_UnixTime).DateTime;
		}

		/// <summary>
		/// The time of the data as a <see cref="DateTime"/>.
		/// </summary>
		[JsonIgnore]
		public DateTime Time
		{
			get => _Time;
			set => Extensions.SetValue(ref _Time, value, OnTimeChanged);
		}

		void OnTimeChanged(DateTime obj)
		{
			_UnixTime = new DateTimeOffset(_Time).ToUnixTimeSeconds();
		}
	}
}
