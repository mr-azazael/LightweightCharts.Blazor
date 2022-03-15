using LightweightCharts.Blazor.Utilities;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Models
{
	/// <summary>
	/// Represents a generic range from one value to another.<br/>
	/// https://tradingview.github.io/lightweight-charts/docs/api/interfaces/Range
	/// </summary>
	public class Range<T>
	{
		protected T _From;
		protected T _To;

		/// <summary>
		/// The from value. The start of the range.
		/// </summary>
		[JsonPropertyName("from")]
		public T From
		{
			get => _From;
			set => Extensions.SetValue(ref _From, value, OnFromChanged);
		}

		protected virtual void OnFromChanged(T obj)
		{

		}

		/// <summary>
		/// The to value. The end of the range.
		/// </summary>
		[JsonPropertyName("to")]
		public T To
		{
			get => _To;
			set => Extensions.SetValue(ref _To, value, OnToChanged);
		}

		protected virtual void OnToChanged(T obj)
		{

		}
	}
}
