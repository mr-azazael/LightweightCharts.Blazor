using Microsoft.JSInterop;

namespace LightweightCharts.Blazor
{
	public interface IJsObjectWrapper
	{
		/// <summary>
		/// The javascript object created by the trading view library.
		/// </summary>
		public IJSObjectReference JsObjectReference { get; }
	}
}
