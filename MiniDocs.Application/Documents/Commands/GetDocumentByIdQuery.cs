using MediatR;
using MiniDocs.Application.Repos;
using MiniDocs.Shared.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDocs.Application.Documents.Commands
{
    public record GetDocumentByIdQuery(Guid Id): IRequest<DocumentResponse?>;
    public class GetDocumentByIdQueryHandler: IRequestHandler<GetDocumentByIdQuery, DocumentResponse?>
    {
        private readonly IDocumentRepository _repository;
        public GetDocumentByIdQueryHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<DocumentResponse?> Handle(GetDocumentByIdQuery request, CancellationToken cancellation)
        {
            var doc = await _repository.GetByIdAsync(request.Id);
            if (doc == null) return null;

            return new DocumentResponse(doc.Id, doc.Title, doc.Content, doc.CreatedAt, doc.LastModifiedAt);
        }
    }
}
