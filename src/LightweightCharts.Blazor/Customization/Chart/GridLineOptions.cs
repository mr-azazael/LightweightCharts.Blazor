using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Utilities;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Grid line options.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/GridLineOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<GridLineOptions>))]
	public class GridLineOptions : JsonOptionsObject
	{
		/// <summary>
		/// Line color.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color
		{
			get => GetValue(() => Extensions.ParseColorCode("#D6DCDE"));
			set => SetValue(value);
		}

		/// <summary>
		/// Line style.
		/// </summary>
		[JsonPropertyName("style")]
		public LineStyle Style
		{
			get => GetValue(() => LineStyle.Solid);
			set => SetValue(value);
		}

		/// <summary>
		/// Display the lines.
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}
	}
}
