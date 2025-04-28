using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDocs.Domain.Model
{
    public class ArchivedDocumentVersion
    {
        public int Id { get; set; }
        public Guid DocumentId { get; set; }
        public Guid SectionId { get; set; }
        public string CompressedData { get; set; } = string.Empty;
        public DateTime OriginalCreatedAt { get; set; }
        public DateTime ArchivedAt { get; set; }
    }
}
