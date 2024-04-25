using ErrorOr;

namespace DocPortal.Application.Errors
{
  public static partial class ApplicationError
  {
    public static class DocumentError
    {
      public static Error NotFound =>
        Error.NotFound("Document.NotFound", "Document is with given id is not found");
    }
  }
}
