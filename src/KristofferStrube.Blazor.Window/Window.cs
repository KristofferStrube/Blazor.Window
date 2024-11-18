using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.Window.Extensions;
using KristofferStrube.Blazor.Window.Options;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Window;

/// <summary>
/// The global object for a browser window.
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/#the-window-object">See the API definition here</see>.</remarks>
[IJSWrapperConverter]
public class Window : EventTarget, IJSCreatable<Window>, IWindowEventHandlers, IGlobalEventHandlers
{
    /// <summary>
    /// A lazily loaded task that evaluates to a helper module instance from the Blazor.Window library.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> windowHelperTask;

    /// <inheritdoc/>
    public static new async Task<Window> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
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
    public async Task<Window> GetSelfAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        IJSObjectReference jSInstance = await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "self");
        return new(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Gets the name of this <see cref="Window"/>.
    /// </summary>
    /// <remarks>
    /// This has historically been used as a way to parse information from a parent/opener context, but has now been replace by the better alternative: <see cref="PostMessageAsync(object, WindowPostMessageOptions?)"/>.
    /// </remarks>
    public async Task<string> GetNameAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        return await helper.InvokeAsync<string>("getAttribute", JSReference, "name");
    }

    /// <summary>
    /// Sets the name of this <see cref="Window"/>.
    /// </summary>
    /// <remarks>
    /// This has historically been used as a way to parse information from a parent/opener context, but has now been replace by the better alternative: <see cref="PostMessageAsync(object, WindowPostMessageOptions?)"/>.
    /// </remarks>
    public async Task SetNameAsync(string value)
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        await helper.InvokeVoidAsync("setAttribute", JSReference, "name", value);
    }

    /// <summary>
    /// Closes the window.
    /// </summary>
    /// <remarks>
    /// This can only be used from the parent/opener context not from the window itself.
    /// </remarks>
    public async Task CloseAsync()
    {
        await JSReference.InvokeVoidAsync("close");
    }

    /// <summary>
    /// Gets a boolean that indicates whether the windows is closed.
    /// </summary>
    public async Task<bool> GetClosedAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        return await helper.InvokeAsync<bool>("getAttribute", JSReference, "closed");
    }

    /// <summary>
    /// Stops loading the document of this window.
    /// </summary>
    public async Task StopAsync()
    {
        await JSReference.InvokeVoidAsync("close");
    }

    /// <summary>
    /// Requests that the window gets focus.
    /// </summary>
    /// <remarks>
    /// Most browsers do now honor this function.
    /// </remarks>
    public async Task FocusAsync()
    {
        await JSReference.InvokeVoidAsync("focus");
    }

    /// <summary>
    /// Requests that the window loses focus.
    /// </summary>
    /// <remarks>
    /// Most browsers do now honor this function.
    /// </remarks>
    public async Task BlurAsync()
    {
        await JSReference.InvokeVoidAsync("blur");
    }

    /// <summary>
    /// Gets the opener navigable of this <see cref="Window"/>.
    /// </summary>
    public async Task<Window?> GetOpenerAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "opener");
        return jSInstance is null ? null : new(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Gets the parent navigable of this <see cref="Window"/>.
    /// </summary>
    public async Task<Window?> GetParentAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        IJSObjectReference? jSInstance = await helper.InvokeAsync<IJSObjectReference?>("getAttribute", JSReference, "parent");
        return jSInstance is null ? null : new(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Opens a window to show <paramref name="url"/>, and returns it. <paramref name="target"/> gives the name of the new window.
    /// If a window already exists with that name, it is reused.
    /// </summary>
    /// <param name="url">Defaults to <c>"about:blank"</c></param>
    /// <param name="target">Defaults to <c>"_blank"</c></param>
    /// <param name="features">Can contain a set of comma-separated tokens <c>noopener</c>, <c>noreferrer</c>, and <c>popup</c>.</param>
    /// <returns>The opened <see cref="Window"/> if it was successful.</returns>
    public async Task<Window?> OpenAsync(string url = "", string target = "_blank", string features = "")
    {
        IJSObjectReference jSInstance = await JSReference.InvokeAsync<IJSObjectReference>("open", url, target, features);
        return new(JSRuntime, jSInstance, new() { DisposesJSReference = true });
    }

    /// <summary>
    /// Displays a modal alert, and waits for the user to dismiss it.
    /// </summary>
    public async Task AlertAsync()
    {
        await JSReference.InvokeVoidAsync("alert");
    }

    /// <summary>
    /// Displays a modal alert with the given <paramref name="message"/>, and waits for the user to dismiss it.
    /// </summary>
    /// <param name="message">The message that will displayed in the dialog.</param>
    public async Task AlertAsync(string message)
    {
        await JSReference.InvokeVoidAsync("alert", message);
    }

    /// <summary>
    /// Displays a modal OK/Cancel prompt with the given message, waits for the user to dismiss it, and returns <see langword="true"/> if the user clicks OK and <see langword="false"/> if the user clicks Cancel.
    /// </summary>
    /// <param name="message">The message that will displayed in the dialog.</param>
    public async Task<bool> ConfirmAsync(string message = "")
    {
        return await JSReference.InvokeAsync<bool>("confirm", message);
    }

    /// <summary>
    /// Displays a modal text control prompt with the given <paramref name="message"/>, waits for the user to dismiss it, and returns the value that the user entered.
    /// If the user cancels the prompt, then returns <see langword="null"/> instead.
    /// If the <paramref name="defaultValue"/> is set, then the given value is used as a default.
    /// </summary>
    /// <param name="message">The message that will displayed in the dialog.</param>
    /// <param name="defaultValue">The value that will be used as the default value in the prompt input.</param>
    public async Task<string?> PromptAsync(string message = "", string defaultValue = "")
    {
        return await JSReference.InvokeAsync<string>("prompt", message, defaultValue);
    }

    /// <summary>
    /// Requests that the document opens a print dialog when it is loaded. If it is already loaded then it opens the print dialog or ignores the request.
    /// </summary>
    /// <remarks>
    /// Before printing, it will dispatch an event that you can subscribe to via <see cref="IWindowEventHandlers.AddOnBeforePrintEventListenerAsync(EventListener{Event}, AddEventListenerOptions?)"/>.<br />
    /// After printing, it will dispatch an event that you can subscribe to via <see cref="IWindowEventHandlers.AddOnAfterPrintEventListenerAsync(EventListener{Event}, AddEventListenerOptions?)"/>
    /// </remarks>
    public async Task PrintAsync()
    {
        await JSReference.InvokeVoidAsync("print");
    }

    /// <summary>
    /// Posts a message to the given window.
    /// </summary>
    /// <param name="message">Messages can be structured objects, e.g. nested JSON objects and arrays, can contain JavaScript values (strings, numbers, Date objects, etc.), and can contain certain data objects that are marked as <see cref="ITransferable"/> like <see cref="ArrayBuffer"/></param>
    /// <param name="options">The options used for posting the message. Defining the target origin and what objects should be transfered.</param>
    public async Task PostMessageAsync(object message, WindowPostMessageOptions? options = null)
    {
        await JSReference.InvokeVoidAsync("postMessage", message, options);
    }

    /// <inheritdoc/>
    public async Task AddOnMessageEventListenerAsync(EventListener<MessageEvent> callback, AddEventListenerOptions? options = null)
    {
        await this.AddOnMessageEventListenerAsync<Window>(callback, options);
    }

    /// <inheritdoc/>
    public async Task RemoveOnMessageEventListenerAsync(EventListener<MessageEvent> callback, EventListenerOptions? options = null)
    {
        await this.RemoveOnMessageEventListenerAsync<Window>(callback, options);
    }

    /// <inheritdoc/>
    public async Task AddOnErrorEventListenerAsync(EventListener<ErrorEvent> callback, AddEventListenerOptions? options = null)
    {
        await this.AddOnErrorEventListenerAsync<Window>(callback, options);
    }

    /// <inheritdoc/>
    public async Task AddOnErrorEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null)
    {
        await this.AddOnErrorEventListenerAsync<Window>(callback, options);
    }

    /// <inheritdoc/>
    public async Task RemoveOnErrorEventListenerAsync(EventListener<ErrorEvent> callback, EventListenerOptions? options = null)
    {
        await this.RemoveOnErrorEventListenerAsync<Window>(callback, options);
    }

    /// <inheritdoc/>
    public async Task RemoveOnErrorEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await this.RemoveOnErrorEventListenerAsync<Window>(callback, options);
    }

    /// <inheritdoc/>
    public async Task AddOnResizeEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null)
    {
        await this.AddOnResizeEventListenerAsync<Window>(callback, options);
    }

    /// <inheritdoc/>
    public async Task RemoveOnResizeEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await this.RemoveOnResizeEventListenerAsync<Window>(callback, options);
    }

    /// <inheritdoc/>
    public async Task AddOnScrollEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null)
    {
        await this.AddOnScrollEventListenerAsync<Window>(callback, options);
    }

    /// <inheritdoc/>
    public async Task RemoveOnScrollEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await this.RemoveOnScrollEventListenerAsync<Window>(callback, options);
    }

    /// <inheritdoc/>
    public async Task AddOnScrollEndEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null)
    {
        await this.AddOnScrollEndEventListenerAsync<Window>(callback, options);
    }

    /// <inheritdoc/>
    public async Task RemoveOnScrollEndEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null)
    {
        await this.RemoveOnScrollEndEventListenerAsync<Window>(callback, options);
    }
}
