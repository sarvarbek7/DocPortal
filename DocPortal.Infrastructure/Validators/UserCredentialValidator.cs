using DocPortal.Domain.Entities;

using FluentValidation;

namespace DocPortal.Infrastructure.Validators;

internal class UserCredentialValidator : AbstractValidator<UserCredential>
{
  public UserCredentialValidator()
  {
    RuleFor(user => user.Login).MaximumLength(127);

    RuleFor(user => user.Password).MaximumLength(63);
  }
}
