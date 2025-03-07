using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Plugins
{
	/// <summary>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/type-aliases/ITextWatermarkPluginApi"/>
	/// </summary>
	public interface ITextWatermarkPluginApi : IPanePrimitiveWrapper<TextWatermarkOptions>
	{

	}

	class TextWatermarkPluginApi : CustomizableObject<TextWatermarkOptions>, ITextWatermarkPluginApi
	{
		public TextWatermarkPluginApi(IJSRuntime jsRuntime, IPaneApi owner, IJSObjectReference jsObjectReference, TextWatermarkOptions options)
			: base(jsRuntime, jsObjectReference)
		{
			_Owner = owner;
			_Options = options;
		}

		IPaneApi _Owner;
		TextWatermarkOptions _Options;

		public ValueTask Detach()
			=> JsObjectReference.InvokeVoidAsync("detach");

		public IPaneApi GetPane()
			=> _Owner;

		public override Task<TextWatermarkOptions> Options()
			=> Task.FromResult(_Options);

		public override Task ApplyOptions(TextWatermarkOptions options)
		{
			_Options = options ?? new();
			return base.ApplyOptions(_Options);
		}
	}
}
