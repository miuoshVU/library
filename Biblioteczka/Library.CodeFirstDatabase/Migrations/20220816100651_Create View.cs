using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library.CodeFirstDatabase.Migrations
{
    public partial class CreateView : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("create view \"CategoryView\" as select \"Id\", \"Name\" from \"Categories\";");
            migrationBuilder.Sql("create view \"BookView\" as  select \"Id\", \"Title\" from \"Books\";");
            migrationBuilder.Sql("create view \"AuthorView\" as select \"Id\", concat(\"FirstName\", \"LastName\") as \"NameAndLastName\" from \"Authors\"; ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"drop view CategoryView;");
            migrationBuilder.Sql(@"drop view BookView;");
            migrationBuilder.Sql(@"drop view AuthorView;");
        }
    }
}
