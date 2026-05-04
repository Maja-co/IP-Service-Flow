using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class nyDatoService : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServiceOpgaver",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Deadline", "SidstUdførtDato" },
                values: new object[] { new DateOnly(2026, 5, 30), new DateOnly(2025, 5, 30) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServiceOpgaver",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Deadline", "SidstUdførtDato" },
                values: new object[] { new DateOnly(2026, 10, 1), new DateOnly(2025, 10, 1) });
        }
    }
}
