using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniOnlineLibrar.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedapilog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "api_logs",
                columns: table => new
                {
                    Api_Log_Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Edpoint = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Request_Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response_Body = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true),
                    Status_Code = table.Column<int>(type: "int", nullable: false),
                    Ip_Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Response_Time_Ms = table.Column<int>(type: "int", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_api_logs", x => x.Api_Log_Id);
                    table.ForeignKey(
                        name: "FK_api_logs_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_api_logs_UserId",
                table: "api_logs",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "api_logs");
        }
    }
}
