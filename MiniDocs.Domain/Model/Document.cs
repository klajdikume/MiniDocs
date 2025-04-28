using MiniDocs.Domain.Model;

namespace MiniDocs.Domain
{
    public class Document
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid OwnerUserId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime LastModifiedAt { get; set; } = DateTime.UtcNow;
        public DocumentVisibility Visibility { get; set; } = DocumentVisibility.Private;
        public List<DocumentUserPermission> Permissions { get; set; } = new();
        public List<Comment> Comments { get; set; } = new();
        public ICollection<DocumentVersion> Versions { get; set; } 
            = new List<DocumentVersion>();
    }

    public enum DocumentVisibility
    {
        Private,
        Public,
        Shared
    }
}
