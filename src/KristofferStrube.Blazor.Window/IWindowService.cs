
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
    /// You should dispose of this <see cref="Window"/> once you are done using it to free the reference.
    /// </remarks>
    Task<Window> GetWindowAsync();
}