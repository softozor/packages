namespace Softozor.FusionAuth;

using System;
using io.fusionauth;
using Microsoft.AspNetCore.Http;
using Softozor.HasuraHandling.Exceptions;
using Softozor.HasuraHandling.Interfaces;

public class AuthClientFactory : IAuthClientFactory
{
    private readonly string apiKey;

    private readonly string host;

    public AuthClientFactory(ISecretReader secretReader)
    {
        this.apiKey = secretReader.GetSecret(FaasKeys.AuthSecret);
        this.host = Environment.GetEnvironmentVariable("AUTH_URL") ?? throw new HasuraFunctionException(
            "AUTH_URL environment variable not set",
            StatusCodes.Status500InternalServerError);
    }

    public IFusionAuthAsyncClient Create()
    {
        return new FusionAuthClient(this.apiKey, this.host);
    }
}