using MediatR;
using MiniDocs.Application.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDocs.Application.Documents.Commands
{
    public record UpdateDocumentCommand(Guid Id, string Title, string Content) : IRequest;

    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand>
    {
        private readonly IDocumentRepository _repository;

        public UpdateDocumentCommandHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var doc = await _repository.GetByIdAsync(request.Id);
            if (doc == null) throw new Exception("Document not found");

            doc.Title = request.Title;
            doc.Content = request.Content;
            doc.LastModifiedAt = DateTime.UtcNow;

            await _repository.UpdateAsync(doc);

            return Unit.Value;
        }

        Task IRequestHandler<UpdateDocumentCommand>.Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
