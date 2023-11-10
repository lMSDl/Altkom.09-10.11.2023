using BCrypt.Net;
using IdentityModel;
using IdentityServer4.Test;
using IdentityServer4.Validation;
using System.Net.Sockets;
using System.Security.Claims;

namespace IdentityServer.Validators
{
    public class UserValidator : IResourceOwnerPasswordValidator
    {
        private readonly Dictionary<string, string> _users;

        public UserValidator()
        {
            _users = new Dictionary<string, string>
            {
                { "admin", "$2a$12$xX44w3DOfyZ6zMY9sbsidek01ydvqqlV/UHITSQIfaB8heWDipKy." } //admin:nimda
            };
        }

        public Task ValidateAsync(ResourceOwnerPasswordValidationContext context)
        {
            var username = context.UserName;
            var password = context.Password;


            if (_users.Any(x => x.Key == username && BCrypt.Net.BCrypt.Verify(password, x.Value)))
            {

                context.Result = new GrantValidationResult(
                    subject: username,
                    authenticationMethod: "custom");
            }

            return Task.CompletedTask;
        }
    }
}
