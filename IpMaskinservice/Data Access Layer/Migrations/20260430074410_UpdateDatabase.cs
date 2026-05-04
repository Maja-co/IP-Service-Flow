using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class UpdateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Medarbejdere",
                keyColumn: "Id",
                keyValue: "M1",
                columns: new[] { "KodeOrdHash", "Salt" },
                values: new object[] { "96cae35ce8a9b0244178bf28e4966c2ce1b8385723a96a6b838858cdd6ca0a1e", "123" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Medarbejdere",
                keyColumn: "Id",
                keyValue: "M1",
                columns: new[] { "KodeOrdHash", "Salt" },
                values: new object[] { null, null });
        }
    }
}
