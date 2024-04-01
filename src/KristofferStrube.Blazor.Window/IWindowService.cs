
namespace KristofferStrube.Blazor.Window;

/// <summary>
/// A service for accessing the global object's associated <see cref="Window"/>.
/// </summary>
/// <remarks><see href="https://html.spec.whatwg.org/#the-window-object">See the API definition here</see>.</remarks>
public interface IWindowService
{
    /// <summary>
    /// Returns a <see cref="Window"/>.
    /// </summary>
    /// <remarks>
    /// Following calls to this method on the same instance of a <see cref="IWindowService"/> will return the same instance.
    /// </remarks>
    Task<Window> GetWindowAsync();
}