using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniDocs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationinDocumentVersion : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "DocumentId1",
                table: "DocumentVersions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_DocumentVersions_DocumentId1",
                table: "DocumentVersions",
                column: "DocumentId1");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentVersions_Documents_DocumentId1",
                table: "DocumentVersions",
                column: "DocumentId1",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentVersions_Documents_DocumentId1",
                table: "DocumentVersions");

            migrationBuilder.DropIndex(
                name: "IX_DocumentVersions_DocumentId1",
                table: "DocumentVersions");

            migrationBuilder.DropColumn(
                name: "DocumentId1",
                table: "DocumentVersions");
        }
    }
}
