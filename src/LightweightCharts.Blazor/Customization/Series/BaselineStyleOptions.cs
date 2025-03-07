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
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/BaselineStyleOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<BaselineStyleOptions>))]
	public class BaselineStyleOptions : SeriesOptionsCommon
	{
		/// <summary>
		/// Base value of the series.
		/// </summary>
		[JsonPropertyName("baseValue")]
		public BaseValuePrice BaseValue
		{
			get => GetValue<BaseValuePrice>();
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
		/// The first color of the top area.
		/// </summary>
		[JsonPropertyName("topFillColor1")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TopFillColor1
		{
			get => GetValue(() => Color.FromArgb(Convert.ToInt32(255 * 0.28), 38, 166, 154));
			set => SetValue(value);
		}

		/// <summary>
		/// The second color of the top area.
		/// </summary>
		[JsonPropertyName("topFillColor2")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TopFillColor2
		{
			get => GetValue(() => Color.FromArgb(Convert.ToInt32(255 * 0.05), 38, 166, 154));
			set => SetValue(value);
		}

		/// <summary>
		/// The line color of the top area.
		/// </summary>
		[JsonPropertyName("topLineColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color TopLineColor
		{
			get => GetValue(() => Color.FromArgb(Convert.ToInt32(255 * 1.00), 38, 166, 154));
			set => SetValue(value);
		}

		/// <summary>
		/// The first color of the bottom area.
		/// </summary>
		[JsonPropertyName("bottomFillColor1")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BottomFillColor1
		{
			get => GetValue(() => Color.FromArgb(Convert.ToInt32(255 * 0.05), 239, 83, 80));
			set => SetValue(value);
		}

		/// <summary>
		/// The second color of the bottom area.
		/// </summary>
		[JsonPropertyName("bottomFillColor2")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BottomFillColor2
		{
			get => GetValue(() => Color.FromArgb(Convert.ToInt32(255 * 0.28), 239, 83, 80));
			set => SetValue(value);
		}

		/// <summary>
		/// The line color of the bottom area.
		/// </summary>
		[JsonPropertyName("bottomLineColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BottomLineColor
		{
			get => GetValue(() => Color.FromArgb(Convert.ToInt32(255 * 1.00), 239, 83, 80));
			set => SetValue(value);
		}

		/// <summary>
		/// Line width.
		/// </summary>
		[JsonPropertyName("lineWidth")]
		public int LineWidth
		{
			get => GetValue(() => 3);
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
