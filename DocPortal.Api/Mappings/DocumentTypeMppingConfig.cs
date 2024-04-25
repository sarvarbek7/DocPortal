using DocPortal.Contracts.Dtos;
using DocPortal.Domain.Entities;

using Mapster;

namespace DocPortal.Api.Mappings
{
  internal class DocumentTypeMppingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<DocumentType, DocumentTypeDto>();
      config.NewConfig<DocumentTypeDto, DocumentType>();
    }
  }
}
