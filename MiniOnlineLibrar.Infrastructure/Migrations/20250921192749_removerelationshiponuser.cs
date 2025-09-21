using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniOnlineLibrar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class removerelationshiponuser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_api_logs_Users_UserId",
                table: "api_logs");

            migrationBuilder.DropIndex(
                name: "IX_api_logs_UserId",
                table: "api_logs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_api_logs_UserId",
                table: "api_logs",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_api_logs_Users_UserId",
                table: "api_logs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
