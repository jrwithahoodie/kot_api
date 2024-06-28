using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class clean_init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Games",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Players",
                keyColumn: "Id",
                keyValue: 14);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Teams",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Teams",
                columns: new[] { "Id", "CategoryId", "Classification_points", "Defeats", "EditionId", "GroupId", "Name", "Pay", "Points_diff", "Wins" },
                values: new object[,]
                {
                    { 1, 1, 0, 2, 1, 1, "teamMasc1A", false, -33, 0 },
                    { 2, 1, 3, 0, 1, 1, "teamMasc2A", false, 13, 1 },
                    { 3, 1, 3, 0, 1, 1, "teamMasc3A", false, 20, 1 },
                    { 4, 1, 0, 0, 1, 2, "teamMasc1B", false, 0, 0 },
                    { 5, 1, 0, 0, 1, 2, "teamMasc2B", false, 0, 0 },
                    { 6, 1, 0, 0, 1, 2, "teamMasc3B", false, 0, 0 },
                    { 7, 1, 0, 0, 1, 3, "teamMasc1C", false, 0, 0 },
                    { 8, 1, 0, 0, 1, 3, "teamMasc2C", false, 0, 0 },
                    { 9, 1, 0, 0, 1, 3, "teamMasc3C", false, 0, 0 },
                    { 10, 2, 0, 0, 1, 4, "teamFem1A", false, 0, 0 },
                    { 11, 2, 0, 0, 1, 4, "teamFem2A", false, 0, 0 },
                    { 12, 2, 0, 0, 1, 5, "teamFem1B", false, 0, 0 },
                    { 13, 2, 0, 0, 1, 5, "teamFem2B", false, 0, 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Mail", "Name", "PlayerId", "Role" },
                values: new object[,]
                {
                    { 1, "kingofthetower3x3@gmail.com", "José Ramón", null, "admin" },
                    { 2, "jorapijo42@gmail.com", "José Ramón", null, "staff" },
                    { 3, "jorapijo@gmail.com", "José Ramón", null, "base_user" }
                });

            migrationBuilder.InsertData(
                table: "Games",
                columns: new[] { "Id", "Court", "Schedule", "Score1", "Score1Old", "Score2", "Score2Old", "StaffId", "Team1Id", "Team2Id" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, 0, 15, 0, 2, 1, 2 },
                    { 2, 2, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 21, 0, 1, 0, 2, 3, 1 }
                });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "Id", "Instagram", "NIF", "Name", "Phone", "TeamId", "WantPics" },
                values: new object[,]
                {
                    { 1, "playerIg", "12354678Z", "playerM1A1", "111222333", 1, true },
                    { 3, "playerIg", "12354678A", "playerM1A2", "111222333", 1, true },
                    { 4, "playerIg", "12354678B", "playerM1A3", "111222333", 1, true },
                    { 5, "playerIg", "12354678C", "playerM2A1", "111222333", 2, true },
                    { 6, "playerIg", "12354678D", "playerM2A2", "111222333", 2, true },
                    { 7, "playerIg", "12354678E", "playerM2A3", "111222333", 2, true },
                    { 8, "playerIg", "12354678F", "playerM3A1", "111222333", 3, true },
                    { 9, "playerIg", "12354678G", "playerM3A2", "111222333", 3, true },
                    { 10, "playerIg", "12354678H", "playerM3A3", "111222333", 3, true },
                    { 11, "playerIg", "12354678I", "playerF1A1", "111222333", 10, true },
                    { 12, "playerIg", "12354678J", "playerF1A2", "111222333", 10, true },
                    { 13, "playerIg", "12354678K", "playerF2B1", "111222333", 10, true },
                    { 14, "playerIg", "12354678ZL", "playerF2B3", "111222333", 10, true }
                });
        }
    }
}
