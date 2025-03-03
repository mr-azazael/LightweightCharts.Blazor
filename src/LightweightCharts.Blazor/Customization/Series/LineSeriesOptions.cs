using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents style options for a line series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/LineStyleOptions"/>
	/// </summary>
	public class LineSeriesOptions : SeriesOptionsCommon
	{
		Color _Color = Extensions.ParseHtmlCode("#2196f3");
		LineStyle _LineStyle = LineStyle.Solid;
		int _LineWidth = 3;
		LineType _LineType = LineType.Simple;
		bool _LineVisible = true;
		bool _PointMarkersVisible;
		int? _PointMarkersRadius;
		bool _CrosshairMarkerVisible = true;
		int _CrosshairMarkerRadius = 4;
		Color _CrosshairMarkerBorderColor = Color.Transparent;
		Color _CrosshairMarkerBackgroundColor = Color.Transparent;
		int _CrosshairMarkerBorderWidth = 2;
		LastPriceAnimationMode _LastPriceAnimation = LastPriceAnimationMode.Disabled;

		/// <summary>
		/// Line color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
		{
			get => _Color;
			set => SetValue(value, ref _Color);
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
		[JsonConverter(typeof(JsonColorConverter))]
		public Color CrosshairMarkerBorderColor
		{
			get => _CrosshairMarkerBorderColor;
			set => SetValue(value, ref _CrosshairMarkerBorderColor);
		}

		/// <summary>
		/// The crosshair marker background color. An empty string falls back to the the color of the series under the crosshair.
		/// </summary>
		[JsonPropertyName("crosshairMarkerBackgroundColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color CrosshairMarkerBackgroundColor
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
