using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniDocs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OnDeleteRestrict : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Documents_DocumentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentUserPermissions_Documents_DocumentId",
                table: "DocumentUserPermissions");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Documents_DocumentId",
                table: "Comments",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentUserPermissions_Documents_DocumentId",
                table: "DocumentUserPermissions",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Documents_DocumentId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_DocumentUserPermissions_Documents_DocumentId",
                table: "DocumentUserPermissions");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Documents_DocumentId",
                table: "Comments",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentUserPermissions_Documents_DocumentId",
                table: "DocumentUserPermissions",
                column: "DocumentId",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
