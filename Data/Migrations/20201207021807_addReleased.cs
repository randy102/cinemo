using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Cinemo.Data.Migrations
{
    public partial class addReleased : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Released",
                table: "Movies",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Released",
                table: "Movies");
        }
    }
}
