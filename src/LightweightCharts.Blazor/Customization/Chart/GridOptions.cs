using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure describing grid options.
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/GridOptions"/>
	/// </summary>
	public class GridOptions : BaseModel
	{
		GridLineOptions _VerticalLines = new();
		GridLineOptions _HorizontalLines = new();

		/// <summary>
		/// Vertical grid line options.
		/// </summary>
		[JsonPropertyName("vertLines")]
		public GridLineOptions VerticalLines
		{
			get => _VerticalLines;
			set => SetValue(value, ref _VerticalLines);
		}

		/// <summary>
		/// Horizontal grid line options.
		/// </summary>
		[JsonPropertyName("horzLines")]
		public GridLineOptions HorizontalLines
		{
			get => _HorizontalLines;
			set => SetValue(value, ref _HorizontalLines);
		}
	}
}
