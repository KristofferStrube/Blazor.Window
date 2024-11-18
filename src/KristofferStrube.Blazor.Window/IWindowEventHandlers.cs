using KristofferStrube.Blazor.DOM;

namespace KristofferStrube.Blazor.Window;

/// <summary>
/// Event handlers that are a part of <see cref="Window"/>.
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/#windoweventhandlers">See the API definition here</see>.</remarks>
public interface IWindowEventHandlers
{
    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> that is invoked after the <see cref="Window"/> has printed.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task AddOnAfterPrintEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null);

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnAfterPrintEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task RemoveOnAfterPrintEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null);

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> that is invoked before the <see cref="Window"/> starts printing.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task AddOnBeforePrintEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null);

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnBeforePrintEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task RemoveOnBeforePrintEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null);

    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when an object receives a message.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task AddOnMessageEventListenerAsync(EventListener<MessageEvent> callback, AddEventListenerOptions? options = null);

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnMessageEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task RemoveOnMessageEventListenerAsync(EventListener<MessageEvent> callback, EventListenerOptions? options = null);
}
