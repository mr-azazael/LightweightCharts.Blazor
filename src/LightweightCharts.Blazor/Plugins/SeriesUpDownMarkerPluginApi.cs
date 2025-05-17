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
	public interface ISeriesUpDownMarkerPluginApi<H> : ISeriesPrimitiveWrapper<H, UpDownMarkersPluginOptions>
		where H : struct
	{
		/// <summary>
		/// Sets the data for the series and manages data points for marker updates.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesUpDownMarkerPluginApi#setdata"/>
		/// </summary>
		/// <param name="data">Array of data points to set.</param>
		ValueTask SetData(IEnumerable<ISeriesData<H>> data);

		/// <summary>
		/// Updates a single data point and manages marker updates for existing data points.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesUpDownMarkerPluginApi#update"/>
		/// </summary>
		/// <param name="data">The data point to update.</param>
		/// <param name="historicalUpdate">Optional flag for historical updates.</param>
		ValueTask Update(ISeriesData<H> data, bool? historicalUpdate = null);

		/// <summary>
		/// Retrieves the current markers on the chart.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesUpDownMarkerPluginApi#markers"/>
		/// </summary>
		ValueTask<SeriesUpDownMarker<H>[]> Markers();

		/// <summary>
		/// Manually sets markers on the chart.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesUpDownMarkerPluginApi#setmarkers"/>
		/// </summary>
		/// <param name="markers">Array of SeriesUpDownMarker to set.</param>
		ValueTask SetMarkers(IEnumerable<SeriesUpDownMarker<H>> markers);

		/// <summary>
		/// Clears all markers from the chart.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesUpDownMarkerPluginApi#clearmarkers"/>
		/// </summary>
		ValueTask ClearMarkers();
	}

	class SeriesUpDownMarkerPluginApi<H> : CustomizableObject<UpDownMarkersPluginOptions>, ISeriesUpDownMarkerPluginApi<H>
		where H : struct
	{
		public SeriesUpDownMarkerPluginApi(IJSRuntime jsRuntime, IJSObjectReference jsObjectReference, ISeriesApi<H> parent)
			: base(jsRuntime, jsObjectReference)
		{
			_SeriesApi = parent;
		}

		ISeriesApi<H> _SeriesApi;

		public ValueTask SetData(IEnumerable<ISeriesData<H>> data)
			=> JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "setData", false, data);

		public ValueTask Update(ISeriesData<H> data, bool? historicalUpdate)
			=> JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "update", false, data, historicalUpdate);

		public ValueTask<SeriesUpDownMarker<H>[]> Markers()
			=> JsModule.InvokeAsync<SeriesUpDownMarker<H>[]>(JsRuntime, JsObjectReference, "markers");

		public ValueTask SetMarkers(IEnumerable<SeriesUpDownMarker<H>> markers)
			=> JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "setMarkers", false, markers);

		public ValueTask ClearMarkers()
			=> JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "clearMarkers");

		public ValueTask Detach()
			=> JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "detach");

		public ISeriesApi<H> GetSeries()
			=> _SeriesApi;
	}
}
