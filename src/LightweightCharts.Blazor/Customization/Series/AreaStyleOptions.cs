using System;
using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents style options for an area series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/AreaStyleOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<AreaStyleOptions>))]
	public class AreaStyleOptions : SeriesOptionsCommon
	{
		/// <summary>
		/// Color of the top part of the area.
		/// </summary>
		[JsonPropertyName("topColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TopColor
		{
			get => GetValue(() => Color.FromArgb(Convert.ToInt32(255 * 0.4), 46, 220, 135));
			set => SetValue(value);
		}

		/// <summary>
		/// Color of the bottom part of the area.
		/// </summary>
		[JsonPropertyName("bottomColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BottomColor
		{
			get => GetValue(() => Color.FromArgb(Convert.ToInt32(255 * 0), 40, 221, 100));
			set => SetValue(value);
		}

		/// <summary>
		/// Gradient is relative to the base value and the currently visible range.<br/>
		/// If it is false, the gradient is relative to the top and bottom of the chart.
		/// </summary>
		[JsonPropertyName("relativeGradient")]
		public bool RelativeGradient
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Invert the filled area. Fills the area above the line if set to true.
		/// </summary>
		[JsonPropertyName("invertFilledArea")]
		public bool InvertFilledArea
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Line color.
		/// </summary>
		[JsonPropertyName("lineColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color LineColor
		{
			get => GetValue(() => Extensions.ParseColorCode("#33D778"));
			set => SetValue(value);
		}

		/// <summary>
		/// Line style.
		/// </summary>
		[JsonPropertyName("lineStyle")]
		public LineStyle LineStyle
		{
			get => GetValue(() => LineStyle.Solid);
			set => SetValue(value);
		}

		/// <summary>
		/// Line width in pixels.
		/// </summary>
		[JsonPropertyName("lineWidth")]
		public int LineWidth
		{
			get => GetValue(() => 3);
			set => SetValue(value);
		}

		/// <summary>
		/// Line type.
		/// </summary>
		[JsonPropertyName("lineType")]
		public LineType LineType
		{
			get => GetValue(() => LineType.Simple);
			set => SetValue(value);
		}

		/// <summary>
		/// Show series line.
		/// </summary>
		[JsonPropertyName("lineVisible")]
		public bool LineVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Show circle markers on each point.
		/// </summary>
		[JsonPropertyName("pointMarkersVisible")]
		public bool PointMarkersVisible
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Circle markers radius in pixels.
		/// </summary>
		[JsonPropertyName("pointMarkersRadius")]
		public int? PointMarkersRadius
		{
			get => GetValue<int?>();
			set => SetValue(value);
		}

		/// <summary>
		/// Show the crosshair marker.
		/// </summary>
		[JsonPropertyName("crosshairMarkerVisible")]
		public bool CrosshairMarkerVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Crosshair marker radius in pixels.
		/// </summary>
		[JsonPropertyName("crosshairMarkerRadius")]
		public int CrosshairMarkerRadius
		{
			get => GetValue(() => 4);
			set => SetValue(value);
		}

		/// <summary>
		/// Crosshair marker border color. An empty string falls back to the the color of the series under the crosshair.
		/// </summary>
		[JsonPropertyName("crosshairMarkerBorderColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? CrosshairMarkerBorderColor
		{
			get => GetValue<Color?>();
			set => SetValue(value);
		}

		/// <summary>
		/// The crosshair marker background color. An empty string falls back to the the color of the series under the crosshair.
		/// </summary>
		[JsonPropertyName("crosshairMarkerBackgroundColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? CrosshairMarkerBackgroundColor
		{
			get => GetValue<Color?>();
			set => SetValue(value);
		}

		/// <summary>
		/// Crosshair marker border width in pixels.
		/// </summary>
		[JsonPropertyName("crosshairMarkerBorderWidth")]
		public int CrosshairMarkerBorderWidth
		{
			get => GetValue(() => 2);
			set => SetValue(value);
		}

		/// <summary>
		/// Last price animation mode.
		/// </summary>
		[JsonPropertyName("lastPriceAnimation")]
		public LastPriceAnimationMode LastPriceAnimation
		{
			get => GetValue(() => LastPriceAnimationMode.Disabled);
			set => SetValue(value);
		}
	}
}
