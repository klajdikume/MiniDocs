using Microsoft.EntityFrameworkCore;
using MiniDocs.Domain;
using MiniDocs.Infrastructure.Database;

namespace MiniDocs.API.Services
{
    public interface IDocumentAuthorizationService
    {
        Task<bool> CanAccessDocumentAsync(Guid documentId, Guid userId);
    }

    public class DocumentAuthorizationService
    {
        private readonly AppDbContext _dbContext;

        public DocumentAuthorizationService(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<bool> CanAccessDocumentAsync(Guid documentId, Guid userId)
        {
            var document = await _dbContext.Documents
                .Include(d => d.Permissions)
                .FirstOrDefaultAsync(d => d.Id == documentId);

            if (document == null) return false;

            if (document.Visibility == DocumentVisibility.Public)
                return true;

            if (document.OwnerUserId == userId)
                return true;

            if (document.Visibility == DocumentVisibility.Shared)
            {
                return document.Permissions.Any(p => p.UserId == userId);
            }

            return true;
        }
    }
}
