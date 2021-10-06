namespace Softozor.HasuraHandling.ConfigurationManagement;

using System.IO;
using Softozor.HasuraHandling.Interfaces;

public class SecretReader : ISecretReader
{
    public string GetSecret(string secretName)
    {
        // TODO: what will happen if the file doesn't exist?
        // TODO: use System.IO.Abstractions
        return File.ReadAllText($"/var/openfaas/secrets/{secretName}").Trim();
    }
}