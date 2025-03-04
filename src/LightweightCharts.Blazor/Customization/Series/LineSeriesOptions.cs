using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Utilities;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents style options for a line series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/LineStyleOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<LineSeriesOptions>))]
	public class LineSeriesOptions : SeriesOptionsCommon
	{
		/// <summary>
		/// Line color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#2196f3"));
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
		[JsonConverter(typeof(JsonColorConverter))]
		public Color CrosshairMarkerBorderColor
		{
			get => GetValue(() => Color.Transparent);
			set => SetValue(value);
		}

		/// <summary>
		/// The crosshair marker background color. An empty string falls back to the the color of the series under the crosshair.
		/// </summary>
		[JsonPropertyName("crosshairMarkerBackgroundColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color CrosshairMarkerBackgroundColor
		{
			get => GetValue(() => Color.Transparent);
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
