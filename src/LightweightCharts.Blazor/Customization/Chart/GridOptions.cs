using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure describing grid options.
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/GridOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<GridOptions>))]
	public class GridOptions : JsonOptionsObject
	{
		/// <summary>
		/// Vertical grid line options.
		/// </summary>
		[JsonPropertyName("vertLines")]
		public GridLineOptions VerticalLines
		{
			get => GetValue(() => new GridLineOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Horizontal grid line options.
		/// </summary>
		[JsonPropertyName("horzLines")]
		public GridLineOptions HorizontalLines
		{
			get => GetValue(() => new GridLineOptions());
			set => SetValue(value);
		}
	}
}
