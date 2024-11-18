using KristofferStrube.Blazor.DOM;

namespace KristofferStrube.Blazor.Window.Extensions;

/// <summary>
/// Extension methods that hold the implementations for <see cref="IGlobalEventHandlers"/> so that any that wish to implement the interface can use the same implementations.
/// </summary>
public static class IGlobalEventHandlersExtensions
{
    /// <inheritdoc cref="IGlobalEventHandlers.AddOnErrorEventListenerAsync(EventListener{ErrorEvent}, AddEventListenerOptions?)"/>
    public static async Task AddOnErrorEventListenerAsync<T>(this T globalEventHandler, EventListener<ErrorEvent> callback, AddEventListenerOptions? options = null) where T : EventTarget, IGlobalEventHandlers
    {
        await globalEventHandler.AddEventListenerAsync("error", callback, options);
    }

    /// <inheritdoc cref="IGlobalEventHandlers.AddOnErrorEventListenerAsync(EventListener{Event}, AddEventListenerOptions?)"/>
    public static async Task AddOnErrorEventListenerAsync<T>(this T globalEventHandler, EventListener<Event> callback, AddEventListenerOptions? options = null) where T : EventTarget, IGlobalEventHandlers
    {
        await globalEventHandler.AddEventListenerAsync("error", callback, options);
    }

    /// <inheritdoc cref="IGlobalEventHandlers.RemoveOnErrorEventListenerAsync(EventListener{ErrorEvent}, EventListenerOptions?)"/>
    public static async Task RemoveOnErrorEventListenerAsync<T>(this T globalEventHandler, EventListener<ErrorEvent> callback, EventListenerOptions? options = null) where T : EventTarget, IGlobalEventHandlers
    {
        await globalEventHandler.RemoveEventListenerAsync("error", callback, options);
    }

    /// <inheritdoc cref="IGlobalEventHandlers.RemoveOnErrorEventListenerAsync(EventListener{Event}, EventListenerOptions?)"/>
    public static async Task RemoveOnErrorEventListenerAsync<T>(this T globalEventHandler, EventListener<Event> callback, EventListenerOptions? options = null) where T : EventTarget, IGlobalEventHandlers
    {
        await globalEventHandler.RemoveEventListenerAsync("error", callback, options);
    }

    /// <inheritdoc cref="IGlobalEventHandlers.AddOnResizeEventListenerAsync(EventListener{Event}, AddEventListenerOptions?)"/>
    public static async Task AddOnResizeEventListenerAsync<T>(this T globalEventHandler, EventListener<Event> callback, AddEventListenerOptions? options = null) where T : EventTarget, IGlobalEventHandlers
    {
        await globalEventHandler.AddEventListenerAsync("resize", callback, options);
    }

    /// <inheritdoc cref="IGlobalEventHandlers.RemoveOnResizeEventListenerAsync(EventListener{Event}, EventListenerOptions?)"/>
    public static async Task RemoveOnResizeEventListenerAsync<T>(this T globalEventHandler, EventListener<Event> callback, EventListenerOptions? options = null) where T : EventTarget, IGlobalEventHandlers
    {
        await globalEventHandler.RemoveEventListenerAsync("resize", callback, options);
    }

    /// <inheritdoc cref="IGlobalEventHandlers.AddOnScrollEventListenerAsync(EventListener{Event}, AddEventListenerOptions?)"/>
    public static async Task AddOnScrollEventListenerAsync<T>(this T globalEventHandler, EventListener<Event> callback, AddEventListenerOptions? options = null) where T : EventTarget, IGlobalEventHandlers
    {
        await globalEventHandler.AddEventListenerAsync("scroll", callback, options);
    }

    /// <inheritdoc cref="IGlobalEventHandlers.RemoveOnScrollEventListenerAsync(EventListener{Event}, EventListenerOptions?)"/>
    public static async Task RemoveOnScrollEventListenerAsync<T>(this T globalEventHandler, EventListener<Event> callback, EventListenerOptions? options = null) where T : EventTarget, IGlobalEventHandlers
    {
        await globalEventHandler.RemoveEventListenerAsync("scroll", callback, options);
    }

    /// <inheritdoc cref="IGlobalEventHandlers.AddOnScrollEndEventListenerAsync(EventListener{Event}, AddEventListenerOptions?)"/>
    public static async Task AddOnScrollEndEventListenerAsync<T>(this T globalEventHandler, EventListener<Event> callback, AddEventListenerOptions? options = null) where T : EventTarget, IGlobalEventHandlers
    {
        await globalEventHandler.AddEventListenerAsync("scrollend", callback, options);
    }

    /// <inheritdoc cref="IGlobalEventHandlers.RemoveOnScrollEndEventListenerAsync(EventListener{Event}, EventListenerOptions?)"/>
    public static async Task RemoveOnScrollEndEventListenerAsync<T>(this T globalEventHandler, EventListener<Event> callback, EventListenerOptions? options = null) where T : EventTarget, IGlobalEventHandlers
    {
        await globalEventHandler.RemoveEventListenerAsync("scrollend", callback, options);
    }
}
