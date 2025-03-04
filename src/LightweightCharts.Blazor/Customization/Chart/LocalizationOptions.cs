using System.Globalization;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Represents options for formatting dates, times, and prices according to a locale.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api/interfaces/LocalizationOptions"/>
	/// </summary>
	[JsonConverter(typeof(JsonOptionsObjectConverter<LocalizationOptions>))]
	public class LocalizationOptions : JsonOptionsObject
	{
		/// <summary>
		/// Current locale used to format dates. Uses the browser's language settings by default.<br/>
		/// </summary>
		[JsonPropertyName("locale")]
		public string Locale
		{
			get => GetValue(() => CultureInfo.CurrentCulture.Name);
			set => SetValue(value);
		}

		/// <summary>
		/// Date formatting string.<br/>
		/// Can contain yyyy, yy, MMMM, MMM, MM and dd literals which will be replaced with corresponding date's value.<br/>
		/// Ignored if timeFormatter has been specified.
		/// </summary>
		[JsonPropertyName("dateFormat")]
		public string DateFormat
		{
			get => GetValue(() => "dd MMM \'yy");
			set => SetValue(value);
		}
	}
}
