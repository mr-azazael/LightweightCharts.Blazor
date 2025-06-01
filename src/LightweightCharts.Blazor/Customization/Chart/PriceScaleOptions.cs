using LightweightCharts.Blazor.Converters;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure that describes price scale options.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceScaleOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<PriceScaleOptions>))]
	public class PriceScaleOptions : BasePriceScaleOptions
	{
		/// <summary>
		/// Autoscaling is a feature that automatically adjusts a price scale to fit the visible range of data.<br/>
		/// Note that overlay price scales are always auto-scaled.
		/// </summary>
		[JsonPropertyName("autoScale")]
		public bool AutoScale
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Price scale text color. If not provided textColor is used.
		/// </summary>
		[JsonPropertyName("textColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? TextColor
		{
			get => GetValue<Color?>();
			set => SetValue(value);
		}

		/// <summary>
		/// Indicates if this price scale visible.
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Define a minimum width for the price scale. Note: This value will be exceeded if the price scale needs more space to display it's contents.<br/>
		/// Setting a minimum width could be useful for ensuring that multiple charts positioned in a vertical stack each have an identical price scale width,<br/>
		/// or for plugins which require a bit more space within the price scale pane.
		/// </summary>
		[JsonPropertyName("minimumWidth")]
		public int MinimumWidth
		{
			get => GetValue<int>();
			set => SetValue(value);
		}

		/// <summary>
		/// Ensures that tick marks are always visible at the very top and bottom of the price scale, regardless of the data range.<br/>
		/// When enabled, a tick mark will be drawn at both edges of the scale, providing clear boundary indicators.
		/// </summary>
		[JsonPropertyName("ensureEdgeTickMarksVisible")]
		public bool EnsureEdgeTickMarksVisible
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}
	}
}
