using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDocs.Domain.Model
{
    public class DocumentVersion
    {
        public int Id { get; set; }
        public Guid DocumentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Content { get; set; } = string.Empty;
        public DateTime CreatedAtUtc { get; set; } = DateTime.UtcNow;
        public string CreatedByUserId { get; set; } = string.Empty;
        public Guid SectionId { get; set; }
        public bool IsArchived { get; set; } = false;
        public DateTime? ArchivedOn { get; set; }
    }
}
