using DocPortal.Contracts.Dtos;
using DocPortal.Domain.Entities;

using Mapster;

namespace DocPortal.Api.Mappings
{
  internal class DocumentMappingConfig : IRegister
  {
    public void Register(TypeAdapterConfig config)
    {
      config.NewConfig<Document, DocumentDto>();

      config.NewConfig<DocumentDto, Document>()
        .Ignore(dest => dest.DocumentType)
        .Ignore(dest => dest.Organization);
    }
  }
}
