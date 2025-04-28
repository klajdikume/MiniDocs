using Microsoft.EntityFrameworkCore;
using MiniDocs.Domain.Model;
using MiniDocs.Infrastructure.Database;
using MiniDocs.Shared.Dtos;

namespace MiniDocs.API.Services
{
    public class CommentService
    {
        private readonly AppDbContext _dbContext;
        public CommentService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Comment> AddCommentAsync(Guid documentId, AddCommentRequest commentRequest)
        {
            var comment = new Comment
            {
                DocumentId = documentId,
                AuthorId = commentRequest.AuthorId,
                Text = commentRequest.Text,
                StartOffset = commentRequest.StartOffset,
                EndOffset = commentRequest.EndOffset
            };

            _dbContext.Comments.Add(comment);
            await _dbContext.SaveChangesAsync();

            return comment;
        }

        public async Task<List<Comment>> GetCommentsForDocumentAsync(Guid documentId)
        {
            return await _dbContext.Comments
                .Where(c => c.DocumentId == documentId)
                .OrderBy(c => c.CreatedAt)
                .ToListAsync();
        }
    }
}
