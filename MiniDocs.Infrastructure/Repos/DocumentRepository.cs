using MiniDocs.Application.Repos;
using MiniDocs.Domain;
using MiniDocs.Infrastructure.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDocs.Infrastructure.Repos
{
    public class DocumentRepository : IDocumentRepository
    {
        private readonly AppDbContext _context;

        public DocumentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Document?> GetByIdAsync(Guid Id)
        {
            return await _context.Documents.FindAsync(Id);
        }

        public async Task AddAsync(Document document)
        {
            _context.Documents.Add(document);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(Document document)
        {
            _context.Documents.Update(document);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(Guid id)
        {
            var doc = await _context.Documents.FindAsync(id);
            if (doc != null) 
            {
                _context.Documents.Remove(doc);
                await _context.SaveChangesAsync();
            }
        }
    }
}
