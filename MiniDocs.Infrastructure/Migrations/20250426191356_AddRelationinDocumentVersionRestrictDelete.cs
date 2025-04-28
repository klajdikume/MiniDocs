using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniDocs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationinDocumentVersionRestrictDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentVersions_Documents_DocumentId",
                table: "DocumentVersions");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentVersions_Documents_DocumentId",
                table: "DocumentVersions",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentVersions_Documents_DocumentId",
                table: "DocumentVersions");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentVersions_Documents_DocumentId",
                table: "DocumentVersions",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
