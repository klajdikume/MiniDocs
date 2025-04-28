using Microsoft.EntityFrameworkCore;
using MiniDocs.Domain;
using MiniDocs.Domain.Model;

namespace MiniDocs.Infrastructure.Database
{
    public class AppDbContext: DbContext
    {
        public DbSet<Document> Documents => Set<Document>();
        public DbSet<DocumentVersion> DocumentVersions => Set<DocumentVersion>();
        public DbSet<DocumentUserPermission> DocumentUserPermissions => Set<DocumentUserPermission>();
        public DbSet<ArchivedDocumentVersion> ArchivedDocumentVersions => Set<ArchivedDocumentVersion>();
        public DbSet<Comment> Comments {  get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(c => c.Id);
                entity.Property(c => c.Text).IsRequired();
                entity.HasOne(c => c.Document)
                      .WithMany(d => d.Comments)
                      .HasForeignKey(c => c.DocumentId);
                      
            });

            //modelBuilder.Entity<DocumentVersion>()
            //    .HasOne<Document>()
            //    .WithMany()
            //    .HasForeignKey(v => v.DocumentId);

            modelBuilder.Entity<DocumentUserPermission>()
                .HasOne<Document>()
                .WithMany(d => d.Permissions)
                .HasForeignKey(p => p.DocumentId);
        }

        public AppDbContext(DbContextOptions<AppDbContext> options) 
            : base(options) { }
    }
}
