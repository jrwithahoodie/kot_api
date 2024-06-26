using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class init_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Players_NIF",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "NIF",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Editions",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IsActive", "Name" },
                values: new object[] { false, "27082022" });

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IsActive", "Name" },
                values: new object[] { false, "26022023" });

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IsActive", "Name" },
                values: new object[] { false, "05082023" });

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IsActive", "Name" },
                values: new object[] { true, "27072024" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Editions");

            migrationBuilder.AlterColumn<string>(
                name: "NIF",
                table: "Players",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 1,
                column: "Name",
                value: "27/08/2022");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 2,
                column: "Name",
                value: "26/02/2023");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 3,
                column: "Name",
                value: "05/08/2023");

            migrationBuilder.UpdateData(
                table: "Editions",
                keyColumn: "Id",
                keyValue: 4,
                column: "Name",
                value: "27/07/2024");

            migrationBuilder.CreateIndex(
                name: "IX_Players_NIF",
                table: "Players",
                column: "NIF",
                unique: true,
                filter: "[NIF] IS NOT NULL");
        }
    }
}
