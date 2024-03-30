using Microsoft.Extensions.DependencyInjection;

namespace KristofferStrube.Blazor.Window;

public static class IServiceCollectionExtensions
{
    public static IServiceCollection AddWindowService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IWindowService, WindowService>();
    }
}