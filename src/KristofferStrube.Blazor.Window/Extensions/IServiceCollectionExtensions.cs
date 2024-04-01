using Microsoft.Extensions.DependencyInjection;

namespace KristofferStrube.Blazor.Window;

/// <summary>
/// Contains extension methods for adding services to a <see cref="IServiceCollection"/>.
/// </summary>
public static class IServiceCollectionExtensions
{
    /// <summary>
    /// Adds a <see cref="IWindowService"/> to the service collection as a scoped service.
    /// </summary>
    /// <param name="serviceCollection">The service collection.</param>
    /// <returns>The same service collection.</returns>
    public static IServiceCollection AddWindowService(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddScoped<IWindowService, WindowService>();
    }
}