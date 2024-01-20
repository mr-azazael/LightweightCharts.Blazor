using LightweightCharts.Blazor.Converters;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure that describes price scale options.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceScaleOptions
	/// </summary>
	public class PriceScaleOptions : BasePriceScaleOptions
	{
		bool _AutoScale = true;
		Color? _TextColor;
		bool _Visible = true;
		int _MinimumWidth;

		/// <summary>
		/// Autoscaling is a feature that automatically adjusts a price scale to fit the visible range of data.<br/>
		/// Note that overlay price scales are always auto-scaled.
		/// </summary>
		[JsonPropertyName("autoScale")]
		public bool AutoScale
		{
			get => _AutoScale;
			set => SetValue(value, ref _AutoScale);
		}

		/// <summary>
		/// Price scale text color. If not provided textColor is used.
		/// </summary>
		[JsonPropertyName("textColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? TextColor
		{
			get => _TextColor;
			set => SetValue(value, ref _TextColor);
		}

		/// <summary>
		/// Indicates if this price scale visible.
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => _Visible;
			set => SetValue(value, ref _Visible);
		}

		/// <summary>
		/// Define a minimum width for the price scale. Note: This value will be exceeded if the price scale needs more space to display it's contents.<br/>
		/// Setting a minimum width could be useful for ensuring that multiple charts positioned in a vertical stack each have an identical price scale width,<br/>
		/// or for plugins which require a bit more space within the price scale pane.
		/// </summary>
		[JsonPropertyName("minimumWidth")]
		public int MinimumWidth
		{
			get => _MinimumWidth;
			set => SetValue(value, ref _MinimumWidth);
		}
	}
}
