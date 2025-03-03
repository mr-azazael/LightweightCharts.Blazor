using System.Drawing;
using System.Threading.Tasks;

namespace LightweightCharts.Blazor.Charts
{
	/// <summary>
	/// This interface represents a label on the price or time axis.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesPrimitiveAxisView"/>
	/// </summary>
	public interface ISeriesPrimitiveAxisView
	{
		/// <summary>
		/// The desired coordinate for the label. Note that the label will be automatically moved to prevent overlapping with other labels.<br/>
		/// If you would like the label to be drawn at the exact coordinate under all circumstances then rather use fixedCoordinate.<br/>
		/// For a price axis the value returned will represent the vertical distance (pixels) from the top.<br/>
		/// For a time axis the value will represent the horizontal distance from the left.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesPrimitiveAxisView#coordinate"/>
		/// </summary>
		/// <returns>Distance from top for price axis, or distance from left for time axis.</returns>
		Task<int> Coordinate();

		/// <summary>
		/// Fixed coordinate of the label. A label with a fixed coordinate value will always be drawn at the specified coordinate and will appear above any 'unfixed' labels.<br/>
		/// If you supply a fixed coordinate then you should return a large negative number for coordinate so that the automatic placement of<br/>
		/// unfixed labels doesn't leave a blank space for this label.<br/>
		/// For a price axis the value returned will represent the vertical distance (pixels) from the top.<br/>
		/// For a time axis the value will represent the horizontal distance from the left.<br/>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesPrimitiveAxisView#fixedcoordinate"/>
		/// </summary>
		/// <returns>Distance from top for price axis, or distance from left for time axis.</returns>
		Task<int?> FixedCoordinate();

		/// <summary>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesPrimitiveAxisView#text"/>
		/// </summary>
		/// <returns>Text of the label.</returns>
		Task<string> Text();

		/// <summary>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesPrimitiveAxisView#textcolor"/>
		/// </summary>
		/// <returns>Text color of the label.</returns>
		Task<Color> TextColor();

		/// <summary>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesPrimitiveAxisView#backcolor"/>
		/// </summary>
		/// <returns>Background color of the label.</returns>
		Task<Color> BackColor();

		/// <summary>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesPrimitiveAxisView#visible"/>
		/// </summary>
		/// <returns>Whether the label should be visible (default: true)</returns>
		Task<bool?> Visible();

		/// <summary>
		/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/ISeriesPrimitiveAxisView#tickvisible"/>
		/// </summary>
		/// <returns>Whether the tick mark line should be visible (default: true)</returns>
		Task<bool?> TickVisible();
	}
}
