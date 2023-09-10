using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectElectra.Migrations
{
    public partial class AlreadyPostedColumn : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TimesPostedAlready",
                table: "GetGuildServerSettings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TimesPostedAlready",
                table: "GetGuildServerSettings");
        }
    }
}
