using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Models;
using System;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents style options for a baseline series.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/BaselineStyleOptions
	/// </summary>
	public class BaselineStyleOptions : SeriesOptionsCommon
	{
		BaseValuePrice _BaseValue = new();
		Color _TopFillColor1 = Color.FromArgb(Convert.ToInt32(255 * 0.28), 38, 166, 154);
		Color _TopFillColor2 = Color.FromArgb(Convert.ToInt32(255 * 0.05), 38, 166, 154);
		Color _TopLineColor = Color.FromArgb(Convert.ToInt32(255 * 1.00), 38, 166, 154);
		Color _BottomFillColor1 = Color.FromArgb(Convert.ToInt32(255 * 0.05), 239, 83, 80);
		Color _BottomFillColor2 = Color.FromArgb(Convert.ToInt32(255 * 0.28), 239, 83, 80);
		Color _BottomLineColor = Color.FromArgb(Convert.ToInt32(255 * 1.00), 239, 83, 80);
		int _LineWidth = 3;
		LineStyle _LineStyle = LineStyle.Solid;
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
		/// Base value of the series.
		/// </summary>
		[JsonPropertyName("baseValue")]
		public BaseValuePrice BaseValue
		{
			get => _BaseValue;
			set => SetValue(value, ref _BaseValue);
		}

		/// <summary>
		/// The first color of the top area.
		/// </summary>
		[JsonPropertyName("topFillColor1")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TopFillColor1
		{
			get => _TopFillColor1;
			set => SetValue(value, ref _TopFillColor1);
		}

		/// <summary>
		/// The second color of the top area.
		/// </summary>
		[JsonPropertyName("topFillColor2")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TopFillColor2
		{
			get => _TopFillColor2;
			set => SetValue(value, ref _TopFillColor2);
		}

		/// <summary>
		/// The line color of the top area.
		/// </summary>
		[JsonPropertyName("topLineColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TopLineColor
		{
			get => _TopLineColor;
			set => SetValue(value, ref _TopLineColor);
		}

		/// <summary>
		/// The first color of the bottom area.
		/// </summary>
		[JsonPropertyName("bottomFillColor1")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BottomFillColor1
		{
			get => _BottomFillColor1;
			set => SetValue(value, ref _BottomFillColor1);
		}

		/// <summary>
		/// The second color of the bottom area.
		/// </summary>
		[JsonPropertyName("bottomFillColor2")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BottomFillColor2
		{
			get => _BottomFillColor2;
			set => SetValue(value, ref _BottomFillColor2);
		}

		/// <summary>
		/// The line color of the bottom area.
		/// </summary>
		[JsonPropertyName("bottomLineColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BottomLineColor
		{
			get => _BottomLineColor;
			set => SetValue(value, ref _BottomLineColor);
		}

		/// <summary>
		/// Line width.
		/// </summary>
		[JsonPropertyName("lineWidth")]
		public int LineWidth
		{
			get => _LineWidth;
			set => SetValue(value, ref _LineWidth);
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
