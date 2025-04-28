namespace MiniDocs.Shared.Dtos
{
    public record CreateDocumentRequest(string Title, Guid OwnerUserId);
    public record UpdateDocumentRequest(Guid Id, string Title, string Content);
    public record DocumentResponse(Guid Id, string Title, string Content, DateTime CreatedAt, DateTime LastModifiedAt);
}
