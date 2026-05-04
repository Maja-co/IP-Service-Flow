using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class DummeMail2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServiceOpgaver",
                keyColumn: "Id",
                keyValue: 1,
                column: "SidstUdførtDato",
                value: new DateOnly(2025, 6, 4));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServiceOpgaver",
                keyColumn: "Id",
                keyValue: 1,
                column: "SidstUdførtDato",
                value: new DateOnly(2025, 5, 30));
        }
    }
}
