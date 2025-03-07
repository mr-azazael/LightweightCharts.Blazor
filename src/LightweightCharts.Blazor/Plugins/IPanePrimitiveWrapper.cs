using LightweightCharts.Blazor.Charts;
using LightweightCharts.Blazor.Customization;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Plugins
{
	/// <summary>
	/// Interface for a pane primitive.<br/>
	/// No options method available in js. The value returned by <see cref="ICustomizableObject{O}.Options"/> is the last value applied.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPanePrimitiveWrapper"/>
	/// </summary>
	public interface IPanePrimitiveWrapper<O> : ICustomizableObject<O>
		where O : class, new()
	{
		/// <summary>
		/// Detaches the plugin from the pane.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPanePrimitiveWrapper#detach"/>
		/// </summary>
		ValueTask Detach();

		/// <summary>
		/// Returns the current pane.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/IPanePrimitiveWrapper#getpane"/>
		/// </summary>
		IPaneApi GetPane();
	}
}
