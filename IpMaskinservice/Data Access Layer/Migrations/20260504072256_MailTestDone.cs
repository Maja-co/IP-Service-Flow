using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class MailTestDone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Medarbejdere",
                keyColumn: "Id",
                keyValue: "M1",
                column: "MailAdresse",
                value: "admin@ipmaskin.dk");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Medarbejdere",
                keyColumn: "Id",
                keyValue: "M1",
                column: "MailAdresse",
                value: "runehjensen@hotmail.dk");
        }
    }
}
