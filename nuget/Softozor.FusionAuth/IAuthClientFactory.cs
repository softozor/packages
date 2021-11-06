namespace Softozor.FusionAuth;

using io.fusionauth;

public interface IAuthClientFactory
{
    IFusionAuthAsyncClient Create();
}