using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinemo.Data.Migrations
{
    public partial class addtheater : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TheaterId",
                table: "Rooms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Theaters",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Theaters", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_TheaterId",
                table: "Rooms",
                column: "TheaterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Theaters_TheaterId",
                table: "Rooms",
                column: "TheaterId",
                principalTable: "Theaters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Theaters_TheaterId",
                table: "Rooms");

            migrationBuilder.DropTable(
                name: "Theaters");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_TheaterId",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "TheaterId",
                table: "Rooms");
        }
    }
}
