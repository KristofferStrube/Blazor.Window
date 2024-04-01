using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Window;

/// <inheritdoc cref="IWindowService"/>
public class WindowService : IAsyncDisposable, IWindowService
{
    private readonly Lazy<Task<Window>> windowTask;

    /// <summary>
    /// Constructs a new instance of the service.
    /// </summary>
    /// <param name="jSRuntime">An <see cref="IJSRuntime"/> instance.</param>
    public WindowService(IJSRuntime jSRuntime)
    {
        windowTask = new(async () =>
        {
            IJSObjectReference jSInstance = await jSRuntime.InvokeAsync<IJSObjectReference>("window.valueOf");
            return await Window.CreateAsync(jSRuntime, jSInstance);
        });
    }

    /// <inheritdoc/>
    public async Task<Window> GetWindowAsync()
    {
        return await windowTask.Value;
    }

    /// <summary>
    /// Disposes the service.
    /// </summary>
    public async ValueTask DisposeAsync()
    {
        if (windowTask.IsValueCreated)
        {
            Window window = await windowTask.Value;
            await window.JSReference.DisposeAsync();
            await (await windowTask.Value).DisposeAsync();
        }
        GC.SuppressFinalize(this);
    }
}
