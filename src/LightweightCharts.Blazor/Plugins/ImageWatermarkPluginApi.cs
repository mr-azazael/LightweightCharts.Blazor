using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Customization.Chart;
using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Plugins
{
	/// <summary>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/type-aliases/IImageWatermarkPluginApi"/>
	/// </summary>
	public interface IImageWatermarkPluginApi : IPanePrimitiveWrapper<ImageWatermarkOptions>
	{

	}

	class ImageWatermarkPluginApi : CustomizableObject<ImageWatermarkOptions>, IImageWatermarkPluginApi
	{
		public ImageWatermarkPluginApi(IJSRuntime jsRuntime, IPaneApi owner, IJSObjectReference jSObject, ImageWatermarkOptions options)
			: base(jsRuntime, jSObject)
		{
			_Owner = owner;
			_Options = options;
		}

		IPaneApi _Owner;
		ImageWatermarkOptions _Options;

		public ValueTask Detach()
			=> JsObjectReference.InvokeVoidAsync("detach");

		public IPaneApi GetPane()
			=> _Owner;

		public override Task<ImageWatermarkOptions> Options()
			=> Task.FromResult(_Options);

		public override Task ApplyOptions(ImageWatermarkOptions options)
		{
			_Options = options ?? new();
			return base.ApplyOptions(_Options);
		}
	}
}
