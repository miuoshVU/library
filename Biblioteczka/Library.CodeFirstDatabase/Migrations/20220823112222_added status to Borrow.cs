using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.CodeFirstDatabase.Migrations
{
    public partial class addedstatustoBorrow : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Borrows",
                type: "integer",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Borrows");
        }
    }
}
