﻿using LightweightCharts.Blazor.Customization.Enums;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization.Chart
{
	/// <summary>
	/// Common class for <see cref="LayoutOptions.Background"/>.<br/>
	/// Types that extend this class: <see cref="SolidColor"/>, <see cref="VerticalGradientColor"/>.<br/>
	/// <see href="https://tradingview.github.io/lightweight-charts/docs/api#background"/>
	/// </summary>
	[JsonPolymorphic(TypeDiscriminatorPropertyName = "type")]
	[JsonDerivedType(typeof(SolidColor), typeDiscriminator: "solid")]
	[JsonDerivedType(typeof(VerticalGradientColor), typeDiscriminator: "gradient")]
	public abstract class Background
	{
		/// <summary>
		/// Type of color.
		/// </summary>
		[JsonPropertyName("type")]
		public abstract ColorType Type { get; }
	}	
}
