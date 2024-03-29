using KristofferStrube.Blazor.DOM;

namespace KristofferStrube.Blazor.Window;

public interface IWindowEventHandlers
{
    /// <summary>
    /// Adds an <see cref="EventListener{TEvent}"/> for when an object receives a message.
    /// </summary>
    /// <param name="callback">Callback that will be invoked when the event is dispatched.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.AddEventListenerAsync{TEvent}(string, EventListener{TEvent}?, AddEventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task AddOnMessageEventListenerAsync(EventListener<Event> callback, AddEventListenerOptions? options = null);

    /// <summary>
    /// Removes the event listener from the event listener list if it has been parsed to <see cref="AddOnMessageEventListenerAsync"/> previously.
    /// </summary>
    /// <param name="callback">The callback <see cref="EventListener{TEvent}"/> that you want to stop listening to events.</param>
    /// <param name="options"><inheritdoc cref="EventTarget.RemoveEventListenerAsync{TEvent}(string, EventListener{TEvent}?, EventListenerOptions?)" path="/param[@name='options']"/></param>
    public Task RemoveOnMessageChangeEventListenerAsync(EventListener<Event> callback, EventListenerOptions? options = null);
}
