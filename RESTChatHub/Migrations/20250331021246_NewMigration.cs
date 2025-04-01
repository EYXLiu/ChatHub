using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatHub.Migrations
{
    /// <inheritdoc />
    public partial class NewMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sender",
                table: "ChatMessages");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sender",
                table: "ChatMessages",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }
    }
}
