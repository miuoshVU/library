using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.CodeFirstDatabase.Migrations
{
    public partial class fix_relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Author",
                table: "Proposed_Books");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Author",
                table: "Proposed_Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
