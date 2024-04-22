using DocPortal.Domain.Entities;

using FluentValidation;

namespace DocPortal.Infrastructure.Validators;

internal class OrganizationValidator : AbstractValidator<Organization>
{
  public OrganizationValidator()
  {
    RuleFor(org => org.Title).NotEmpty().MaximumLength(1023);

    RuleFor(org => org.PhysicalIdentity).NotEmpty().MaximumLength(255);
  }
}
