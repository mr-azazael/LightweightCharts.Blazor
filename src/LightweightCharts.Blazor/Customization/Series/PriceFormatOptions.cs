using System.Text.Json.Serialization;
using LightweightCharts.Blazor.Customization.Enums;

namespace LightweightCharts.Blazor.Customization.Series
{
	/// <summary>
	/// Represents series value formatting options. The precision and minMove properties allow wide customization of formatting.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/PriceFormatBuiltIn"/>
	/// </summary>
	public class PriceFormatOptions : BaseModel
	{
		PriceFormatType _Type = PriceFormatType.Price;
		int _Precision = 2;
		double _MinimumMove = 0.01d;

		/// <summary>
		/// Sets a type of price displayed by series.
		/// </summary>
		[JsonPropertyName("type")]
		public PriceFormatType Type
		{
			get => _Type;
			set => SetValue(value, ref _Type);
		}

		/// <summary>
		/// Number of digits after the decimal point. If it is not set, then its value is calculated automatically based on minMove.
		/// </summary>
		[JsonPropertyName("precision")]
		public int Precision
		{
			get => _Precision;
			set => SetValue(value, ref _Precision);
		}

		/// <summary>
		/// The minimum possible step size for price value movement. This value shouldn't have more decimal digits than the precision.
		/// </summary>
		[JsonPropertyName("minMove")]
		public double MinimumMove
		{
			get => _MinimumMove;
			set => SetValue(value, ref _MinimumMove);
		}
	}
}
