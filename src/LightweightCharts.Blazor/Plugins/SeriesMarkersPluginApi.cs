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
	public interface ISeriesMarkersPluginApi : ISeriesPrimitiveWrapper<object>
	{
		/// <summary>
		/// Set markers to the series.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesMarkersPluginApi#setmarkers"/>
		/// </summary>
		/// <param name="markers">An array of markers to be displayed on the series.</param>
		ValueTask SetMarkers(SeriesMarker[] markers);

		/// <summary>
		/// Returns current markers.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesMarkersPluginApi#markers"/>
		/// </summary>
		SeriesMarker[] Markers();
	}

	class SeriesMarkersPluginApi : CustomizableObject<object>, ISeriesMarkersPluginApi
	{
		public SeriesMarkersPluginApi(IJSRuntime jsRuntime, ISeriesApi owner, IJSObjectReference jsObjectReference, SeriesMarker[] markers)
			: base(jsRuntime, jsObjectReference)
		{
			_Owner = owner;
			_Markers = markers;
		}

		ISeriesApi _Owner;
		SeriesMarker[] _Markers;

		public ValueTask SetMarkers(SeriesMarker[] markers)
		{
			_Markers = markers ?? [];
			return JsObjectReference.InvokeVoidAsync("setMarkers", _Markers);
		}

		public SeriesMarker[] Markers()
			=> _Markers;

		public ISeriesApi GetSeries()
			=> _Owner;

		public ValueTask Detach()
			=> JsObjectReference.InvokeVoidAsync("detach");

		public override Task ApplyOptions(object options)
			=> Task.CompletedTask;

		public override Task<object> Options()
			=> null;
	}
}
