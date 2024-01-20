using System;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Models
{
	/// <summary>
	/// Represents a range of bars and the number of bars outside the range.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/BarsInfo
	/// </summary>
	public class BarsInfo
	{
		long? _UnixFrom;
		long? _UnixTo;

		DateTime? _FromDate;
		DateTime? _ToDate;

		/// <summary>
		/// The from value. The start of the range.
		/// </summary>
		[JsonPropertyName("from")]
		public long? UnixFrom
		{
			get => _UnixFrom;
			set => Extensions.SetValue(ref _UnixFrom, value, OnUnixFromChanged);
		}

		void OnUnixFromChanged(long? obj)
		{
			if (_UnixFrom != null)
				_FromDate = DateTimeOffset.FromUnixTimeSeconds(_UnixFrom.Value).DateTime;
			else
				_FromDate = null;
		}

		/// <summary>
		/// The to value. The end of the range.
		/// </summary>
		[JsonPropertyName("to")]
		public long? UnixTo
		{
			get => _UnixTo;
			set => Extensions.SetValue(ref _UnixTo, value, OnUnixToChanged);
		}

		void OnUnixToChanged(long? obj)
		{
			if (_UnixTo != null)
				_ToDate = DateTimeOffset.FromUnixTimeSeconds(_UnixTo.Value).DateTime;
			else
				_ToDate = null;
		}

		/// <summary>
		/// Same as <see cref="UnixFrom"/> but as a <see cref="DateTime"/>
		/// </summary>
		[JsonIgnore]
		public DateTime? FromDate
		{
			get => _FromDate;
			set => Extensions.SetValue(ref _FromDate, value, OnFromDateChanged);
		}

		void OnFromDateChanged(DateTime? obj)
		{
			if (_FromDate != null)
				_UnixFrom = new DateTimeOffset(_FromDate.Value).ToUnixTimeSeconds();
			else
				_UnixFrom = null;
		}

		/// <summary>
		/// Same as <see cref="UnixTo"/> but as a <see cref="DateTime"/>
		/// </summary>
		[JsonIgnore]
		public DateTime? ToDate
		{
			get => _ToDate;
			set => Extensions.SetValue(ref _ToDate, value, OnToDateChanged);
		}

		void OnToDateChanged(DateTime? obj)
		{
			if (_ToDate != null)
				_UnixTo = new DateTimeOffset(_ToDate.Value).ToUnixTimeSeconds();
			else
				_UnixTo = null;
		}

		/// <summary>
		/// The number of bars before the start of the range. Positive value means that there are some bars before (out of logical range from the left) the Range.from logical index in the series. <br/>
		/// Negative value means that the first series' bar is inside the passed logical range, and between the first series' bar and the Range.from logical index are some bars.
		/// </summary>
		[JsonPropertyName("barsBefore")]
		public int BarsBefore { get; set; }

		/// <summary>
		/// The number of bars after the end of the range. Positive value in the barsAfter field means that there are some bars after (out of logical range from the right) the Range.to logical index in the series. <br/>
		/// Negative value means that the last series' bar is inside the passed logical range, and between the last series' bar and the Range.to logical index are some bars.
		/// </summary>
		[JsonPropertyName("barsAfter")]
		public int BarsAfter { get; set; }
	}
}
