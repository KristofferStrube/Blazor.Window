using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.Window.Extensions;
using KristofferStrube.Blazor.Window.Options;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Window;

public class Window : EventTarget, IJSCreatable<Window>, IWindowEventHandlers
{
    /// <summary>
    /// A lazily loaded task that evaluates to a helper module instance from the Blazor.Window library.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> windowHelperTask;

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
        windowHelperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// Gets this window.
    /// </summary>
    /// <returns></returns>
    public async Task<Window> GetWindowAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "window");
        return new(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Gets this window.
    /// </summary>
    /// <returns></returns>
    public async Task<Window> GetSelfAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "self");
        return new(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Gets the parent navigable of this <see cref="Window"/>.
    /// </summary>
    /// <returns></returns>
    public async Task<Window?> GetParentAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "parent");
        return jSInstance is null ? null : new(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Gets the opener navigable of this <see cref="Window"/>.
    /// </summary>
    /// <returns></returns>
    public async Task<Window?> GetOpenerAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "opener");
        return jSInstance is null ? null : new(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    public async Task<Window?> OpenAsync(string url = "", string target = "_blank", string features = "")
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("open", url, target, features);
        return new(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Posts a message to the given window.
    /// </summary>
    /// <param name="message">Messages can be structured objects, e.g. nested JSON objects and arrays, can contain JavaScript values (strings, numbers, Date objects, etc.), and can contain certain data objects that are marked as <see cref="ITransferable"/> like <see cref="ArrayBuffer"/></param>
    /// <param name="options">The options used for posting the message. Defining the target origin and what objects should be transfered.</param>
    public async Task PostMessage(object message, WindowPostMessageOptions? options = null)
    {
        await JSReference.InvokeVoidAsync("postMessage", message);
    }

    /// <inheritdoc/>
    public async Task AddOnMessageEventListenerAsync(EventListener<MessageEvent> callback, AddEventListenerOptions? options = null)
    {
        await AddEventListenerAsync("message", callback, options);
    }

    /// <inheritdoc/>
    public async Task RemoveOnMessageChangeEventListenerAsync(EventListener<MessageEvent> callback, EventListenerOptions? options = null)
    {
        await RemoveEventListenerAsync("message", callback, options);
    }
}
