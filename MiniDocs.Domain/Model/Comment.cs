using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDocs.Domain.Model
{
    public class Comment
    {
        public int Id { get; set; }

        public Guid DocumentId { get; set; }
        public Document Document { get; set; } = null!;

        public string AuthorId { get; set; } = null!; 
        public string Text { get; set; } = null!;

        public int? StartOffset { get; set; } // start character position
        public int? EndOffset { get; set; }   // end character position

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
