using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProjectElectra.Migrations
{
    public partial class InitCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GetGuildServerSettings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GuildId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    AssignedChannelId = table.Column<decimal>(type: "decimal(20,0)", nullable: false),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PostingInterval = table.Column<int>(type: "int", nullable: false),
                    NumberOfTimesToPost = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GetGuildServerSettings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GetGuildServerSettings");
        }
    }
}