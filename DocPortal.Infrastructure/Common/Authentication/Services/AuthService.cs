using DocPortal.Application.Common.Authentication;
using DocPortal.Application.Common.Authentication.Services;
using DocPortal.Application.Services;
using DocPortal.Domain.Entities;

using ErrorOr;

using static DocPortal.Application.Errors.ApplicationError;

namespace DocPortal.Infrastructure.Common.Authentication.Services;

internal class AuthService(IUserService userService,
                           IUserOrganizationService userOrganizationService,
                           IUserCredentialService userCredentialService,
                           IHashingService hashingService,
                           ITokenGeneratorService tokenGeneratorService) : IAuthService
{
  public async ValueTask<ErrorOr<AccessToken>> LoginAsync(LoginDetails details, CancellationToken cancellationToken = default)
  {
    try
    {
      var orErrorUserCredential =
        await userCredentialService.RetrieveUserCredentialByLoginAsync(details.Login, cancellationToken);

      var userCredential = orErrorUserCredential.Value;

      if ((!orErrorUserCredential.IsError && orErrorUserCredential.FirstError == UserError.NotFound) || userCredential is null)
      {
        return AuthenticationError.InvalidCredentials;
      }

      if (!hashingService.ValidateHash(details.Password, userCredential.Password))
      {
        return AuthenticationError.InvalidCredentials;
      }

      var user = await userService.RetrieveByIdAsync(userCredential.Id, cancellationToken: cancellationToken);

      if (user.IsError)
      {
        return user.Errors;
      }

      return tokenGeneratorService.GenerateAccessToken(user.Value!);
    }
    catch (Exception ex)
    {
      return Error.Unexpected(description: ex.Message);
    }
  }

  public async ValueTask<ErrorOr<RegisterDetails>> RegisterAsync(RegisterDetails details, CancellationToken cancellationToken = default)
  {
    try
    {
      var orErrorUserCredential =
       await userCredentialService.RetrieveUserCredentialByLoginAsync(details.Login, cancellationToken);

      if (!orErrorUserCredential.IsError)
      {
        return UserError.AlreadyExistsUserWithLogin;
      }

      var passwordHash = hashingService.HashText(details.Password);

      details.User.UserCredential = new UserCredential()
      {
        Login = details.Login,
        Password = passwordHash
      };

      var addedUser = await userService.AddEntityAsync(details.User, false, cancellationToken);

      if (addedUser.IsError)
      {
        return addedUser.Errors;
      }

      ICollection<UserOrganization>? userOrganizations = details.UserOrganizations;

      if (details.UserOrganizations is not null)
      {
        try
        {
          userOrganizations =
            await userOrganizationService.AddMultipleUserOrganizationsAsync(
            details.UserOrganizations!, false, cancellationToken) as ICollection<UserOrganization>;
        }
        catch
        {
          userOrganizations = null;
        }
      }

      var registerDetails =
        new RegisterDetails(User: addedUser.Value,
                            Login: details.Login,
                            Password: passwordHash,
                            UserOrganizations: userOrganizations);

      await userOrganizationService.SaveChangesAsync(cancellationToken);

      return registerDetails;
    }
    catch (Exception ex)
    {
      return Error.Unexpected(description: ex.Message);
    }
  }

  public async ValueTask<ErrorOr<UpdateCredentialDetails>> UpdateUserCredentialAsync(UpdateCredentialDetails details, CancellationToken cancellationToken = default)
  {
    try
    {
      ErrorOr<UserCredential?> errorOrUserCredential =
        await userCredentialService.RetrieveByIdAsync(details.UserId, false, cancellationToken);

      if (errorOrUserCredential.IsError &&
        errorOrUserCredential.FirstError.Type is not ErrorType.NotFound)
      {
        return Error.Unexpected();
      }

      UserCredential? storedUserCredential = errorOrUserCredential.Value;

      var password = hashingService.HashText(details.Password);

      var userCredential = new UserCredential()
      {
        Id = details.UserId,
        Login = details.Login,
        Password = password,
      };

      if (storedUserCredential is null)
      {
        ErrorOr<UserCredential> errorOrUserCreatedCredential =
          await userCredentialService.AddEntityAsync(userCredential, true, cancellationToken);

        if (errorOrUserCreatedCredential.IsError)
        {
          return errorOrUserCreatedCredential.Errors;
        }

        UserCredential? value = errorOrUserCreatedCredential.Value;

        return new UpdateCredentialDetails(value.Id, value.Login, value.Password);
      }
      else
      {
        storedUserCredential.Login = userCredential.Login;
        storedUserCredential.Password = userCredential.Password;

        await userCredentialService.SaveChangesAsync(cancellationToken);

        return new UpdateCredentialDetails(
          storedUserCredential.Id,
          storedUserCredential.Login,
          storedUserCredential.Password);
      }
    }
    catch (Exception ex)
    {
      return Error.Unexpected();
    }
  }

  public async ValueTask<ErrorOr<bool>> DeleterUserCredentialAsync(int id, CancellationToken cancellationToken = default)
  {
    try
    {
      var errorOrUserCredential =
        await userCredentialService.RemoveByIdAsync(id, cancellationToken: cancellationToken);

      return errorOrUserCredential.IsError ?
        errorOrUserCredential.Errors :
        true;

    }
    catch (Exception ex)
    {
      return Error.Unexpected();
    }
  }

}
