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
	public class AreaStyleOptions : SeriesOptionsCommon
	{
		Color _TopColor = Color.FromArgb(Convert.ToInt32(255 * 0.4), 46, 220, 135);
		Color _BottomColor = Color.FromArgb(Convert.ToInt32(255 * 0), 40, 221, 100);
		bool _InvertFilledArea;
		Color _LineColor = Extensions.ParseHtmlCode("#33D778");
		LineStyle _LineStyle = LineStyle.Solid;
		int _LineWidth = 3;
		LineType _LineType = LineType.Simple;
		bool _LineVisible = true;
		bool _PointMarkersVisible;
		int? _PointMarkersRadius;
		bool _CrosshairMarkerVisible = true;
		int _CrosshairMarkerRadius = 4;
		Color? _CrosshairMarkerBorderColor;
		Color? _CrosshairMarkerBackgroundColor;
		int _CrosshairMarkerBorderWidth = 2;
		LastPriceAnimationMode _LastPriceAnimation = LastPriceAnimationMode.Disabled;

		/// <summary>
		/// Color of the top part of the area.
		/// </summary>
		[JsonPropertyName("topColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TopColor
		{
			get => _TopColor;
			set => SetValue(value, ref _TopColor);
		}

		/// <summary>
		/// Color of the bottom part of the area.
		/// </summary>
		[JsonPropertyName("bottomColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BottomColor
		{
			get => _BottomColor;
			set => SetValue(value, ref _BottomColor);
		}

		/// <summary>
		/// Invert the filled area. Fills the area above the line if set to true.
		/// </summary>
		[JsonPropertyName("invertFilledArea")]
		public bool InvertFilledArea
		{
			get => _InvertFilledArea;
			set => SetValue(value, ref _InvertFilledArea);
		}

		/// <summary>
		/// Line color.
		/// </summary>
		[JsonPropertyName("lineColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color LineColor
		{
			get => _LineColor;
			set => SetValue(value, ref _LineColor);
		}

		/// <summary>
		/// Line style.
		/// </summary>
		[JsonPropertyName("lineStyle")]
		public LineStyle LineStyle
		{
			get => _LineStyle;
			set => SetValue(value, ref _LineStyle);
		}

		/// <summary>
		/// Line width in pixels.
		/// </summary>
		[JsonPropertyName("lineWidth")]
		public int LineWidth
		{
			get => _LineWidth;
			set => SetValue(value, ref _LineWidth);
		}

		/// <summary>
		/// Line type.
		/// </summary>
		[JsonPropertyName("lineType")]
		public LineType LineType
		{
			get => _LineType;
			set => SetValue(value, ref _LineType);
		}

		/// <summary>
		/// Show series line.
		/// </summary>
		[JsonPropertyName("lineVisible")]
		public bool LineVisible
		{
			get => _LineVisible;
			set => SetValue(value, ref _LineVisible);
		}

		/// <summary>
		/// Show circle markers on each point.
		/// </summary>
		[JsonPropertyName("pointMarkersVisible")]
		public bool PointMarkersVisible
		{
			get => _PointMarkersVisible;
			set => SetValue(value, ref _PointMarkersVisible);
		}

		/// <summary>
		/// Circle markers radius in pixels.
		/// </summary>
		[JsonPropertyName("pointMarkersRadius")]
		public int? PointMarkersRadius
		{
			get => _PointMarkersRadius;
			set => SetValue(value, ref _PointMarkersRadius);
		}

		/// <summary>
		/// Show the crosshair marker.
		/// </summary>
		[JsonPropertyName("crosshairMarkerVisible")]
		public bool CrosshairMarkerVisible
		{
			get => _CrosshairMarkerVisible;
			set => SetValue(value, ref _CrosshairMarkerVisible);
		}

		/// <summary>
		/// Crosshair marker radius in pixels.
		/// </summary>
		[JsonPropertyName("crosshairMarkerRadius")]
		public int CrosshairMarkerRadius
		{
			get => _CrosshairMarkerRadius;
			set => SetValue(value, ref _CrosshairMarkerRadius);
		}

		/// <summary>
		/// Crosshair marker border color. An empty string falls back to the the color of the series under the crosshair.
		/// </summary>
		[JsonPropertyName("crosshairMarkerBorderColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? CrosshairMarkerBorderColor
		{
			get => _CrosshairMarkerBorderColor;
			set => SetValue(value, ref _CrosshairMarkerBorderColor);
		}

		/// <summary>
		/// The crosshair marker background color. An empty string falls back to the the color of the series under the crosshair.
		/// </summary>
		[JsonPropertyName("crosshairMarkerBackgroundColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? CrosshairMarkerBackgroundColor
		{
			get => _CrosshairMarkerBackgroundColor;
			set => SetValue(value, ref _CrosshairMarkerBackgroundColor);
		}

		/// <summary>
		/// Crosshair marker border width in pixels.
		/// </summary>
		[JsonPropertyName("crosshairMarkerBorderWidth")]
		public int CrosshairMarkerBorderWidth
		{
			get => _CrosshairMarkerBorderWidth;
			set => SetValue(value, ref _CrosshairMarkerBorderWidth);
		}

		/// <summary>
		/// Last price animation mode.
		/// </summary>
		[JsonPropertyName("lastPriceAnimation")]
		public LastPriceAnimationMode LastPriceAnimation
		{
			get => _LastPriceAnimation;
			set => SetValue(value, ref _LastPriceAnimation);
		}
	}
}
