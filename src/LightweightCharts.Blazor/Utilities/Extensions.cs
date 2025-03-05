using System;
using System.Drawing;

namespace LightweightCharts.Blazor.Utilities
{
	/// <summary>
	/// expension methods
	/// </summary>
	public static class Extensions
	{
		internal static void SetValue<T>(ref T field, T value, Action<T> propertyChanged)
		{
			if (!Equals(field, value))
			{
				var prev = field;
				field = value;
				propertyChanged?.Invoke(prev);
			}
		}

		/// <summary>
		/// unix timestamp to DateTime
		/// </summary>
		/// <param name="unixTime"></param>
		/// <returns></returns>
		public static DateTime UnixTimeToDateTime(long unixTime)
			=> DateTimeOffset.FromUnixTimeSeconds(unixTime).DateTime;

		/// <summary>
		/// DateTime to unix timestamp
		/// </summary>
		/// <param name="dateTime"></param>
		/// <returns></returns>
		public static long DateTimeToUnixTime(DateTime dateTime)
			=> new DateTimeOffset(dateTime).ToUnixTimeSeconds();

		/// <summary>
		/// Accepted formats are RGB and RGBA with two values per channel (ex: #FF0000 = red, #00FF0088 = green, 50% alpha)
		/// </summary>
		public static Color ParseHtmlCode(string code)
		{
			if (code.StartsWith("#"))
				code = code[1..];

			if (code.Length != 6 && code.Length != 8)
				throw new InvalidOperationException("Invalid color code length (expected 6 or 8).");

			var r = Convert.ToInt32(code[0..2], 16);
			var g = Convert.ToInt32(code[2..4], 16);
			var b = Convert.ToInt32(code[4..6], 16);
			var a = code.Length == 8 ? Convert.ToInt32(code[6..8], 16) : 255;
			return Color.FromArgb(a, r, g, b);
		}
	}
}
