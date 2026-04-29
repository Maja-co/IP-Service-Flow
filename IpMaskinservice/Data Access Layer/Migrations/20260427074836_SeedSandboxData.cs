using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class SeedSandboxData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AktivSessionID",
                table: "Medarbejdere",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.InsertData(
                table: "EftersynsRegler",
                columns: new[] { "Id", "Regel" },
                values: new object[,]
                {
                    { 1, "Tjek bremser for slid" },
                    { 2, "Tjek nødstop funktion" }
                });

            migrationBuilder.InsertData(
                table: "Kunder",
                columns: new[] { "Id", "Adresse", "CvrNummer", "ErAktiv", "FirmaNavn", "KontaktPersonNavn", "KontaktPersonTelefonnummer", "MailAdresse" },
                values: new object[,]
                {
                    { 1, "Testvej 1", 11223344, true, "Byg & Maskin A/S", "Jens Jensen", "12341234", "B&M@test.com" },
                    { 2, "Testvej 2", 99887766, true, "Skovens Entreprenør", "Mia Skov", "43214321", "Skovens@test.com" }
                });

            migrationBuilder.InsertData(
                table: "MaterialeType",
                columns: new[] { "Id", "MaterialeBeskrivelse" },
                values: new object[,]
                {
                    { 1, "Motorolie 5W-30" },
                    { 2, "Hydraulikslange 2m" }
                });

            migrationBuilder.InsertData(
                table: "Medarbejdere",
                columns: new[] { "Id", "AktivSessionID", "KodeOrdHash", "MailAdresse", "MedarbejderNavn", "Salt" },
                values: new object[] { "M1", null, null, "admin@ipmaskin.dk", "Admin Alice", null });

            migrationBuilder.InsertData(
                table: "OpgaveTyper",
                columns: new[] { "Id", "OpgaveBeskrivelse" },
                values: new object[,]
                {
                    { 1, "Olieskift" },
                    { 2, "Udskiftning af hydraulikslange" }
                });

            migrationBuilder.InsertData(
                table: "ServiceTeknikkere",
                columns: new[] { "Id", "TeknikkerNavn", "TelefonNummer" },
                values: new object[] { 1, "Tom Værktøj", "11223344" });

            migrationBuilder.InsertData(
                table: "Maskiner",
                columns: new[] { "Id", "KundeId", "MaskineType", "Producent", "SerieNummer" },
                values: new object[,]
                {
                    { 1, 1, 0, "Volvo", "SN-1001" },
                    { 2, 2, 2, "CAT", "SN-2002" }
                });

            migrationBuilder.InsertData(
                table: "ServiceOpgaver",
                columns: new[] { "Id", "Deadline", "MaskineId", "MaterialeListeId", "MedarbejderId", "ServiceInterval", "ServiceTeknikkerId", "SidstUdførtDato", "SidstUdførtNote" },
                values: new object[,]
                {
                    { 1, new DateOnly(2026, 10, 1), 1, null, "M1", 2, 1, new DateOnly(2025, 10, 1), "Olie skiftet, alt ok" },
                    { 2, new DateOnly(2026, 11, 1), 2, null, "M1", 2, 1, new DateOnly(2025, 11, 1), "Nødstop testet og godkendt" }
                });

            migrationBuilder.InsertData(
                table: "AfsluttedeServices",
                columns: new[] { "Id", "MaskineId", "Note", "ServiceOpgaveId", "UdførtDato" },
                values: new object[] { 1, 1, "Service afsluttet uden anmærkninger", 1, new DateOnly(2025, 10, 1) });

            migrationBuilder.InsertData(
                table: "Påmindelser",
                columns: new[] { "Id", "PåmindelsesDato", "ServiceOpgaveId" },
                values: new object[,]
                {
                    { 1, new DateOnly(2026, 9, 1), 1 },
                    { 2, new DateOnly(2026, 10, 1), 2 }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Servicetype" },
                values: new object[] { 1, 0 });

            migrationBuilder.InsertData(
                table: "SikkerhedsEftersyn",
                column: "Id",
                value: 2);

            migrationBuilder.InsertData(
                table: "EftersynsRegelLinks",
                columns: new[] { "EftersynsRegelListeId", "SikkerhedsEftersynId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "ServiceOpgaveTypeLinks",
                columns: new[] { "OpgaveTypeListeId", "ServiceId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 2, 1 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AfsluttedeServices",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "EftersynsRegelLinks",
                keyColumns: new[] { "EftersynsRegelListeId", "SikkerhedsEftersynId" },
                keyValues: new object[] { 1, 2 });

            migrationBuilder.DeleteData(
                table: "EftersynsRegler",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "MaterialeType",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "MaterialeType",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Påmindelser",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Påmindelser",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServiceOpgaveTypeLinks",
                keyColumns: new[] { "OpgaveTypeListeId", "ServiceId" },
                keyValues: new object[] { 1, 1 });

            migrationBuilder.DeleteData(
                table: "ServiceOpgaveTypeLinks",
                keyColumns: new[] { "OpgaveTypeListeId", "ServiceId" },
                keyValues: new object[] { 2, 1 });

            migrationBuilder.DeleteData(
                table: "EftersynsRegler",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OpgaveTyper",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "OpgaveTyper",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Services",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "SikkerhedsEftersyn",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ServiceOpgaver",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ServiceOpgaver",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Maskiner",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Maskiner",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Medarbejdere",
                keyColumn: "Id",
                keyValue: "M1");

            migrationBuilder.DeleteData(
                table: "ServiceTeknikkere",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kunder",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Kunder",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "AktivSessionID",
                table: "Medarbejdere");
        }
    }
}
