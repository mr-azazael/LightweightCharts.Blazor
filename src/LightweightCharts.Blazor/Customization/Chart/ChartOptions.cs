using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure describing options of the chart. Series options are to be set separately.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ChartOptionsBase"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<ChartOptions>))]
	public class ChartOptions : JsonOptionsObject
	{
		/// <summary>
		/// Width of the chart in pixels.
		/// </summary>
		[JsonPropertyName("width")]
		public double Width
		{
			get => GetValue<double>();
			set => SetValue(value);
		}

		/// <summary>
		/// Height of the chart in pixels.
		/// </summary>
		[JsonPropertyName("height")]
		public double Height
		{
			get => GetValue<double>();
			set => SetValue(value);
		}

		/// <summary>
		/// Setting this flag to true will make the chart watch the chart container's size and automatically resize the chart to fit its container whenever the size changes.<br/>
		/// This feature requires ResizeObserver class to be available in the global scope.<br/>
		/// Note that calling code is responsible for providing a polyfill if required. If the global scope does not have<br/>
		/// ResizeObserver, a warning will appear and the flag will be ignored.<br/>
		/// Please pay attention that autoSize option and explicit sizes options width and height don't conflict with one another.<br/>
		/// If you specify autoSize flag, then width and height options will be ignored unless ResizeObserver has failed. If it fails then the values will be used as fallback.<br/>
		/// The flag autoSize could also be set with and unset with applyOptions function.
		/// </summary>
		[JsonPropertyName("autoSize")]
		public bool AutoSize
		{
			get => GetValue<bool>();
			set => SetValue(value);
		}

		/// <summary>
		/// Watermark options.<br/>
		/// A watermark is a background label that includes a brief description of the drawn data.Any text can be added to it.<br/>
		/// Please make sure you enable it and set an appropriate font color and size to make your watermark visible in the background of the chart.<br/>
		/// We recommend a semi-transparent color and a large font. Also note that watermark position can be aligned vertically and horizontally.
		/// </summary>
		[JsonPropertyName("watermark")]
		public WatermarkOptions Watermark
		{
			get => GetValue(() => new WatermarkOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Layout options.
		/// </summary>
		[JsonPropertyName("layout")]
		public LayoutOptions Layout
		{
			get => GetValue(() => new LayoutOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Left price scale options.
		/// </summary>
		[JsonPropertyName("leftPriceScale")]
		public PriceScaleOptions LeftPriceScale
		{
			get => GetValue(() => new PriceScaleOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Right price scale options.
		/// </summary>
		[JsonPropertyName("rightPriceScale")]
		public PriceScaleOptions RightPriceScale
		{
			get => GetValue(() => new PriceScaleOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Overlay price scale options.
		/// </summary>
		[JsonPropertyName("overlayPriceScales")]
		public BasePriceScaleOptions OverlayPriceScale
		{
			get => GetValue(() => new BasePriceScaleOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Time scale options.
		/// </summary>
		[JsonPropertyName("timeScale")]
		public TimeScaleOptions TimeScale
		{
			get => GetValue(() => new TimeScaleOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// The crosshair shows the intersection of the price and time scale values at any point on the chart.
		/// </summary>
		[JsonPropertyName("crosshair")]
		public CrosshairOptions Crosshair
		{
			get => GetValue(() => new CrosshairOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// A grid is represented in the chart background as a vertical and horizontal lines drawn at the levels of visible marks of price and the time scales.
		/// </summary>
		[JsonPropertyName("grid")]
		public GridOptions Grid
		{
			get => GetValue(() => new GridOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Scroll options.
		/// </summary>
		[JsonPropertyName("handleScroll")]
		public HandleScrollOptions HandleScroll
		{
			get => GetValue(() => new HandleScrollOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Scale options.
		/// </summary>
		[JsonPropertyName("handleScale")]
		public HandleScaleOptions HandleScale
		{
			get => GetValue(() => new HandleScaleOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Kinetic scroll options.
		/// </summary>
		[JsonPropertyName("kineticScroll")]
		public KineticScrollOptions KineticScroll
		{
			get => GetValue(() => new KineticScrollOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Represent options for the tracking mode's behavior.<br/>
		/// Mobile users will not have the ability to see the values/dates like they do on desktop.<br/>
		/// To see it, they should enter the tracking mode. <br/>
		/// The tracking mode will deactivate the scrolling and make it possible to check values and dates.
		/// </summary>
		[JsonPropertyName("trackingMode")]
		public TrackingModeOptions TrackingMode
		{
			get => GetValue(() => new TrackingModeOptions());
			set => SetValue(value);
		}

		/// <summary>
		/// Localization options.
		/// </summary>
		[JsonPropertyName("localization")]
		public LocalizationOptions Localization
		{
			get => GetValue(() => new LocalizationOptions());
			set => SetValue(value);
		}
	}
}
