using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;

namespace LightweightCharts.Blazor.DataItems
{
	/// <summary>
	/// Represents a series marker.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/SeriesMarkerBase"/>
	/// </summary>
	public class SeriesMarkerBase<H> : WhitespaceData<H>
		where H : struct
	{
		/// <summary>
		/// The shape of the marker.
		/// </summary>
		[JsonPropertyName("shape")]
		public SeriesMarkerShape Shape { get; set; }

		/// <summary>
		/// The optional size of the marker.
		/// </summary>
		[JsonPropertyName("size")]
		public double Size { get; set; } = 1;

		/// <summary>
		/// The color of the marker.
		/// </summary>
		[JsonPropertyName("color")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color Color { get; set; }

		/// <summary>
		/// The ID of the marker.
		/// </summary>
		[JsonPropertyName("id")]
		public string Id { get; set; }

		/// <summary>
		/// The optional text of the marker.
		/// </summary>
		[JsonPropertyName("text")]
		public string Text { get; set; }

		/// <summary>
		/// The price value for exact Y-axis positioning.<br/>
		/// Required when using <see cref="SeriesMarkerPricePosition"/> position type.<br/>
		/// </summary>
		[JsonPropertyName("price")]
		public double? Price { get; set; }
	}

	/// <summary>
	/// Represents a series marker bar.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/SeriesMarkerBar"/>
	/// </summary>
	public class SeriesMarkerBar<H> : SeriesMarkerBase<H>
		where H : struct
	{
		/// <summary>
		/// The position of the marker.
		/// </summary>
		[JsonPropertyName("position")]
		public SeriesMarkerBarPosition Position { get; set; }
	}

	/// <summary>
	/// Represents a series marker price.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/SeriesMarkerPrice"/>
	/// </summary>
	public class SeriesMarkerPrice<H> : SeriesMarkerBase<H>
		where H : struct
	{
		/// <summary>
		/// The position of the marker.
		/// </summary>
		[JsonPropertyName("position")]
		public SeriesMarkerPricePosition Position { get; set; }
	}
}
