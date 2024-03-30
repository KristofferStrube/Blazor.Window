
namespace KristofferStrube.Blazor.Window;

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