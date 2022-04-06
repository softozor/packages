namespace Softozor.HasuraHandling.ConfigurationManagement;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddConfigurationManagement(this IServiceCollection services)
    {
        return services.AddSingleton<ISecretReader, SecretReader>();
    }
}