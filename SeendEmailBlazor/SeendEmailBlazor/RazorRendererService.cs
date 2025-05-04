using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Endpoints;



namespace SeendEmailBlazor;

// RazorRendererService.cs
public class RazorRendererService
{
    private readonly RazorComponentRenderer _renderer;

    public RazorRendererService(RazorComponentRenderer renderer)
    {
        _renderer = renderer;
    }

    public async Task<string> RenderComponentAsync<TComponent>(ParameterView parameters)
        where TComponent : IComponent
    {
        var html = await _renderer.RenderComponentAsync<TComponent>(parameters);
        return html.ToString();
    }
}

