using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ServerList.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SplitServerAndModerationStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "game_server");

            migrationBuilder.AddColumn<string>(
                name: "ModerationStatus",
                table: "game_server",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ServerStatus",
                table: "game_server",
                type: "character varying(32)",
                maxLength: 32,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ModerationStatus",
                table: "game_server");

            migrationBuilder.DropColumn(
                name: "ServerStatus",
                table: "game_server");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "game_server",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
