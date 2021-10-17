namespace Softozor.GraphqlClient;

using Microsoft.Extensions.DependencyInjection;
using Softozor.GraphqlClient.Interfaces;

public static class GraphQLServiceCollectionExtensions
{
    public static IServiceCollection AddGraphQL(this IServiceCollection services)
    {
        return services.AddSingleton<IClientFactory, ClientFactory>().AddSingleton<IRequestBuilder, RequestBuilder>();
    }
}