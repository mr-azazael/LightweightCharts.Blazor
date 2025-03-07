using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Customization
{
	/// <summary>
	/// Exposes the apply and get options methods.
	/// </summary>
	/// <typeparam name="O">Options type.</typeparam>
	public interface ICustomizableObject<O> : IJsObjectWrapper
		where O : class, new()
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

	internal abstract class CustomizableObject<O> : ICustomizableObject<O>
		where O : class, new()
	{
		public CustomizableObject(IJSRuntime jsRuntime, IJSObjectReference jsObjectReference)
		{
			JsRuntime = jsRuntime;
			JsObjectReference = jsObjectReference;
		}

		protected virtual string GetOptionsMethodName
			=> "options";

		protected virtual string SetOptionsMethodName
			=> "applyOptions";

		public IJSRuntime JsRuntime { get; }

		public IJSObjectReference JsObjectReference { get; }

		public virtual async Task<O> Options()
			=> await JsModule.InvokeAsync<O>(JsRuntime, JsObjectReference, GetOptionsMethodName);

		public virtual async Task ApplyOptions(O options)
			=> await JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, SetOptionsMethodName, true, options ?? new O());
	}
}
