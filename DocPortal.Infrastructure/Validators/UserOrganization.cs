using FluentValidation;

namespace DocPortal.Infrastructure.Validators;

internal class UserOrganization : AbstractValidator<Domain.Entities.UserOrganization>
{
  public UserOrganization()
  {
  }
}
