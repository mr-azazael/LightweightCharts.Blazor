using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.Models;
using LightweightCharts.Blazor.Models.Events;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Charts
{
	/// <summary>
	/// Interface to chart time scale.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi"/>
	/// </summary>
	public interface ITimeScaleApi<T> : IJsObjectWrapper, ICustomizableObject<TimeScaleOptions>
		where T : struct
	{
		/// <summary>
		/// Subscribe to the visible time range change events.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#subscribevisibletimerangechange"/>
		/// </summary>
		event EventHandler<TimeRange> VisibleTimeRangeChanged;

		/// <summary>
		/// Subscribe to the visible logical range change events.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#subscribevisiblelogicalrangechange"/>
		/// </summary>
		event EventHandler<LogicalRange> VisibleLogicalRangeChanged;

		/// <summary>
		/// Adds a subscription to time scale size changes.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#subscribesizechange"/>
		/// </summary>
		event EventHandler<SizeChangedArgs> SizeChanged;

		/// <summary>
		/// Return the distance from the right edge of the time scale to the lastest bar of the series measured in bars.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#scrollposition"/>
		/// </summary>
		Task<long> ScrollPosition();

		/// <summary>
		/// Scrolls the chart to the specified position.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#scrolltoposition"/>
		/// </summary>
		/// <param name="position">Target data position.</param>
		/// <param name="animated">Setting this to true makes the chart scrolling smooth and adds animation.</param>
		Task ScrollToPosition(long position, bool animated);

		/// <summary>
		/// Restores default scroll position of the chart. This process is always animated.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#scrolltorealtime"/>
		/// </summary>
		Task ScrollToRealTime();

		/// <summary>
		/// Returns current visible time range of the chart.<br/>
		/// Note that this method cannot extrapolate time and will use the only currently existent data.<br/>
		/// To get complete information about current visible range, please use <see cref="GetVisibleLogicalRange"/> and <see cref="Series.ISeriesApi{H}.BarsInLogicalRange"/>.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#getvisiblerange"/>
		/// </summary>
		/// <returns>Visible range or null if the chart has no data at all.</returns>
		Task<TimeRange> GetVisibleRange();

		/// <summary>
		/// Sets visible range of data.
		/// Note that this method cannot extrapolate time and will use the only currently existent data.<br/>
		/// Thus, for example, if currently a chart doesn't have data prior 2018-01-01 date and you set visible range with from date 2016-01-01, it will be automatically adjusted to 2018-01-01 (and the same for to date).<br/>
		/// But if you can approximate indexes on your own - you could use <see cref="SetVisibleLogicalRange"/> instead.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#setvisiblerange"/>
		/// </summary>
		/// <param name="timeRange">Target visible range of data.</param>
		Task SetVisibleRange(TimeRange timeRange);

		/// <summary>
		/// Returns the current visible logical range of the chart as an object with the first and last time points of the logical range, or returns null if the chart has no data.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#getvisiblelogicalrange"/>
		/// </summary>
		/// <returns>Visible range or null if the chart has no data at all.</returns>
		Task<LogicalRange> GetVisibleLogicalRange();

		/// <summary>
		/// Sets visible logical range of data.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#setvisiblelogicalrange"/>
		/// </summary>
		/// <param name="timeRange">Target visible logical range of data.</param>
		Task SetVisibleLogicalRange(LogicalRange timeRange);

		/// <summary>
		/// Restores default zoom level and scroll position of the time scale.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#resettimescale"/>
		/// </summary>
		Task ResetTimeScale();

		/// <summary>
		/// Automatically calculates the visible range to fit all data from all series.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#fitcontent"/>
		/// </summary>
		Task FitContent();

		/// <summary>
		/// Converts a logical index to local x coordinate.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#logicaltocoordinate"/>
		/// </summary>
		/// <param name="logical">Logical index needs to be converted.</param>
		/// <returns>x coordinate of that time or null if the chart doesn't have data.</returns>
		Task<double?> LogicalToCoordinate(double logical);

		/// <summary>
		/// Converts a coordinate to logical index.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#coordinatetological"/>
		/// </summary>
		/// <param name="coordinate">Coordinate needs to be converted.</param>
		/// <returns>Logical index that is located on that coordinate or null if the chart doesn't have data.</returns>
		Task<double?> CoordinateToLogical(double coordinate);

		/// <summary>
		/// Converts a time to local x coordinate.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#timetocoordinate"/>
		/// </summary>
		/// <param name="time">Time needs to be converted.</param>
		/// <returns>X coordinate of that time or null if no time found on time scale.</returns>
		Task<double?> TimeToCoordinate(long time);

		/// <summary>
		/// Converts a coordinate to time.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#coordinatetotime"/>
		/// </summary>
		/// <param name="coordinate">Coordinate needs to be converted.</param>
		/// <returns>Time of a bar that is located on that coordinate or null if there are no bars found on that coordinate.</returns>
		Task<long?> CoordinateToTime(double coordinate);

		/// <summary>
		/// Returns a width of the time scale.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#width"/>
		/// </summary>
		Task<double> Width();

		/// <summary>
		/// Returns a height of the time scale.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ITimeScaleApi#height"/>
		/// </summary>
		Task<double> Height();
	}

	internal class TimeScaleApi<T> : ITimeScaleApi<T>, IAsyncDisposable
		where T : struct
	{
		IJSObjectReference _JsObjectRef;
		EventsHelper _EventsHelper;
		IJSRuntime _JsRuntime;

		public IJSObjectReference JsObjectReference
			=> _JsObjectRef;

		public IChartApiBase<T> ChartLayout { get; }

		internal TimeScaleApi(IChartApiBase<T> chartlayout, IJSObjectReference jsObjectRef, IJSRuntime jsRuntime)
		{
			_JsObjectRef = jsObjectRef;
			_JsRuntime = jsRuntime;
			_EventsHelper = new EventsHelper(this, jsRuntime);
			ChartLayout = chartlayout;
		}

		#region events

		public event EventHandler<TimeRange> VisibleTimeRangeChanged
		{
			add => _EventsHelper.AddEvent(value, Events.VisibleTimeRangeChange);
			remove => _EventsHelper.RemoveEvent(value, Events.VisibleTimeRangeChange);
		}

		public event EventHandler<LogicalRange> VisibleLogicalRangeChanged
		{
			add => _EventsHelper.AddEvent(value, Events.VisibleLogicalRangeChange);
			remove => _EventsHelper.RemoveEvent(value, Events.VisibleLogicalRangeChange);
		}

		public event EventHandler<SizeChangedArgs> SizeChanged
		{
			add => _EventsHelper.AddEvent(value, Events.SizeChanged);
			remove => _EventsHelper.RemoveEvent(value, Events.SizeChanged);
		}

		#endregion

		public async Task<long> ScrollPosition()
			=> await JsModule.InvokeAsync<long>(_JsRuntime, _JsObjectRef, "scrollPosition");

		public async Task ScrollToPosition(long position, bool animated)
			=> await JsModule.InvokeVoidAsync(_JsRuntime, _JsObjectRef, "scrollToPosition", false, position, animated);

		public async Task ScrollToRealTime()
			=> await JsModule.InvokeVoidAsync(_JsRuntime, _JsObjectRef, "scrollToRealTime");

		public async Task<TimeRange> GetVisibleRange()
			=> await JsModule.InvokeAsync<TimeRange>(_JsRuntime, _JsObjectRef, "getVisibleRange");

		public async Task SetVisibleRange(TimeRange timeRange)
			=> await JsModule.InvokeVoidAsync(_JsRuntime, _JsObjectRef, "setVisibleRange", false, timeRange);

		public async Task<LogicalRange> GetVisibleLogicalRange()
			=> await JsModule.InvokeAsync<LogicalRange>(_JsRuntime, _JsObjectRef, "getVisibleLogicalRange");

		public async Task SetVisibleLogicalRange(LogicalRange timeRange)
			=> await JsModule.InvokeVoidAsync(_JsRuntime, _JsObjectRef, "setVisibleLogicalRange", false, timeRange);

		public async Task ResetTimeScale()
			=> await JsModule.InvokeVoidAsync(_JsRuntime, _JsObjectRef, "resetTimeScale");

		public async Task FitContent()
			=> await JsModule.InvokeVoidAsync(_JsRuntime, _JsObjectRef, "fitContent");

		public async Task ApplyOptions(TimeScaleOptions options)
			=> await JsModule.InvokeVoidAsync(_JsRuntime, _JsObjectRef, "applyOptions", true, options);

		public async Task<TimeScaleOptions> Options()
			=> await JsModule.InvokeAsync<TimeScaleOptions>(_JsRuntime, _JsObjectRef, "options");

		public async Task<double?> TimeToCoordinate(long time)
			=> await JsModule.InvokeAsync<double?>(_JsRuntime, _JsObjectRef, "timeToCoordinate", false, time);

		public async Task<long?> CoordinateToTime(double coordinate)
			=> await JsModule.InvokeAsync<long?>(_JsRuntime, _JsObjectRef, "coordinateToTime", false, coordinate);

		public async Task<double?> LogicalToCoordinate(double logical)
			=> await JsModule.InvokeAsync<double?>(_JsRuntime, _JsObjectRef, "logicalToCoordinate", false, logical);

		public async Task<double?> CoordinateToLogical(double coordinate)
			=> await JsModule.InvokeAsync<double?>(_JsRuntime, _JsObjectRef, "coordinateToLogical", false, coordinate);

		public async Task<double> Width()
			=> await JsModule.InvokeAsync<double>(_JsRuntime, _JsObjectRef, "width");

		public async Task<double> Height()
			=> await JsModule.InvokeAsync<double>(_JsRuntime, _JsObjectRef, "height");

		public async ValueTask DisposeAsync()
		{
			await _EventsHelper.DisposeAsync();
		}
	}
}
