namespace LightweightCharts.Blazor.Customization.Enums
{
	/// <summary>
	/// Search direction if no data found at provided index<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/enums/MismatchDirection"/>
	/// </summary>
	public enum MismatchDirection
	{
		/// <summary>
		/// Search the nearest left item
		/// </summary>
		NearestLeft = -1,

		/// <summary>
		/// Do not search
		/// </summary>
		None = 0,

		/// <summary>
		/// Search the nearest right item
		/// </summary>
		NearestRight = 1
	}
}
