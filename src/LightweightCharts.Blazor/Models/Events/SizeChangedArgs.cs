using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models.Events
{
	/// <summary>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api#sizechangeeventhandler"/>
	/// </summary>
	public class SizeChangedArgs
	{
		/// <summary>
		/// 
		/// </summary>
		[JsonPropertyName("width")]
		public double Width { get; set; }

		/// <summary>
		/// 
		/// </summary>
		[JsonPropertyName("height")]
		public double Height { get; set; }
	}
}
