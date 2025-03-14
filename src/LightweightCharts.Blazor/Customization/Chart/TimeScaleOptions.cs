﻿using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Extended time scale options for time-based horizontal scale.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/TimeScaleOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<TimeScaleOptions>))]
	public class TimeScaleOptions : HorzScaleOptions
	{
		/// <summary>
		/// Tick marks formatter can be used to customize tick marks labels on the time axis.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/type-aliases/TickMarkFormatter"/>
		/// </summary>
		[JsonPropertyName("tickMarkFormatter")]
		public JsDelegate TickMarkFormatter
		{
			get => GetValue<JsDelegate>();
			set => SetValue(value);
		}
	}
}
