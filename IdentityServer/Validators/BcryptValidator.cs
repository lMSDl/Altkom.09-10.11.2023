using IdentityServer4.Models;
using IdentityServer4.Validation;

internal class BcryptValidator : ISecretValidator
{
    public Task<SecretValidationResult> ValidateAsync(IEnumerable<Secret> secrets, ParsedSecret parsedSecret)
    {
        var result = new SecretValidationResult();
        var secret = secrets.SingleOrDefault(x => x.Type == "bcrypt");

        if (secret is not null && BCrypt.Net.BCrypt.Verify(parsedSecret.Credential.ToString(), secret.Value))
        {
            result.Success = true;
        }

        return Task.FromResult(result);
    }
}