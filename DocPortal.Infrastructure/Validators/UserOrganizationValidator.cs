using FluentValidation;

namespace DocPortal.Infrastructure.Validators;

internal class UserOrganizationValidator : AbstractValidator<Domain.Entities.UserOrganization>
{
  public UserOrganizationValidator()
  {
  }
}
