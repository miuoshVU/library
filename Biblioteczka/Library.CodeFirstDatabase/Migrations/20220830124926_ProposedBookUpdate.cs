using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library.CodeFirstDatabase.Migrations
{
    public partial class ProposedBookUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthorProposedBook");

            migrationBuilder.DropTable(
                name: "CategoryProposedBook");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Proposed_Books",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Authors",
                table: "Proposed_Books",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Categories",
                table: "Proposed_Books",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Proposed_Books",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Proposed_Books_AuthorId",
                table: "Proposed_Books",
                column: "AuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_Proposed_Books_CategoryId",
                table: "Proposed_Books",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposed_Books_Authors_AuthorId",
                table: "Proposed_Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Proposed_Books_Categories_CategoryId",
                table: "Proposed_Books",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Proposed_Books_Authors_AuthorId",
                table: "Proposed_Books");

            migrationBuilder.DropForeignKey(
                name: "FK_Proposed_Books_Categories_CategoryId",
                table: "Proposed_Books");

            migrationBuilder.DropIndex(
                name: "IX_Proposed_Books_AuthorId",
                table: "Proposed_Books");

            migrationBuilder.DropIndex(
                name: "IX_Proposed_Books_CategoryId",
                table: "Proposed_Books");

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Proposed_Books");

            migrationBuilder.DropColumn(
                name: "Authors",
                table: "Proposed_Books");

            migrationBuilder.DropColumn(
                name: "Categories",
                table: "Proposed_Books");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Proposed_Books");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Users",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ReturnDate",
                table: "Borrows",
                type: "timestamp with time zone",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<Guid>(
                name: "Id",
                table: "Borrows",
                type: "uuid",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer")
                .OldAnnotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.CreateTable(
                name: "AuthorProposedBook",
                columns: table => new
                {
                    AuthorsId = table.Column<int>(type: "integer", nullable: false),
                    ProposedBooksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthorProposedBook", x => new { x.AuthorsId, x.ProposedBooksId });
                    table.ForeignKey(
                        name: "FK_AuthorProposedBook_Authors_AuthorsId",
                        column: x => x.AuthorsId,
                        principalTable: "Authors",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AuthorProposedBook_Proposed_Books_ProposedBooksId",
                        column: x => x.ProposedBooksId,
                        principalTable: "Proposed_Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CategoryProposedBook",
                columns: table => new
                {
                    CategoriesId = table.Column<int>(type: "integer", nullable: false),
                    ProposedBooksId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryProposedBook", x => new { x.CategoriesId, x.ProposedBooksId });
                    table.ForeignKey(
                        name: "FK_CategoryProposedBook_Categories_CategoriesId",
                        column: x => x.CategoriesId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CategoryProposedBook_Proposed_Books_ProposedBooksId",
                        column: x => x.ProposedBooksId,
                        principalTable: "Proposed_Books",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AuthorProposedBook_ProposedBooksId",
                table: "AuthorProposedBook",
                column: "ProposedBooksId");

            migrationBuilder.CreateIndex(
                name: "IX_CategoryProposedBook_ProposedBooksId",
                table: "CategoryProposedBook",
                column: "ProposedBooksId");
        }
    }
}
