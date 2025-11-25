using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PersonalExpenseTracker.Migrations
{
    /// <inheritdoc />
    public partial class passwordhashtouser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Password",
                table: "Users",
                newName: "PasswordHash");

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: new Guid("ab7bb8f4-950e-4c84-b6d7-214a96ac3333"),
                column: "Date",
                value: new DateTime(2025, 11, 24, 14, 27, 2, 536, DateTimeKind.Utc).AddTicks(2183));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: new Guid("d39afe32-a4f7-4d53-9668-0f0726c44444"),
                column: "Date",
                value: new DateTime(2025, 11, 20, 14, 27, 2, 536, DateTimeKind.Utc).AddTicks(2185));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: new Guid("f94c2c07-9f92-4c21-9cb8-df1e0227d333"),
                column: "Date",
                value: new DateTime(2025, 11, 22, 14, 27, 2, 536, DateTimeKind.Utc).AddTicks(2173));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("52a6240b-0f04-4be8-b54a-d1f6c6e05522"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 14, 27, 2, 536, DateTimeKind.Utc).AddTicks(2125));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c6b7c56f-1c72-4c2e-9c11-5cdd7d00c111"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 25, 14, 27, 2, 536, DateTimeKind.Utc).AddTicks(2119));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                table: "Users",
                newName: "Password");

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: new Guid("ab7bb8f4-950e-4c84-b6d7-214a96ac3333"),
                column: "Date",
                value: new DateTime(2025, 11, 23, 14, 57, 53, 526, DateTimeKind.Utc).AddTicks(9904));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: new Guid("d39afe32-a4f7-4d53-9668-0f0726c44444"),
                column: "Date",
                value: new DateTime(2025, 11, 19, 14, 57, 53, 526, DateTimeKind.Utc).AddTicks(9906));

            migrationBuilder.UpdateData(
                table: "Expenses",
                keyColumn: "Id",
                keyValue: new Guid("f94c2c07-9f92-4c21-9cb8-df1e0227d333"),
                column: "Date",
                value: new DateTime(2025, 11, 21, 14, 57, 53, 526, DateTimeKind.Utc).AddTicks(9893));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("52a6240b-0f04-4be8-b54a-d1f6c6e05522"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 24, 14, 57, 53, 526, DateTimeKind.Utc).AddTicks(9842));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c6b7c56f-1c72-4c2e-9c11-5cdd7d00c111"),
                column: "CreatedAt",
                value: new DateTime(2025, 11, 24, 14, 57, 53, 526, DateTimeKind.Utc).AddTicks(9833));
        }
    }
}
