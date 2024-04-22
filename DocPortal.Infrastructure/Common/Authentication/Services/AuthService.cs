using DocPortal.Application.Common.Authentication;
using DocPortal.Application.Common.Authentication.Services;
using DocPortal.Application.Services;
using DocPortal.Domain.Entities;

using ErrorOr;

using static DocPortal.Application.Errors.ApplicationError;

namespace DocPortal.Infrastructure.Common.Authentication.Services;

internal class AuthService(IUserService userService,
                           IUserOrganizationService userOrganizationService,
                           IHashingService hashingService,
                           ITokenGeneratorService tokenGeneratorService) : IAuthService
{
  public async ValueTask<ErrorOr<AccessToken>> LoginAsync(LoginDetails details)
  {
    try
    {
      var foundUserOrError =
        await userService.RetrieveUserByLoginAsync(details.Login);

      if (!foundUserOrError.IsError && foundUserOrError.Value is null)
      {
        return AuthenticationError.InvalidCredentials;
      }

      var user = foundUserOrError.Value;

      if (!hashingService.ValidateHash(details.Password, user.PasswordHash))
      {
        return AuthenticationError.InvalidCredentials;
      }

      return tokenGeneratorService.GenerateAccessToken(user);
    }
    catch (Exception ex)
    {
      return Error.Unexpected(description: ex.Message);
    }
  }

  public async ValueTask<ErrorOr<RegisterDetails>> RegisterAsync(RegisterDetails details)
  {
    try
    {
      var foundUser =
        await userService.RetrieveUserByLoginAsync(details.User?.Login);

      if (!foundUser.IsError)
      {
        return UserError.AlreadyExistsUserWithLogin;
      }

      var passwordHash = hashingService.HashText(details.Password);

      details.User.PasswordHash = passwordHash;

      var addedUser = await userService.AddEntityAsync(details.User, saveChanges: false);

      if (addedUser.IsError)
      {
        return addedUser.Errors;
      }

      ICollection<UserOrganization>? userOrganizations = null;

      if (details.UserOrganizations is not null)
      {
        userOrganizations =
          await userOrganizationService.AddMultipleUserOrganizations(
            details.UserOrganizations!) as ICollection<UserOrganization>;
      }
      else
      {
        await userService.SaveChanges();
      }

      var registerDetails =
        new RegisterDetails(User: addedUser.Value, UserOrganizations: userOrganizations);

      return registerDetails;
    }
    catch (Exception ex)
    {
      return Error.Unexpected(description: ex.Message);
    }
  }
}
