using KristofferStrube.Blazor.DOM;

namespace KristofferStrube.Blazor.Window;

/// <summary>
/// Event handlers that are a part of <see cref="Window"/>, Document and all HTMLElements.
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/#globaleventhandlers">See the API definition here</see>.</remarks>
public interface IGlobalEventHandlers
{
    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when the viewport is resized.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task AddOnResizeEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null);

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnResizeEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task RemoveOnResizeEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null);

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when the viewport is scrolled.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task AddOnScrollEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null);

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnScrollEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task RemoveOnScrollEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null);

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when the viewport has been scrolled, the scroll sequence has ended, and any scroll offset changes have been applied.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task AddOnScrollEndEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null);

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnScrollEndEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task RemoveOnScrollEndEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null);
}
