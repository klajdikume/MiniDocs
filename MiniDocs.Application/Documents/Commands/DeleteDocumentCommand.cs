using MediatR;
using MiniDocs.Application.Repos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MiniDocs.Application.Documents.Commands
{
    public record DeleteDocumentCommand(Guid Id) : IRequest;

    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand>
    {
        private readonly IDocumentRepository _repository;

        public DeleteDocumentCommandHandler(IDocumentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            await _repository.DeleteAsync(request.Id);
            return Unit.Value;
        }

        Task IRequestHandler<DeleteDocumentCommand>.Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            return Handle(request, cancellationToken);
        }
    }
}
