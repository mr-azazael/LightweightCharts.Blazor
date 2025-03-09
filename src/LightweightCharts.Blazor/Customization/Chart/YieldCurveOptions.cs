using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Options specific to yield curve charts.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/YieldCurveOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<YieldCurveOptions>))]
	public class YieldCurveOptions : JsonOptionsObject
	{
		/// <summary>
		/// The smallest time unit for the yield curve, typically representing one month. This value determines the granularity of the time scale.
		/// </summary>
		[JsonPropertyName("baseResolution")]
		public double BaseResolution
		{
			get => GetValue(() => 1);
			set => SetValue(value);
		}

		/// <summary>
		/// The minimum time range to be displayed on the chart, in units of baseResolution.<br/>
		/// This ensures that the chart always shows at least this much time range, even if there's less data.
		/// </summary>
		[JsonPropertyName("minimumTimeRange")]
		public int MinimumTimeRange
		{
			get => GetValue(() => 120);
			set => SetValue(value);
		}

		/// <summary>
		/// The starting time value for the chart, in units of baseResolution. This determines where the time scale begins.
		/// </summary>
		[JsonPropertyName("startTimeRange")]
		public int StartTimeRange
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		/// <summary>
		/// Optional custom formatter for time values on the horizontal axis. If not provided, a default formatter will be used.<br/>
		/// formatTime: (months) => string
		/// </summary>
		[JsonPropertyName("formatTime")]
		public JsDelegate FormatTime
		{
			get => GetValue<JsDelegate>();
			set => SetValue(value);
		}
	}
}
