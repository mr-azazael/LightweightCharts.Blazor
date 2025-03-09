using LightweightCharts.Blazor.Customization;
using LightweightCharts.Blazor.Series;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Plugins
{
	/// <summary>
	/// Interface for a series primitive.<br/>
	/// No options method available in js. The value returned by <see cref="ICustomizableObject{O}.Options"/> is the last value applied.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesPrimitiveWrapper"/>
	/// </summary>
	public interface ISeriesPrimitiveWrapper<H, O> : ICustomizableObject<O>
		where H : struct
		where O : class, new()
	{
		/// <summary>
		/// Detaches the plugin from the series.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesPrimitiveWrapper#detach"/>
		/// </summary>
		ValueTask Detach();

		/// <summary>
		/// Returns the current series.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesPrimitiveWrapper#getseries"/>
		/// </summary>
		ISeriesApi<H> GetSeries();
	}
}
