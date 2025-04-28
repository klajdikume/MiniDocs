using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDocs.Shared.Dtos
{
    public class InviteUserRequest
    {
        public Guid UserId { get; set; }
        public bool CanEdit { get; set; }
    }
}
