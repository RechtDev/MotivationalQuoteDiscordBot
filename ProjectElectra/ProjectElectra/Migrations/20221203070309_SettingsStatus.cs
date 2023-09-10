using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectElectra.Migrations
{
    public partial class SettingsStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasUpdated",
                table: "GetGuildServerSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsNew",
                table: "GetGuildServerSettings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasUpdated",
                table: "GetGuildServerSettings");

            migrationBuilder.DropColumn(
                name: "IsNew",
                table: "GetGuildServerSettings");
        }
    }
}