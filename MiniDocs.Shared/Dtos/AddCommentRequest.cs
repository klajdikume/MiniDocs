using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDocs.Shared.Dtos
{
    public class AddCommentRequest
    {
        public string AuthorId { get; set; } = null!;
        public string Text { get; set; } = null!;
        public int? StartOffset { get; set; }
        public int? EndOffset { get; set; }
    }
}
