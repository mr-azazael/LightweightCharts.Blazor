using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Structure describing options of the chart. Series options are to be set separately.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ChartOptionsBase"/>
	/// </summary>
	public class ChartOptionsBase : BaseModel
	{
		double _Width;
		double _Height;
		bool _AutoSize;
		WatermarkOptions _Watermark = new();
		LayoutOptions _Layout = new();
		PriceScaleOptions _LeftPriceScale = new();
		PriceScaleOptions _RightPriceScale = new();
		BasePriceScaleOptions _OverlayPriceScale = new();
		TimeScaleOptions _TimeScale = new();
		CrosshairOptions _Crosshair = new();
		GridOptions _Grid = new();
		HandleScrollOptions _HandleScroll = new();
		HandleScaleOptions _HandleScale = new();
		KineticScrollOptions _KineticScroll = new();
		TrackingModeOptions _TrackingMode = new();
		LocalizationOptions _Localization = new();

		/// <summary>
		/// Width of the chart in pixels.
		/// </summary>
		[JsonPropertyName("width")]
		public double Width
		{
			get => _Width;
			set => SetValue(value, ref _Width);
		}

		/// <summary>
		/// Height of the chart in pixels.
		/// </summary>
		[JsonPropertyName("height")]
		public double Height
		{
			get => _Height;
			set => SetValue(value, ref _Height);
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
			get => _AutoSize;
			set => SetValue(value, ref _AutoSize);
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
			get => _Watermark;
			set => SetValue(value, ref _Watermark);
		}

		/// <summary>
		/// Layout options.
		/// </summary>
		[JsonPropertyName("layout")]
		public LayoutOptions Layout
		{
			get => _Layout;
			set => SetValue(value, ref _Layout);
		}

		/// <summary>
		/// Left price scale options.
		/// </summary>
		[JsonPropertyName("leftPriceScale")]
		public PriceScaleOptions LeftPriceScale
		{
			get => _LeftPriceScale;
			set => SetValue(value, ref _LeftPriceScale);
		}

		/// <summary>
		/// Right price scale options.
		/// </summary>
		[JsonPropertyName("rightPriceScale")]
		public PriceScaleOptions RightPriceScale
		{
			get => _RightPriceScale;
			set => SetValue(value, ref _RightPriceScale);
		}

		/// <summary>
		/// Overlay price scale options.
		/// </summary>
		[JsonPropertyName("overlayPriceScales")]
		public BasePriceScaleOptions OverlayPriceScale
		{
			get => _OverlayPriceScale;
			set => SetValue(value, ref _OverlayPriceScale);
		}

		/// <summary>
		/// Time scale options.
		/// </summary>
		[JsonPropertyName("timeScale")]
		public TimeScaleOptions TimeScale
		{
			get => _TimeScale;
			set => SetValue(value, ref _TimeScale);
		}

		/// <summary>
		/// The crosshair shows the intersection of the price and time scale values at any point on the chart.
		/// </summary>
		[JsonPropertyName("crosshair")]
		public CrosshairOptions Crosshair
		{
			get => _Crosshair;
			set => SetValue(value, ref _Crosshair);
		}

		/// <summary>
		/// A grid is represented in the chart background as a vertical and horizontal lines drawn at the levels of visible marks of price and the time scales.
		/// </summary>
		[JsonPropertyName("grid")]
		public GridOptions Grid
		{
			get => _Grid;
			set => SetValue(value, ref _Grid);
		}

		/// <summary>
		/// Scroll options.
		/// </summary>
		[JsonPropertyName("handleScroll")]
		public HandleScrollOptions HandleScroll
		{
			get => _HandleScroll;
			set => SetValue(value, ref _HandleScroll);
		}

		/// <summary>
		/// Scale options.
		/// </summary>
		[JsonPropertyName("handleScale")]
		public HandleScaleOptions HandleScale
		{
			get => _HandleScale;
			set => SetValue(value, ref _HandleScale);
		}

		/// <summary>
		/// Kinetic scroll options.
		/// </summary>
		[JsonPropertyName("kineticScroll")]
		public KineticScrollOptions KineticScroll
		{
			get => _KineticScroll;
			set => SetValue(value, ref _KineticScroll);
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
			get => _TrackingMode;
			set => SetValue(value, ref _TrackingMode);
		}

		/// <summary>
		/// Localization options.
		/// </summary>
		[JsonPropertyName("localization")]
		public LocalizationOptions Localization
		{
			get => _Localization;
			set => SetValue(value, ref _Localization);
		}
	}
}
