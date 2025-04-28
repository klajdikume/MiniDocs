using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDocs.Domain.Model
{
    public class DocumentUserPermission
    {
        public int Id { get; set; }
        public Guid DocumentId { get; set; }
        public Guid UserId { get; set; } 
        public bool CanEdit { get; set; }
    }
}
