using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinemo.Data.Migrations
{
    public partial class changemovielength : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Runtime",
                table: "Movies",
                newName: "Length");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Length",
                table: "Movies",
                newName: "Runtime");
        }
    }
}
