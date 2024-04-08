using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Window;

/// <inheritdoc cref="IWindowService"/>
public class WindowService : IWindowService
{
    private readonly IJSRuntime jSRuntime;

    /// <summary>
    /// Constructs a new instance of the service.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    public WindowService(IJSRuntime jSRuntime)
    {
        this.jSRuntime = jSRuntime;
    }

    /// <inheritdoc/>
    public async Task<Window> GetWindowAsync()
    {
        IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("window.valueOf");
        return await Window.CreateAsync(jSRuntime, jSInstance, new() { DisposesJSReference = true });
    }
}
