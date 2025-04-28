using Microsoft.EntityFrameworkCore;
using MiniDocs.Domain.Model;
using MiniDocs.Infrastructure.Database;
using System;
using System.IO.Compression;
using System.Text.Json;

namespace MiniDocs.API.Services
{
    public class AutoArchiverService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TimeSpan _archiveAge = TimeSpan.FromDays(30);
        public AutoArchiverService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using var scope = _serviceProvider.CreateScope();
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                var blobService = scope.ServiceProvider.GetRequiredService<BlobStorageService>();

                var cutoffDate = DateTime.UtcNow - _archiveAge;

                var oldVersions = await db.DocumentVersions
                    .Where(v => v.CreatedAtUtc <= cutoffDate && !v.IsArchived)
                    .ToListAsync(stoppingToken);
                
                foreach (var version in oldVersions)
                {
                    try
                    {
                        var json = JsonSerializer.Serialize(version);
                        var compressedBase64 = CompressToBase64(json);

                        var blobName = $"documentversions/{version.Id}_{DateTime.UtcNow:yyyyMMddHHmmss}.gz";

                        await blobService.UploadCompressedVersionAsync(blobName, compressedBase64);

                        db.DocumentVersions.Remove(version);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Failed to archive version {version.Id}: {ex.Message}");
                    }

                    var archive = new ArchivedDocumentVersion
                    {
                        DocumentId = version.DocumentId,
                        SectionId = version.SectionId,
                        CompressedData = CompressVersion(version),
                        OriginalCreatedAt = version.CreatedAtUtc,
                        ArchivedAt = DateTime.UtcNow
                    };

                    db.ArchivedDocumentVersions.Add(archive);

                    version.IsArchived = true;
                    version.ArchivedOn = DateTime.UtcNow;
                }
                
                
                await db.SaveChangesAsync(stoppingToken);

                await Task.Delay(TimeSpan.FromHours(24), stoppingToken);
            }
        }

        private string CompressVersion(DocumentVersion version)
        {
            var json = JsonSerializer.Serialize(version);
            return CompressToBase64(json);
        }

        private string CompressToBase64(string json)
        {
            using var output = new MemoryStream();
            using (var gzip = new GZipStream(output, CompressionLevel.Optimal))
            using (var writter = new StreamWriter(gzip)) 
            {
                writter.Write(json); 
            }

            return Convert.ToBase64String(output.ToArray());
        }
    }
}
