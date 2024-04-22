using DocPortal.Domain.Entities;

using FluentValidation;

namespace DocPortal.Infrastructure.Validators;

internal class UserValidator : AbstractValidator<User>
{
  public UserValidator()
  {
    RuleFor(user => user.Role).NotEmpty().MaximumLength(31);

    RuleFor(user => user.PhysicalIdentity).NotEmpty().Length(14);

    RuleFor(user => user.FirstName).NotEmpty().MaximumLength(127);

    RuleFor(user => user.LastName).NotEmpty().MaximumLength(127);

    RuleFor(user => user.JobPosition).NotEmpty().MaximumLength(127);

    RuleFor(user => user.Login).MaximumLength(127);

    RuleFor(user => user.PasswordHash).MaximumLength(63);
  }
}
