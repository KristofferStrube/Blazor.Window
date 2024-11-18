using KristofferStrube.Blazor.DOM;

namespace KristofferStrube.Blazor.Window.Extensions;

/// <summary>
/// Extension methods that hold the implementations for <see cref="IWindowEventHandlers"/> so that any that wish to implement the interface can use the same implementations.
/// </summary>
public static class IWindowEventHandlersExtensions
{
    /// <inheritdoc cref="IWindowEventHandlers.AddOnMessageEventListenerAsync(EventListener{MessageEvent}, AddEventListenerOptions?)"/>
    public static async Task AddOnMessageEventListenerAsync<T>(this T windowEventHandler, EventListener<MessageEvent> callback, AddEventListenerOptions? options = null) where T : EventTarget, IWindowEventHandlers
    {
        await windowEventHandler.AddEventListenerAsync("message", callback, options);
    }

    /// <inheritdoc cref="IWindowEventHandlers.RemoveOnMessageEventListenerAsync(EventListener{MessageEvent}, EventListenerOptions?)"/>
    public static async Task RemoveOnMessageEventListenerAsync<T>(this T windowEventHandler, EventListener<MessageEvent> callback, EventListenerOptions? options = null) where T : EventTarget, IWindowEventHandlers
    {
        await windowEventHandler.RemoveEventListenerAsync("message", callback, options);
    }
}
