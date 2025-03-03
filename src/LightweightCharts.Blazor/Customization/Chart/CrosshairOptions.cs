using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure describing crosshair options.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/CrosshairOptions"/>
	/// </summary>
	public class CrosshairOptions : BaseModel
	{
		CrosshairMode _Mode = CrosshairMode.Magnet;
		CrosshairLineOption _VerticalLine = new();
		CrosshairLineOption _HorizontalLine = new();

		/// <summary>
		/// Vertical line options.
		/// </summary>
		[JsonPropertyName("vertLine")]
		public CrosshairLineOption VerticalLine
		{
			get => _VerticalLine;
			set => SetValue(value, ref _VerticalLine);
		}

		/// <summary>
		/// Horizontal line options.
		/// </summary>
		[JsonPropertyName("horzLine")]
		public CrosshairLineOption HorizontalLine
		{
			get => _HorizontalLine;
			set => SetValue(value, ref _HorizontalLine);
		}

		/// <summary>
		/// Crosshair mode.
		/// </summary>
		[JsonPropertyName("mode")]
		public CrosshairMode Mode
		{
			get => _Mode;
			set => SetValue(value, ref _Mode);
		}
	}
}
