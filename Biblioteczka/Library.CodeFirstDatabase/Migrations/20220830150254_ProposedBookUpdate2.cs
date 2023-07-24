using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.CodeFirstDatabase.Migrations
{
    public partial class ProposedBookUpdate2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Cover",
                table: "Proposed_Books",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cover",
                table: "Proposed_Books");
        }
    }
}
