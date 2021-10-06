namespace Softozor.HasuraHandling.Interfaces;

public interface ISecretReader
{
    string GetSecret(string secretName);
}