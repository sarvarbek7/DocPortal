using DocPortal.Domain.Entities;

using FluentValidation;

namespace DocPortal.Infrastructure.Validators;

internal class DocumentValidator : AbstractValidator<Document>
{
  public DocumentValidator()
  {
    RuleFor(document => document.Title).NotEmpty().MaximumLength(1023);

    RuleFor(document => document.RegisteredNumber).NotEmpty().MaximumLength(63);

    RuleFor(document => document.RegisteredDate).NotNull()
      .LessThanOrEqualTo(DateOnly.FromDateTime(DateTime.Now))
      .WithMessage("Registered date can not be after now.");
  }
}
