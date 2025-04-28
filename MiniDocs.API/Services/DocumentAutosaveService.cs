using Microsoft.EntityFrameworkCore;
using MiniDocs.API.Hubs;
using MiniDocs.Domain.Model;
using MiniDocs.Infrastructure.Database;

namespace MiniDocs.API.Services
{
    public class DocumentAutosaveService : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private readonly ILogger<DocumentAutosaveService> _logger;
        private readonly TimeSpan _autosaveInterval = TimeSpan.FromSeconds(15);

        public DocumentAutosaveService(IServiceScopeFactory scopeFactory, ILogger<DocumentAutosaveService> logger)
        {
            _scopeFactory = scopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await Task.Delay(_autosaveInterval, stoppingToken);

                using var scope = _scopeFactory.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                foreach (var (docId, (content, lastSaved)) in DocumentHub._autosaveBuffer.ToList())
                {
                    try
                    {
                        var doc = await db.Documents.Include(d => d.Versions)
                            .FirstOrDefaultAsync(d => d.Id == docId, stoppingToken);

                        if (doc == null) continue;

                        doc.Content = content;
                        doc.LastModifiedAt = DateTime.UtcNow;

                        doc.Versions.Add(new DocumentVersion
                        {
                            DocumentId = docId,
                            Content = content
                        });

                        await db.SaveChangesAsync(stoppingToken);

                        DocumentHub._autosaveBuffer.Remove(docId);

                        _logger.LogInformation($"Auto-saved document {docId}");
                    }
                    catch (Exception ex) 
                    {
                        _logger.LogError(ex, $"Error autosaving document {docId}");
                    }
                }
            }
        }
    }
}
