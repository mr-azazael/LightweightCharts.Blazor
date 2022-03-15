using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Customization
{
	/// <summary>
	/// Exposes the apply and get options methods.
	/// </summary>
	/// <typeparam name="O">Options type.</typeparam>
	public interface ICustomizableObject<O> : IJsObjectWrapper where O : class, new()
	{
		/// <summary>
		/// Get the currently applied options.
		/// </summary>
		Task<O> Options();

		/// <summary>
		/// Apply options to this object.
		/// </summary>
		Task ApplyOptions(O options);
	}

	internal abstract class CustomizableObject<O> : ICustomizableObject<O> where O : class, new()
	{
		public CustomizableObject(IJSObjectReference jsObjectReference)
		{
			JsObjectReference = jsObjectReference;
		}

		protected virtual string _GetOptionsMethod => "options";
		protected virtual string _SetOptionsMethod => "applyOptions";

		public IJSObjectReference JsObjectReference { get; }

		public async Task<O> Options()
			=> await JsObjectReference.InvokeAsync<O>(_GetOptionsMethod);

		public async Task ApplyOptions(O options)
			=> await JsObjectReference.InvokeVoidAsync(_SetOptionsMethod, options ?? new O());
	}
}
