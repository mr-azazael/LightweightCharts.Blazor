using System;
using System.Drawing;
using System.Globalization;

namespace LightweightCharts.Blazor.Examples;

class HexColorConverter
{
	public static Color FromString(string hexValue)
	{
		if (!hexValue.StartsWith('#'))
			throw new InvalidOperationException();

		byte r = 0;
		byte g = 0;
		byte b = 0;
		byte a = 255;

		r = byte.Parse(hexValue.Substring(1, 2), NumberStyles.HexNumber);
		g = byte.Parse(hexValue.Substring(3, 2), NumberStyles.HexNumber);
		b = byte.Parse(hexValue.Substring(5, 2), NumberStyles.HexNumber);

		return Color.FromArgb(a, r, g, b);
	}

	public static string FromColor(Color value)
	{
		var hexValue = $"{value.R:X2}{value.G:X2}{value.B:X2}";
		return "#" + hexValue;
	}
}
