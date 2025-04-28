using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniDocs.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationinDocumentVersionChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentVersions_Documents_DocumentId1",
                table: "DocumentVersions");

            migrationBuilder.DropColumn(
                name: "Version",
                table: "DocumentVersions");

            migrationBuilder.RenameColumn(
                name: "CreatedAt",
                table: "DocumentVersions",
                newName: "CreatedAtUtc");

            migrationBuilder.RenameColumn(
                name: "ContentSnapshot",
                table: "DocumentVersions",
                newName: "Title");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentId1",
                table: "DocumentVersions",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "DocumentVersions",
                type: "int",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<string>(
                name: "CreatedByUserId",
                table: "DocumentVersions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentVersions_Documents_DocumentId1",
                table: "DocumentVersions",
                column: "DocumentId1",
                principalTable: "Documents",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DocumentVersions_Documents_DocumentId1",
                table: "DocumentVersions");

            migrationBuilder.DropColumn(
                name: "CreatedByUserId",
                table: "DocumentVersions");

            migrationBuilder.RenameColumn(
                name: "Title",
                table: "DocumentVersions",
                newName: "ContentSnapshot");

            migrationBuilder.RenameColumn(
                name: "CreatedAtUtc",
                table: "DocumentVersions",
                newName: "CreatedAt");

            migrationBuilder.AlterColumn<Guid>(
                name: "DocumentId1",
                table: "DocumentVersions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "DocumentVersions",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Version",
                table: "DocumentVersions",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_DocumentVersions_Documents_DocumentId1",
                table: "DocumentVersions",
                column: "DocumentId1",
                principalTable: "Documents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
