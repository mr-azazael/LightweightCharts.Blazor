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
	public interface ITextWatermarkPluginApi<H> : IPanePrimitiveWrapper<H, TextWatermarkOptions>
		where H : struct
	{

	}

	class TextWatermarkPluginApi<H> : CustomizableObject<TextWatermarkOptions>, ITextWatermarkPluginApi<H>
		where H : struct
	{
		public TextWatermarkPluginApi(IJSRuntime jsRuntime, IPaneApi<H> owner, IJSObjectReference jsObjectReference, TextWatermarkOptions options)
			: base(jsRuntime, jsObjectReference)
		{
			_Owner = owner;
			_Options = options;
		}

		IPaneApi<H> _Owner;
		TextWatermarkOptions _Options;

		public ValueTask Detach()
			=> JsModule.InvokeVoidAsync(JsRuntime, JsObjectReference, "detach");

		public IPaneApi<H> GetPane()
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
