using Microsoft.Extensions.DependencyInjection;
using Unwired.Commons.Criptography.Implementations;
using Unwired.Commons.Criptography.Interfaces;

namespace Unwired.Commons.Configurations;

public static class CommonsConfiguration
{
    public static void AddUnwiredCommons(this IServiceCollection services)
    {
        services.AddSingleton<ICriptographyMethods, CriptographyMethods>();
    }
}