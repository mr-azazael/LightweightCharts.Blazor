using Microsoft.AspNetCore.Components;

namespace LightweightCharts.Blazor.Examples;

static class Configuration
{
	public static IComponentRenderMode RenderMode { get; } = Microsoft.AspNetCore.Components.Web.RenderMode.InteractiveServer;
}
