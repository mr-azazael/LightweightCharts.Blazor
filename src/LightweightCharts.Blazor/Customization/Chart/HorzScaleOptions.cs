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
	public class HorzScaleOptions : BaseModel
	{
		int _RightOffset;
		int _BarSpacing = 6;
		double _MinBarSpacing = 0.5;
		bool _FixLeftEdge;
		bool _FixRightEdge;
		bool _LockVisibleTimeRangeOnResize;
		bool _RightBarStaysOnScroll;
		bool _BorderVisible = true;
		Color _BorderColor = Extensions.ParseHtmlCode("#2B2B43");
		bool _Visible = true;
		bool _TimeVisible = false;
		bool _SecondsVisible = true;
		bool _ShiftVisibleRangeOnNewBar = true;
		bool _AllowShiftVisibleRangeOnWhitespaceReplacement;
		bool _TicksVisible;
		int? _TickMarkMaxCharacterLength;
		bool _UniformDistribution;
		int _MinimumHeight;

		/// <summary>
		/// The margin space in bars from the right side of the chart.
		/// </summary>
		[JsonPropertyName("rightOffset")]
		public int RightOffset
		{
			get => _RightOffset;
			set => SetValue(value, ref _RightOffset);
		}

		/// <summary>
		/// The space between bars in pixels.
		/// </summary>
		[JsonPropertyName("barSpacing")]
		public int BarSpacing
		{
			get => _BarSpacing;
			set => SetValue(value, ref _BarSpacing);
		}

		/// <summary>
		/// The minimum space between bars in pixels.
		/// </summary>
		[JsonPropertyName("minBarSpacing")]
		public double MinBarSpacing
		{
			get => _MinBarSpacing;
			set => SetValue(value, ref _MinBarSpacing);
		}

		/// <summary>
		/// Prevent scrolling to the left of the first bar.
		/// </summary>
		[JsonPropertyName("fixLeftEdge")]
		public bool FixLeftEdge
		{
			get => _FixLeftEdge;
			set => SetValue(value, ref _FixLeftEdge);
		}

		/// <summary>
		/// Prevent scrolling to the right of the most recent bar.
		/// </summary>
		[JsonPropertyName("fixRightEdge")]
		public bool FixRightEdge
		{
			get => _FixRightEdge;
			set => SetValue(value, ref _FixRightEdge);
		}

		/// <summary>
		/// Prevent changing the visible time range during chart resizing.
		/// </summary>
		[JsonPropertyName("lockVisibleTimeRangeOnResize")]
		public bool LockVisibleTimeRangeOnResize
		{
			get => _LockVisibleTimeRangeOnResize;
			set => SetValue(value, ref _LockVisibleTimeRangeOnResize);
		}

		/// <summary>
		/// Prevent the hovered bar from moving when scrolling.
		/// </summary>
		[JsonPropertyName("rightBarStaysOnScroll")]
		public bool RightBarStaysOnScroll
		{
			get => _RightBarStaysOnScroll;
			set => SetValue(value, ref _RightBarStaysOnScroll);
		}

		/// <summary>
		/// Show the time scale border.
		/// </summary>
		[JsonPropertyName("borderVisible")]
		public bool BorderVisible
		{
			get => _BorderVisible;
			set => SetValue(value, ref _BorderVisible);
		}

		/// <summary>
		/// The time scale border color.
		/// </summary>
		[JsonPropertyName("borderColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BorderColor
		{
			get => _BorderColor;
			set => SetValue(value, ref _BorderColor);
		}

		/// <summary>
		/// Show the time scale.
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => _Visible;
			set => SetValue(value, ref _Visible);
		}

		/// <summary>
		/// Show the time, not just the date, in the time scale and vertical crosshair label.
		/// </summary>
		[JsonPropertyName("timeVisible")]
		public bool TimeVisible
		{
			get => _TimeVisible;
			set => SetValue(value, ref _TimeVisible);
		}

		/// <summary>
		/// Show seconds in the time scale and vertical crosshair label in hh:mm:ss format for intraday data.
		/// </summary>
		[JsonPropertyName("secondsVisible")]
		public bool SecondsVisible
		{
			get => _SecondsVisible;
			set => SetValue(value, ref _SecondsVisible);
		}

		/// <summary>
		/// Shift the visible range to the right (into the future) by the number of new bars when new data is added.<br/>
		/// Note that this only applies when the last bar is visible.
		/// </summary>
		[JsonPropertyName("shiftVisibleRangeOnNewBar")]
		public bool ShiftVisibleRangeOnNewBar
		{
			get => _ShiftVisibleRangeOnNewBar;
			set => SetValue(value, ref _ShiftVisibleRangeOnNewBar);
		}

		/// <summary>
		/// Allow the visible range to be shifted to the right when a new bar is added which is replacing an existing whitespace time point on the chart.<br/>
		/// Note that this only applies when the last bar is visible & shiftVisibleRangeOnNewBar is enabled.
		/// </summary>
		[JsonPropertyName("allowShiftVisibleRangeOnWhitespaceReplacement")]
		public bool AllowShiftVisibleRangeOnWhitespaceReplacement
		{
			get => _AllowShiftVisibleRangeOnWhitespaceReplacement;
			set => SetValue(value, ref _AllowShiftVisibleRangeOnWhitespaceReplacement);
		}

		/// <summary>
		/// Draw small vertical line on time axis labels.
		/// </summary>
		[JsonPropertyName("ticksVisible")]
		public bool TicksVisible
		{
			get => _TicksVisible;
			set => SetValue(value, ref _TicksVisible);
		}

		/// <summary>
		/// Maximum tick mark label length. Used to override the default 8 character maximum length.
		/// </summary>
		[JsonPropertyName("tickMarkMaxCharacterLength")]
		public int? TickMarkMaxCharacterLength
		{
			get => _TickMarkMaxCharacterLength;
			set => SetValue(value, ref _TickMarkMaxCharacterLength);
		}

		/// <summary>
		/// Changes horizontal scale marks generation. With this flag equal to true, marks of the same weight are either all drawn or none are drawn at all.
		/// </summary>
		[JsonPropertyName("uniformDistribution")]
		public bool UniformDistribution
		{
			get => _UniformDistribution;
			set => SetValue(value, ref _UniformDistribution);
		}

		/// <summary>
		/// Define a minimum height for the time scale. Note: This value will be exceeded if the time scale needs more space to display it's contents.<br/>
		/// Setting a minimum height could be useful for ensuring that multiple charts positioned in a horizontal stack each have an identical<br/>
		/// time scale height, or for plugins which require a bit more space within the time scale pane.
		/// </summary>
		[JsonPropertyName("minimumHeight")]
		public int MinimumHeight
		{
			get => _MinimumHeight;
			set => SetValue(value, ref _MinimumHeight);
		}
	}
}
