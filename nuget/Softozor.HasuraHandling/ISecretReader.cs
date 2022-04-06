namespace Softozor.HasuraHandling;

public interface ISecretReader
{
    string GetSecret(string secretName);
}