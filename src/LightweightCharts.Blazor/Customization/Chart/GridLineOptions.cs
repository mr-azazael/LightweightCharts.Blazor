using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Grid line options.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/GridLineOptions"/>
	/// </summary>
	public class GridLineOptions : BaseModel
	{
		Color _Color = Extensions.ParseHtmlCode("#D6DCDE");
		LineStyle _Style = LineStyle.Solid;
		bool _Visible = true;

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
		[JsonPropertyName("style")]
		public LineStyle Style
		{
			get => _Style;
			set => SetValue(value, ref _Style);
		}

		/// <summary>
		/// Display the lines.
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => _Visible;
			set => SetValue(value, ref _Visible);
		}
	}
}
