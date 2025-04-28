using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MiniDocs.Application.Documents.Commands;
using MiniDocs.Domain.Model;
using MiniDocs.Infrastructure.Database;
using MiniDocs.Shared.Dtos;

namespace MiniDocs.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DocumentsController: ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly AppDbContext _dbContext;

        public DocumentsController(IMediator mediator, AppDbContext context)
        {
            _mediator = mediator;
            _dbContext = context;
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateDocumentRequest request)
        {
            var id = await _mediator.Send(new CreateDocumentCommand(request.Title, request.OwnerUserId));

            return Ok(id);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DocumentResponse>> Get(Guid id)
        {
            var doc = await _mediator.Send(new GetDocumentByIdQuery(id));
            if (doc == null) return NotFound();
            return Ok(doc);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody] UpdateDocumentRequest request)
        {
            await _mediator.Send(new UpdateDocumentCommand(request.Id, request.Title, request.Content));
            return NoContent();
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDocument(Guid id, [FromBody] UpdateDocumentRequest request)
        {
            var document = await _dbContext.Documents.FindAsync(id);
            if (document == null) return NotFound();

            var version = new DocumentVersion
            {
                DocumentId = document.Id,
                Title = document.Title,
                Content = document.Content,
                CreatedAtUtc = DateTime.UtcNow
            };

            _dbContext.DocumentVersions.Add(version);

            document.Title = request.Title;
            document.Content = request.Content;

            await _dbContext.SaveChangesAsync();

            return Ok(document);
        }

        [HttpGet("/{id}/versions")]
        public async Task<IActionResult> GetVersions(Guid id)
        {
            var versions = await _dbContext.DocumentVersions
                .Where(v => v.DocumentId == id)
                .OrderByDescending(v => v.CreatedAtUtc)
                .ToListAsync();

            return Ok(versions.Select(v => new {
                v.Id,
                v.Title,
                v.CreatedAtUtc,
                v.CreatedByUserId
            }));
        }
      
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _mediator.Send(new DeleteDocumentCommand(id));
            return NoContent();
        }

        [HttpGet("{id}/versions")]
        public async Task<IActionResult> GetDocumentVersions(Guid id)
        {
            var document = await _dbContext.Documents
                .Include(d => d.Versions)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (document == null)
                return NotFound();

            var versions = document.Versions
                .OrderByDescending(v => v.CreatedAtUtc)
                .Select(v => new
                {
                    v.Id,
                    v.CreatedAtUtc
                })
                .ToList();

            return Ok(versions);
        }

        [HttpPost("restore-version")]
        public async Task<IActionResult> RestoreVersion(Guid id, int versionId)
        {
            var document = await _dbContext.Documents
                .Include(d => d.Versions)
                .FirstOrDefaultAsync(d => d.Id == id);

            if (document == null)
                return NotFound();

            var version = document.Versions.FirstOrDefault(v => v.Id == versionId);

            if (version == null)
                return NotFound();

            document.Content = version.Content;
            document.LastModifiedAt = DateTime.UtcNow;

            await _dbContext.SaveChangesAsync();

            return Ok(new { Message = "Document restored successfully!" });
        }
    }
}
