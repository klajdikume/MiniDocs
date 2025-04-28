using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniDocs.Domain.Model;
using MiniDocs.Infrastructure.Database;
using MiniDocs.Shared.Dtos;

namespace MiniDocs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentSharingController : ControllerBase
    {
        private readonly AppDbContext _dbContext;

        public DocumentSharingController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost("{documentId}/make-public")]
        public async Task<IActionResult> MakePublic(Guid documentId)
        {
            var document = await _dbContext.Documents.FindAsync(documentId);
            if (document == null) return NotFound();

            document.Visibility = Domain.DocumentVisibility.Public;
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpPost("{documentId}/invite")]
        public async Task<IActionResult> InviteUser(Guid documentId, [FromBody] InviteUserRequest request)
        {
            var document = await _dbContext.Documents.FindAsync(documentId);
            if (document == null) return NotFound();

            var permission = new DocumentUserPermission
            {
                DocumentId = documentId,
                UserId = request.UserId,
                CanEdit = request.CanEdit
            };

            _dbContext.DocumentUserPermissions.Add(permission);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        [HttpGet("/public-documents/{documentId}")]
        public async Task<IActionResult> GetPublicDocument(Guid documentId)
        {
            var document = await _dbContext.Documents
                .FirstOrDefaultAsync(d => d.Id == documentId && d.Visibility == Domain.DocumentVisibility.Public);
            
            if (document == null) return NotFound();

            return Ok(new { document.Title, document.Content });
        }
    }
}
