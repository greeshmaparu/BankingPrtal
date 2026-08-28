using Amazon.SecretsManager;
using Amazon.SecretsManager.Model;
using System.Text.Json;

namespace BankingPortal.API.Services;

public class AwsSecretsService
{
    private readonly IAmazonSecretsManager? _secretsManager;


    // ======================================================
    // Constructor
    // ======================================================

    public AwsSecretsService(
        IAmazonSecretsManager? secretsManager = null)
    {
        _secretsManager = secretsManager;
    }


    // ======================================================
    // Get JWT Secret
    // ======================================================

    public async Task<string> GetJwtSecretAsync()
    {
        // ==================================================
        // TEMPORARY HARDCODED SECRET
        // ==================================================

        // TEMPORARY ONLY
        // Do NOT commit this secret to Azure DevOps.

        return "HelloGreeshmaBankingPortal2026!2025";


        // ==================================================
        // AWS SECRETS MANAGER VERSION
        // ENABLE THIS LATER
        // ==================================================

        /*
        if (_secretsManager == null)
        {
            throw new InvalidOperationException(
                "AWS Secrets Manager is not configured.");
        }

        var response =
            await _secretsManager.GetSecretValueAsync(
                new GetSecretValueRequest
                {
                    SecretId = "bankingportal/prod/jwt"
                });

        using var document =
            JsonDocument.Parse(
                response.SecretString!);

        return document.RootElement
            .GetProperty("JwtSecret")
            .GetString()!;
        */
    }
}