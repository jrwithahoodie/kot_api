using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Groups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Groups", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Teams",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Pay = table.Column<bool>(type: "bit", nullable: false),
                    Wins = table.Column<int>(type: "int", nullable: false),
                    Defeats = table.Column<int>(type: "int", nullable: false),
                    Points_diff = table.Column<int>(type: "int", nullable: false),
                    Classification_points = table.Column<int>(type: "int", nullable: false),
                    GroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Teams", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Teams_Groups_GroupId",
                        column: x => x.GroupId,
                        principalTable: "Groups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NIF = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Instagram = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    WantPics = table.Column<bool>(type: "bit", nullable: false),
                    TeamId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Players_Teams_TeamId",
                        column: x => x.TeamId,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Mail = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Games",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Team1Id = table.Column<int>(type: "int", nullable: false),
                    Team2Id = table.Column<int>(type: "int", nullable: false),
                    Score1 = table.Column<int>(type: "int", nullable: false),
                    Score2 = table.Column<int>(type: "int", nullable: false),
                    Score1Old = table.Column<int>(type: "int", nullable: false),
                    Score2Old = table.Column<int>(type: "int", nullable: false),
                    Court = table.Column<int>(type: "int", nullable: false),
                    Schedule = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StaffId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Games", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Games_Teams_Team1Id",
                        column: x => x.Team1Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Games_Teams_Team2Id",
                        column: x => x.Team2Id,
                        principalTable: "Teams",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Games_Users_StaffId",
                        column: x => x.StaffId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "MasculinoA" },
                    { 2, "MasculinoB" },
                    { 3, "MasculinoC" },
                    { 4, "FemeninoA" },
                    { 5, "FemeninoB" }
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
                table: "Teams",
                columns: new[] { "Id", "Category", "Classification_points", "Defeats", "GroupId", "Name", "Pay", "Points_diff", "Wins" },
                values: new object[,]
                {
                    { 1, "Masculino", 0, 2, 1, "teamMasc1A", false, -33, 0 },
                    { 2, "Masculino", 3, 0, 1, "teamMasc2A", false, 13, 1 },
                    { 3, "Masculino", 3, 0, 1, "teamMasc3A", false, 20, 1 },
                    { 4, "Masculino", 0, 0, 2, "teamMasc1B", false, 0, 0 },
                    { 5, "Masculino", 0, 0, 2, "teamMasc2B", false, 0, 0 },
                    { 6, "Masculino", 0, 0, 2, "teamMasc3B", false, 0, 0 },
                    { 7, "Masculino", 0, 0, 3, "teamMasc1C", false, 0, 0 },
                    { 8, "Masculino", 0, 0, 3, "teamMasc2C", false, 0, 0 },
                    { 9, "Masculino", 0, 0, 3, "teamMasc3C", false, 0, 0 },
                    { 10, "Femenino", 0, 0, 4, "teamFem1A", false, 0, 0 },
                    { 11, "Femenino", 0, 0, 4, "teamFem2A", false, 0, 0 },
                    { 12, "Femenino", 0, 0, 5, "teamFem1B", false, 0, 0 },
                    { 13, "Femenino", 0, 0, 5, "teamFem2B", false, 0, 0 }
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

            migrationBuilder.CreateIndex(
                name: "IX_Games_StaffId",
                table: "Games",
                column: "StaffId");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Team1Id",
                table: "Games",
                column: "Team1Id");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Team2Id",
                table: "Games",
                column: "Team2Id");

            migrationBuilder.CreateIndex(
                name: "IX_Players_NIF",
                table: "Players",
                column: "NIF",
                unique: true,
                filter: "[NIF] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Players_TeamId",
                table: "Players",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Teams_GroupId",
                table: "Teams",
                column: "GroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Mail",
                table: "Users",
                column: "Mail",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_PlayerId",
                table: "Users",
                column: "PlayerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Games");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Teams");

            migrationBuilder.DropTable(
                name: "Groups");
        }
    }
}
