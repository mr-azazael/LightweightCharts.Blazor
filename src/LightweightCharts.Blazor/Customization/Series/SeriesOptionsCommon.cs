using System.Drawing;
using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Converters;
using LightweightCharts.Blazor.Customization.Enums;
using LightweightCharts.Blazor.Utilities;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents options common for all types of series.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/SeriesOptionsCommon"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<SeriesOptionsCommon>))]
	public class SeriesOptionsCommon : JsonOptionsObject
	{
		/// <summary>
		/// Target price scale to bind new series to.
		/// </summary>
		[JsonPropertyName("priceScaleId")]
		public string PriceScaleId
		{
			get => GetValue(() => "right");
			set => SetValue(value);
		}

		/// <summary>
		/// You can name series when adding it to a chart. This name will be displayed on the label next to the last value label.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title
		{
			get => GetValue<string>();
			set => SetValue(value);
		}

		/// <summary>
		/// Visibility of the series. If the series is hidden, everything including price lines, baseline, price labels and markers, will also be hidden.<br/>
		/// Please note that hiding a series is not equivalent to deleting it, since hiding does not affect the timeline at all, unlike deleting where the timeline can be changed (some points can be deleted).
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Show the price line. Price line is a horizontal line indicating the last price of the series.
		/// </summary>
		[JsonPropertyName("priceLineVisible")]
		public bool PriceLineVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// The source to use for the value of the price line.
		/// </summary>
		[JsonPropertyName("priceLineSource")]
		public PriceLineSource PriceLineSource
		{
			get => GetValue(() => PriceLineSource.LastBar);
			set => SetValue(value);
		}

		/// <summary>
		/// Width of the price line.
		/// </summary>
		[JsonPropertyName("priceLineWidth")]
		public int PriceLineWidth
		{
			get => GetValue(() => 1);
			set => SetValue(value);
		}

		/// <summary>
		/// Color of the price line. By default, its color is set by the last bar color (or by line color on Line and Area charts).
		/// </summary>
		[JsonPropertyName("priceLineColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? PriceLineColor
		{
			get => GetValue<Color?>();
			set => SetValue(value);
		}

		/// <summary>
		/// Price line style.
		/// </summary>
		[JsonPropertyName("priceLineStyle")]
		public LineStyle PriceLineStyle
		{
			get => GetValue(() => LineStyle.Dashed);
			set => SetValue(value);
		}

		/// <summary>
		/// Visibility of the label with the latest visible price on the price scale.
		/// </summary>
		[JsonPropertyName("lastValueVisible")]
		public bool LastValueVisible
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Visibility of base line. Suitable for percentage and IndexedTo100 scales.
		/// </summary>
		[JsonPropertyName("baseLineVisible")]
		public bool BaseLineVisible
		{
			get => GetValue(() => true);
			set => SetValue(value);
		}

		/// <summary>
		/// Base line width. Suitable for percentage and IndexedTo10 scales.
		/// </summary>
		[JsonPropertyName("baseLineWidth")]
		public int BaseLineWidth
		{
			get => GetValue(() => 1);
			set => SetValue(value);
		}

		/// <summary>
		/// Color of the base line in IndexedTo100 mode.
		/// </summary>
		[JsonPropertyName("baseLineColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BaseLineColor
		{
			get => GetValue(() => Extensions.ParseHtmlCode("#B2B5BE"));
			set => SetValue(value);
		}

		/// <summary>
		/// Base line style. Suitable for percentage and indexedTo100 scales.
		/// </summary>
		[JsonPropertyName("baseLineStyle")]
		public LineStyle BaseLineStyle
		{
			get => GetValue(() => LineStyle.Solid);
			set => SetValue(value);
		}

		/// <summary>
		/// Price format.
		/// </summary>
		[JsonPropertyName("priceFormat")]
		public PriceFormatOptions PriceFormat
		{
			get => GetValue(() => new PriceFormatOptions());
			set => SetValue(value);
		}

#warning todo
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/SeriesOptionsCommon#autoscaleinfoprovider"/>
	}
}
