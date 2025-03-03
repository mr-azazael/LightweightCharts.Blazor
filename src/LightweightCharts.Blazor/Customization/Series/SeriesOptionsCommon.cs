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
	public class SeriesOptionsCommon : BaseModel
	{
		bool _LastValueVisible = true;
		string _Title;
		string _PriceScaleId = "right";
		bool _Visible = true;
		bool _PriceLineVisible = true;
		PriceLineSource _PriceLineSource = PriceLineSource.LastBar;
		int _PriceLineWidth = 1;
		Color? _PriceLineColor;
		LineStyle _PriceLineStyle = LineStyle.Dashed;
		PriceFormatOptions _PriceFormat = new();
		bool _BaseLineVisible = true;
		Color _BaseLineColor = Extensions.ParseHtmlCode("#B2B5BE");
		int _BaseLineWidth = 1;
		LineStyle _BaseLineStyle = LineStyle.Solid;

		/// <summary>
		/// Target price scale to bind new series to.
		/// </summary>
		[JsonPropertyName("priceScaleId")]
		public string PriceScaleId
		{
			get => _PriceScaleId;
			set => SetValue(value, ref _PriceScaleId);
		}

		/// <summary>
		/// You can name series when adding it to a chart. This name will be displayed on the label next to the last value label.
		/// </summary>
		[JsonPropertyName("title")]
		public string Title
		{
			get => _Title;
			set => SetValue(value, ref _Title);
		}

		/// <summary>
		/// Visibility of the series. If the series is hidden, everything including price lines, baseline, price labels and markers, will also be hidden.<br/>
		/// Please note that hiding a series is not equivalent to deleting it, since hiding does not affect the timeline at all, unlike deleting where the timeline can be changed (some points can be deleted).
		/// </summary>
		[JsonPropertyName("visible")]
		public bool Visible
		{
			get => _Visible;
			set => SetValue(value, ref _Visible);
		}

		/// <summary>
		/// Show the price line. Price line is a horizontal line indicating the last price of the series.
		/// </summary>
		[JsonPropertyName("priceLineVisible")]
		public bool PriceLineVisible
		{
			get => _PriceLineVisible;
			set => SetValue(value, ref _PriceLineVisible);
		}

		/// <summary>
		/// The source to use for the value of the price line.
		/// </summary>
		[JsonPropertyName("priceLineSource")]
		public PriceLineSource PriceLineSource
		{
			get => _PriceLineSource;
			set => SetValue(value, ref _PriceLineSource);
		}

		/// <summary>
		/// Width of the price line.
		/// </summary>
		[JsonPropertyName("priceLineWidth")]
		public int PriceLineWidth
		{
			get => _PriceLineWidth;
			set => SetValue(value, ref _PriceLineWidth);
		}

		/// <summary>
		/// Color of the price line. By default, its color is set by the last bar color (or by line color on Line and Area charts).
		/// </summary>
		[JsonPropertyName("priceLineColor")]
		[JsonConverter(typeof(JsonOptionalColorConverter))]
		public Color? PriceLineColor
		{
			get => _PriceLineColor;
			set => SetValue(value, ref _PriceLineColor);
		}

		/// <summary>
		/// Price line style.
		/// </summary>
		[JsonPropertyName("priceLineStyle")]
		public LineStyle PriceLineStyle
		{
			get => _PriceLineStyle;
			set => SetValue(value, ref _PriceLineStyle);
		}

		/// <summary>
		/// Visibility of the label with the latest visible price on the price scale.
		/// </summary>
		[JsonPropertyName("lastValueVisible")]
		public bool LastValueVisible
		{
			get => _LastValueVisible;
			set => SetValue(value, ref _LastValueVisible);
		}

		/// <summary>
		/// Visibility of base line. Suitable for percentage and IndexedTo100 scales.
		/// </summary>
		[JsonPropertyName("baseLineVisible")]
		public bool BaseLineVisible
		{
			get => _BaseLineVisible;
			set => SetValue(value, ref _BaseLineVisible);
		}

		/// <summary>
		/// Base line width. Suitable for percentage and IndexedTo10 scales.
		/// </summary>
		[JsonPropertyName("baseLineWidth")]
		public int BaseLineWidth
		{
			get => _BaseLineWidth;
			set => SetValue(value, ref _BaseLineWidth);
		}

		/// <summary>
		/// Color of the base line in IndexedTo100 mode.
		/// </summary>
		[JsonPropertyName("baseLineColor")]
		[JsonConverter(typeof(JsonColorConverter))]
		public Color BaseLineColor
		{
			get => _BaseLineColor;
			set => SetValue(value, ref _BaseLineColor);
		}

		/// <summary>
		/// Base line style. Suitable for percentage and indexedTo100 scales.
		/// </summary>
		[JsonPropertyName("baseLineStyle")]
		public LineStyle BaseLineStyle
		{
			get => _BaseLineStyle;
			set => SetValue(value, ref _BaseLineStyle);
		}

		/// <summary>
		/// Price format.
		/// </summary>
		[JsonPropertyName("priceFormat")]
		public PriceFormatOptions PriceFormat
		{
			get => _PriceFormat;
			set => SetValue(value, ref _PriceFormat);
		}

		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/SeriesOptionsCommon#autoscaleinfoprovider"/>
	}
}
