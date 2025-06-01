using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using LightweightCharts.Blazor.DataItems;
using LightweightCharts.Blazor.Series;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Plugins
{
	/// <summary>
	/// Interface for a series markers plugin.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesMarkersPluginApi"/>
	/// </summary>
	public interface ISeriesMarkersPluginApi<H> : ISeriesPrimitiveWrapper<H, SeriesMarkersOptions>
		where H : struct
	{
		/// <summary>
		/// Set markers to the series.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesMarkersPluginApi#setmarkers"/>
		/// </summary>
		/// <param name="markers">An array of markers to be displayed on the series.</param>
		ValueTask SetMarkers(SeriesMarkerBase<H>[] markers);

		/// <summary>
		/// Returns current markers.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesMarkersPluginApi#markers"/>
		/// </summary>
		SeriesMarkerBase<H>[] Markers();
	}

	class SeriesMarkersPluginApi<H> : CustomizableObject<SeriesMarkersOptions>, ISeriesMarkersPluginApi<H>
		where H : struct
	{
		public SeriesMarkersPluginApi(IJSRuntime jsRuntime, ISeriesApi<H> owner, IJSObjectReference jsObjectReference, SeriesMarkerBase<H>[] markers, SeriesMarkersOptions options = null)
			: base(jsRuntime, jsObjectReference)
		{
			_Owner = owner;
			_Markers = markers;
			_Options = options ?? new();
		}

		ISeriesApi<H> _Owner;
		SeriesMarkerBase<H>[] _Markers;
		SeriesMarkersOptions _Options;

		public ValueTask SetMarkers(SeriesMarkerBase<H>[] markers)
		{
			_Markers = markers ?? [];
			return JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "setMarkers", false, _Markers);
		}

		public SeriesMarkerBase<H>[] Markers()
			=> _Markers;

		public ISeriesApi<H> GetSeries()
			=> _Owner;

		public ValueTask Detach()
			=> JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "detach");

		public override Task<SeriesMarkersOptions> Options()
			=> Task.FromResult(_Options);

		public override Task ApplyOptions(SeriesMarkersOptions options)
		{
			_Options = options ?? new();
			return base.ApplyOptions(_Options);
		}
	}
}
