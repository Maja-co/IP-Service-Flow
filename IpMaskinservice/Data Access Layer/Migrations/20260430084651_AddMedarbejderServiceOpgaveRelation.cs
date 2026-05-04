using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class AddMedarbejderServiceOpgaveRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOpgaver_Medarbejdere_MedarbejderId",
                table: "ServiceOpgaver");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOpgaver_Medarbejdere_MedarbejderId",
                table: "ServiceOpgaver",
                column: "MedarbejderId",
                principalTable: "Medarbejdere",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ServiceOpgaver_Medarbejdere_MedarbejderId",
                table: "ServiceOpgaver");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceOpgaver_Medarbejdere_MedarbejderId",
                table: "ServiceOpgaver",
                column: "MedarbejderId",
                principalTable: "Medarbejdere",
                principalColumn: "Id");
        }
    }
}
