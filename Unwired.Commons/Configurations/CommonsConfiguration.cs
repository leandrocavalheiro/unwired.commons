using Microsoft.Extensions.DependencyInjection;
using Unwired.Commons.Criptography.Implementations;
using Unwired.Commons.Criptography.Interfaces;

namespace Unwired.Commons.Configurations;

public static class CommonsConfiguration
{
    /// <summary>
    /// Service register
    /// </summary>
    /// <param name="services">Service Collection</param>
    public static void AddUnwiredCommons(this IServiceCollection services)
    {
        services.AddSingleton<ICriptographyMethods, CriptographyMethods>();
    }
}