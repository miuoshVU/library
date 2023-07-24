using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Library.CodeFirstDatabase.Migrations
{
    public partial class updatelogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Log_Entries_Db_Operations_DbOperationId",
                table: "Log_Entries");

            migrationBuilder.DropTable(
                name: "Db_Operations");

            migrationBuilder.DropIndex(
                name: "IX_Log_Entries_DbOperationId",
                table: "Log_Entries");

            migrationBuilder.DropColumn(
                name: "DbOperationId",
                table: "Log_Entries");

            migrationBuilder.RenameColumn(
                name: "EnteryDescription",
                table: "Log_Entries",
                newName: "Operation");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Log_Entries",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "Log_Entries");

            migrationBuilder.RenameColumn(
                name: "Operation",
                table: "Log_Entries",
                newName: "EnteryDescription");

            migrationBuilder.AddColumn<int>(
                name: "DbOperationId",
                table: "Log_Entries",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Db_Operations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Code = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Db_Operations", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Log_Entries_DbOperationId",
                table: "Log_Entries",
                column: "DbOperationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Log_Entries_Db_Operations_DbOperationId",
                table: "Log_Entries",
                column: "DbOperationId",
                principalTable: "Db_Operations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
