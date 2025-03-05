using Microsoft.JSInterop;

namespace LightweightCharts.Blazor
{
	/// <summary>
	/// wrapper interface for a <see cref="IJSObjectReference"/>
	/// </summary>
	public interface IJsObjectWrapper
	{
		/// <summary>
		/// The javascript object created by the trading view library.
		/// </summary>
		public IJSObjectReference JsObjectReference { get; }
	}
}
