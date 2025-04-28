using Microsoft.AspNetCore.Mvc;
using MiniDocs.API.Services;
using MiniDocs.Shared.Dtos;

namespace MiniDocs.API.Controllers
{
    [ApiController]
    [Route("api/documents/{documentId}/comments")]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;

        public CommentsController(CommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        public async Task<IActionResult> AddComment(Guid documentId, [FromBody] AddCommentRequest request)
        {
            var comment = await _commentService.AddCommentAsync(documentId, request);

            return Ok(comment);
        }

        [HttpGet]
        public async Task<IActionResult> GetComments(Guid documentId)
        {
            var comments = await _commentService.GetCommentsForDocumentAsync(documentId);

            return Ok(comments);
        }
    }
}
