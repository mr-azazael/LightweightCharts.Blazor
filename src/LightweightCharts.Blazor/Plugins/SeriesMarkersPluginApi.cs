using LightweightCharts.Blazor.Customization;
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
	public interface ISeriesMarkersPluginApi<H> : ISeriesPrimitiveWrapper<H, object>
		where H : struct
	{
		/// <summary>
		/// Set markers to the series.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesMarkersPluginApi#setmarkers"/>
		/// </summary>
		/// <param name="markers">An array of markers to be displayed on the series.</param>
		ValueTask SetMarkers(SeriesMarker<H>[] markers);

		/// <summary>
		/// Returns current markers.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesMarkersPluginApi#markers"/>
		/// </summary>
		SeriesMarker<H>[] Markers();
	}

	class SeriesMarkersPluginApi<H> : CustomizableObject<object>, ISeriesMarkersPluginApi<H>
		where H : struct
	{
		public SeriesMarkersPluginApi(IJSRuntime jsRuntime, ISeriesApi<H> owner, IJSObjectReference jsObjectReference, SeriesMarker<H>[] markers)
			: base(jsRuntime, jsObjectReference)
		{
			_Owner = owner;
			_Markers = markers;
		}

		ISeriesApi<H> _Owner;
		SeriesMarker<H>[] _Markers;

		public ValueTask SetMarkers(SeriesMarker<H>[] markers)
		{
			_Markers = markers ?? [];
			return JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "setMarkers", false, _Markers);
		}

		public SeriesMarker<H>[] Markers()
			=> _Markers;

		public ISeriesApi<H> GetSeries()
			=> _Owner;

		public ValueTask Detach()
			=> JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "detach");

		public override Task ApplyOptions(object options)
			=> Task.CompletedTask;

		public override Task<object> Options()
			=> null;
	}
}
