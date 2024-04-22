using DocPortal.Domain.Entities;

using FluentValidation;

namespace DocPortal.Infrastructure.Validators;

internal class DocumentTypeValidator : AbstractValidator<DocumentType>
{
  public DocumentTypeValidator()
  {
    RuleFor(docType => docType.Title).NotEmpty().MaximumLength(1023);
  }
}
