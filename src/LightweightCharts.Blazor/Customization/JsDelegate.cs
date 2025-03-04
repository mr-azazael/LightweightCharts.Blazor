using System;
using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace LightweightCharts.Blazor.Customization;

/// <summary>
/// descriptor for a global js function<br/>
/// value is used to set the proper function value in the js code
/// </summary>
public class JsDelegate
{
	/// <summary>
	/// public constructor
	/// </summary>
	/// <param name="delegateName"></param>
	[SetsRequiredMembers]
	public JsDelegate(string delegateName)
	{
		ArgumentException.ThrowIfNullOrEmpty(delegateName);
		DelegateName = delegateName;
	}

	/// <summary>
	/// discriminator property to recognize a function name in js code
	/// </summary>
	[JsonPropertyName("objectType")]
	public string ObjectType { get; } = nameof(JsDelegate);

	/// <summary>
	/// js function name (must be accesible in the window scope)
	/// </summary>
	[JsonPropertyName("delegateName")]
	public required string DelegateName { get; init; }
}
