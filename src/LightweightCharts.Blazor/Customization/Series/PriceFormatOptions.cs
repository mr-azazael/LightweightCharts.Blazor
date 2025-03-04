using LightweightCharts.Blazor.Customization.Enums;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents series value formatting options. The precision and minMove properties allow wide customization of formatting.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceFormatBuiltIn"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<PriceFormatOptions>))]
	public class PriceFormatOptions : JsonOptionsObject
	{
		/// <summary>
		/// Sets a type of price displayed by series.
		/// </summary>
		[JsonPropertyName("type")]
		public PriceFormatType Type
		{
			get => GetValue(() => PriceFormatType.Price);
			set => SetValue(value);
		}

		/// <summary>
		/// Number of digits after the decimal point. If it is not set, then its value is calculated automatically based on minMove.
		/// </summary>
		[JsonPropertyName("precision")]
		public int Precision
		{
			get => GetValue(() => 2);
			set => SetValue(value);
		}

		/// <summary>
		/// The minimum possible step size for price value movement. This value shouldn't have more decimal digits than the precision.
		/// </summary>
		[JsonPropertyName("minMove")]
		public double MinimumMove
		{
			get => GetValue(() => 0.01);
			set => SetValue(value);
		}
	}
}
