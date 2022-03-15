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
		bool _Visible = true;

		/// <summary>
		/// Autoscaling is a feature that automatically adjusts a price scale to fit the visible range of data.
		/// </summary>
		[JsonPropertyName("autoScale")]
		public bool AutoScale
		{
			get => _AutoScale;
			set => SetValue(value, ref _AutoScale);
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
	}
}
