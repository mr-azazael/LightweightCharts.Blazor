using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Series;
using Microsoft.JSInterop;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Plugins
{
	/// <summary>
	/// UpDownMarkersPrimitive Plugin for showing the direction of price changes on the chart.<br/>
	/// This plugin can only be used with Line and Area series types.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesUpDownMarkerPluginApi"/>
	/// </summary>
	public interface ISeriesUpDownMarkerPluginApi : ISeriesPrimitiveWrapper<UpDownMarkersPluginOptions>
	{
		/// <summary>
		/// Sets the data for the series and manages data points for marker updates.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesUpDownMarkerPluginApi#setdata"/>
		/// </summary>
		/// <param name="data">Array of data points to set.</param>
		ValueTask SetData(IEnumerable<ISeriesData> data);

		/// <summary>
		/// Updates a single data point and manages marker updates for existing data points.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesUpDownMarkerPluginApi#update"/>
		/// </summary>
		/// <param name="data">The data point to update.</param>
		/// <param name="historicalUpdate">Optional flag for historical updates.</param>
		ValueTask Update(ISeriesData data, bool? historicalUpdate = null);

		/// <summary>
		/// Retrieves the current markers on the chart.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesUpDownMarkerPluginApi#markers"/>
		/// </summary>
		ValueTask<SeriesUpDownMarker[]> Markers();

		/// <summary>
		/// Manually sets markers on the chart.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesUpDownMarkerPluginApi#setmarkers"/>
		/// </summary>
		/// <param name="markers">Array of SeriesUpDownMarker to set.</param>
		ValueTask SetMarkers(IEnumerable<SeriesUpDownMarker> markers);

		/// <summary>
		/// Clears all markers from the chart.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesUpDownMarkerPluginApi#clearmarkers"/>
		/// </summary>
		ValueTask ClearMarkers();
	}

	class SeriesUpDownMarkerPluginApi : CustomizableObject<UpDownMarkersPluginOptions>, ISeriesUpDownMarkerPluginApi
	{
		public SeriesUpDownMarkerPluginApi(IJSRuntime jsRuntime, IJSObjectReference jsObjectReference, ISeriesApi parent)
			: base(jsRuntime, jsObjectReference)
		{
			_SeriesApi = parent;
		}

		ISeriesApi _SeriesApi;

		public ValueTask SetData(IEnumerable<ISeriesData> data)
			=> JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "setData", false, data);

		public ValueTask Update(ISeriesData data, bool? historicalUpdate)
			=> JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "update", false, data, historicalUpdate);

		public ValueTask<SeriesUpDownMarker[]> Markers()
			=> JsObjectReference.InvokeAsync<SeriesUpDownMarker[]>("markers");

		public ValueTask SetMarkers(IEnumerable<SeriesUpDownMarker> markers)
			=> JsObjectReference.InvokeVoidAsync("setMarkers", markers);

		public ValueTask ClearMarkers()
			=> JsObjectReference.InvokeVoidAsync("clearMarkers");

		public ValueTask Detach()
			=> JsObjectReference.InvokeVoidAsync("detach");

		public ISeriesApi GetSeries()
			=> _SeriesApi;
	}
}
