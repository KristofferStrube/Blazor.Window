
namespace KristofferStrube.Blazor.Window;

public interface IWindowService
{
    Task<Window> GetMediaDevicesAsync();
}