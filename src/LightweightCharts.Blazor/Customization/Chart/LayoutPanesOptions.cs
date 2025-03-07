using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Utilities;
using System.Drawing;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents panes customizations.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/LayoutPanesOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<LayoutPanesOptions>))]
	public class LayoutPanesOptions : JsonOptionsObject
	{
		/// <summary>
		/// Enable panes resizing
		/// </summary>
		[JsonPropertyName("enableResize")]
		public bool EnableResize
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Color of pane separator
		/// </summary>
		[JsonPropertyName("separatorColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color SeparatorColor
		{
			get => GetValue(() => Extensions.ParseColorCode("#2B2B43"));
			set => SetValue(value);
		}

		/// <summary>
		/// Color of pane separator background applied on hover
		/// </summary>
		[JsonPropertyName("separatorHoverColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color SeparatorHoverColor
		{
			get => GetValue(() => Extensions.ParseColorCode("rgba(178, 181, 189, 0.2)"));
			set => SetValue(value);
		}
	}
}
