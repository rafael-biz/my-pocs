using System;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;

namespace KeyVaultDemo
{
    class Program
    {
        private static readonly string tenantId = "<replace_me>";
        private static readonly string clientId = "<replace_me>";
        private static readonly string clientSecret = "<replace_me>";
        private static readonly string vaultName = "<replace_me>";

        static void Main(string[] args)
        {
            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                 }
            };

            var credential = new ClientSecretCredential(tenantId, clientId, clientSecret);

            // Uncomment this line if you want to authenticate with a *managed identity* instead of *service principal*.
            // var credential = new DefaultAzureCredential();

            var client = new SecretClient(new Uri($"https://{vaultName}.vault.azure.net/"), credential, options);

            Console.WriteLine("Retrieving secret...");

            KeyVaultSecret secret = client.GetSecret("MyPassword");

            string secretValue = secret.Value;

            Console.WriteLine("The secret value is: " + secretValue);

            Console.ReadKey();
        }
    }
}
