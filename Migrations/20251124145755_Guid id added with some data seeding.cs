using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace PersonalExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class Guididaddedwithsomedataseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Expenses",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Amount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Notes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expenses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Expenses_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "Email", "Name", "Password" },
                values: new object[,]
                {
                    { new Guid("52a6240b-0f04-4be8-b54a-d1f6c6e05522"), new DateTime(2025, 11, 24, 14, 57, 53, 526, DateTimeKind.Utc).AddTicks(9842), "ali@example.com", "Ali Khan", "password123" },
                    { new Guid("c6b7c56f-1c72-4c2e-9c11-5cdd7d00c111"), new DateTime(2025, 11, 24, 14, 57, 53, 526, DateTimeKind.Utc).AddTicks(9833), "ahmad@example.com", "Ahmad Raza", "123456" }
                });

            migrationBuilder.InsertData(
                table: "Expenses",
                columns: new[] { "Id", "Amount", "Date", "Notes", "Title", "UserId" },
                values: new object[,]
                {
                    { new Guid("ab7bb8f4-950e-4c84-b6d7-214a96ac3333"), 5200.00m, new DateTime(2025, 11, 23, 14, 57, 53, 526, DateTimeKind.Utc).AddTicks(9904), "Car petrol full tank", "Fuel Refill", new Guid("c6b7c56f-1c72-4c2e-9c11-5cdd7d00c111") },
                    { new Guid("d39afe32-a4f7-4d53-9668-0f0726c44444"), 2500.00m, new DateTime(2025, 11, 19, 14, 57, 53, 526, DateTimeKind.Utc).AddTicks(9906), "PTCL monthly bill", "Internet Bill", new Guid("52a6240b-0f04-4be8-b54a-d1f6c6e05522") },
                    { new Guid("f94c2c07-9f92-4c21-9cb8-df1e0227d333"), 2500.75m, new DateTime(2025, 11, 21, 14, 57, 53, 526, DateTimeKind.Utc).AddTicks(9893), "Bought fruits, vegetables, snacks", "Grocery Shopping", new Guid("c6b7c56f-1c72-4c2e-9c11-5cdd7d00c111") }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_UserId",
                table: "Expenses",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expenses");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
