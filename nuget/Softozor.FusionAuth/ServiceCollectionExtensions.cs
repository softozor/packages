namespace Softozor.FusionAuth;

using Microsoft.Extensions.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddFusionAuthClient(this IServiceCollection services)
    {
        return services.AddTransient<IAuthClientFactory, AuthClientFactory>()
            .AddTransient(
                srvProvider =>
                {
                    var factory = srvProvider.GetService<IAuthClientFactory>();
                    return factory!.Create();
                });
    }
}