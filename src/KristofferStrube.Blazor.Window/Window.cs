using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebIDL;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Window;

public class Window : EventTarget, IJSCreatable<Window>, IWindowEventHandlers
{
    /// <inheritdoc/>
    public static async Task<Window> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<Window> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new Window(jSRuntime, jSReference, options));
    }

    /// <inheritdoc/>
    public Window(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
    }

    /// <inheritdoc/>
    public async Task AddOnMessageEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null)
    {
        await AddEventListenerAsync("message", callback, options);
    }

    /// <inheritdoc/>
    public async Task RemoveOnMessageChangeEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("message", callback, options);
    }
}
