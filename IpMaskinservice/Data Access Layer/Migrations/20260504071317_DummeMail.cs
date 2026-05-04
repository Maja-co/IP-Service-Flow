using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class DummeMail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServiceOpgaver",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deadline",
                value: new DateOnly(2026, 6, 4));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "ServiceOpgaver",
                keyColumn: "Id",
                keyValue: 1,
                column: "Deadline",
                value: new DateOnly(2026, 6, 1));
        }
    }
}
