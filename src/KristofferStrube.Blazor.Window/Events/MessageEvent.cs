using KristofferStrube.Blazor.DOM;
using KristofferStrube.Blazor.WebIDL;
using KristofferStrube.Blazor.Window.Extensions;
using Microsoft.JSInterop;

namespace KristofferStrube.Blazor.Window;

public class MessageEvent : Event, IJSCreatable<MessageEvent>
{
    /// <summary>
    /// A lazily loaded task that evaluates to a helper module instance from the Blazor.Window library.
    /// </summary>
    protected readonly Lazy<Task<IJSObjectReference>> windowHelperTask;

    /// <inheritdoc/>
    public static new async Task<MessageEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference)
    {
        return await CreateAsync(jSRuntime, jSReference, new());
    }

    /// <inheritdoc/>
    public static new Task<MessageEvent> CreateAsync(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options)
    {
        return Task.FromResult(new MessageEvent(jSRuntime, jSReference, options));
    }

    protected MessageEvent(IJSRuntime jSRuntime, IJSObjectReference jSReference, CreationOptions options) : base(jSRuntime, jSReference, options)
    {
        windowHelperTask = new(jSRuntime.GetHelperAsync);
    }

    /// <summary>
    /// Gets the message being sent as an <see cref="IJSObjectReference"/>.
    /// </summary>
    /// <returns></returns>
    public async Task<IJSObjectReference> GetDataAsync()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        return await helper.InvokeAsync<IJSObjectReference>("getAttribute", JSReference, "data");
    }

    /// <summary>
    /// Gets the message being sent as any type you want.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <returns></returns>
    public async Task<T> GetDataAsync<T>()
    {
        IJSObjectReference helper = await windowHelperTask.Value;
        return await helper.InvokeAsync<T>("getAttribute", JSReference, "data");
    }

    /// <summary>
    /// Gets the message being sent as a <see cref="ValueReference"/> that can be used to check the concrete underlying type.
    /// </summary>
    /// <returns></returns>
    public ValueReference Data => new(JSRuntime, JSReference, "data");
}
