using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// 
	/// <see href=""/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<ChartOptions>))]
	public class ChartOptions : ChartOptionsBase<LocalizationOptions>
	{

	}
}
