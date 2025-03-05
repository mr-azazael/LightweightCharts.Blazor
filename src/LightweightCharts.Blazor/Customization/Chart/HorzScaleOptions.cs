using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Options for the time scale; the horizontal scale at the bottom of the chart that displays the time of data.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/HorzScaleOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<HorzScaleOptions>))]
	public class HorzScaleOptions : JsonOptionsObject
	{
		/// <summary>
		/// The margin space in bars from the right side of the chart.
		/// </summary>
		[JsonPropertyName("rightOffset")]
		public int RightOffset
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		/// <summary>
		/// The space between bars in pixels.
		/// </summary>
		[JsonPropertyName("barSpacing")]
		public int BarSpacing
		{
			get => GetValue(() => 6);
			set => SetValue(value);
		}

		/// <summary>
		/// The minimum space between bars in pixels.
		/// </summary>
		[JsonPropertyName("minBarSpacing")]
		public double MinBarSpacing
		{
			get => GetValue(() => 0.5);
			set => SetValue(value);
		}

		/// <summary>
		/// Prevent scrolling to the left of the first bar.
		/// </summary>
		[JsonPropertyName("fixLeftEdge")]
		public bool FixLeftEdge
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Prevent scrolling to the right of the most recent bar.
		/// </summary>
		[JsonPropertyName("fixRightEdge")]
		public bool FixRightEdge
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Prevent changing the visible time range during chart resizing.
		/// </summary>
		[JsonPropertyName("lockVisibleTimeRangeOnResize")]
		public bool LockVisibleTimeRangeOnResize
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Prevent the hovered bar from moving when scrolling.
		/// </summary>
		[JsonPropertyName("rightBarStaysOnScroll")]
		public bool RightBarStaysOnScroll
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Show the time scale border.
		/// </summary>
		[JsonPropertyName("borderVisible")]
		public bool BorderVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// The time scale border color.
		/// </summary>
		[JsonPropertyName("borderColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BorderColor
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#2B2B43"));
			set => SetValue(value);
		}

		/// <summary>
		/// Show the time scale.
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Show the time, not just the date, in the time scale and vertical crosshair label.
		/// </summary>
		[JsonPropertyName("timeVisible")]
		public bool TimeVisible
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Show seconds in the time scale and vertical crosshair label in hh:mm:ss format for intraday data.
		/// </summary>
		[JsonPropertyName("secondsVisible")]
		public bool SecondsVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Shift the visible range to the right (into the future) by the number of new bars when new data is added.<br/>
		/// Note that this only applies when the last bar is visible.
		/// </summary>
		[JsonPropertyName("shiftVisibleRangeOnNewBar")]
		public bool ShiftVisibleRangeOnNewBar
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Allow the visible range to be shifted to the right when a new bar is added which is replacing an existing whitespace time point on the chart.<br/>
		/// Note that this only applies when the last bar is visible and shiftVisibleRangeOnNewBar is enabled.
		/// </summary>
		[JsonPropertyName("allowShiftVisibleRangeOnWhitespaceReplacement")]
		public bool AllowShiftVisibleRangeOnWhitespaceReplacement
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Draw small vertical line on time axis labels.
		/// </summary>
		[JsonPropertyName("ticksVisible")]
		public bool TicksVisible
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Maximum tick mark label length. Used to override the default 8 character maximum length.
		/// </summary>
		[JsonPropertyName("tickMarkMaxCharacterLength")]
		public int? TickMarkMaxCharacterLength
		{
			get => GetValue<int?>();
			set => SetValue(value);
		}

		/// <summary>
		/// Changes horizontal scale marks generation. With this flag equal to true, marks of the same weight are either all drawn or none are drawn at all.
		/// </summary>
		[JsonPropertyName("uniformDistribution")]
		public bool UniformDistribution
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Define a minimum height for the time scale. Note: This value will be exceeded if the time scale needs more space to display it's contents.<br/>
		/// Setting a minimum height could be useful for ensuring that multiple charts positioned in a horizontal stack each have an identical<br/>
		/// time scale height, or for plugins which require a bit more space within the time scale pane.
		/// </summary>
		[JsonPropertyName("minimumHeight")]
		public int MinimumHeight
		{
			get => GetValue<int>();
			set => SetValue(value);
		}
	}
}
