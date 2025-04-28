using MiniDocs.Domain;
using MiniDocs.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDocs.Application.Repos
{
    public interface IDocumentRepository
    {
        Task<Document?> GetByIdAsync(Guid Id);
        Task AddAsync(Document document);
        Task UpdateAsync(Document document);
        Task DeleteAsync(Guid id);
    }
}
