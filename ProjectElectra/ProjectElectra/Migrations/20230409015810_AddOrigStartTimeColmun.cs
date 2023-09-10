using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectElectra.Migrations
{
    public partial class AddOrigStartTimeColmun : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "OrignalStartTime",
                table: "GetGuildServerSettings",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "OrignalStartTime",
                table: "GetGuildServerSettings");
        }
    }
}
