using MediatR;
using MiniDocs.Application.Repos;
using MiniDocs.Domain;

namespace MiniDocs.Application.Documents.Commands
{
    public record CreateDocumentCommand(string Title, Guid OwnerUserId): IRequest<Guid>;
    public class CreateDocumentCommandHandler: IRequestHandler<CreateDocumentCommand, Guid>
    {
        private readonly IDocumentRepository _repository;
        public CreateDocumentCommandHandler(IDocumentRepository repository)
        {
                _repository = repository;
        }

        public async Task<Guid> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var doc = new Document
            {
                Id = Guid.NewGuid(),
                Title = request.Title,
                OwnerUserId = request.OwnerUserId,
            };

            await _repository.AddAsync(doc);

            return doc.Id;
        }
    }
}
 