using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data_Access_Layer.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EftersynsRegler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Regel = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EftersynsRegler", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kunder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirmaNavn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktPersonNavn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KontaktPersonTelefonnummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailAdresse = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ErAktiv = table.Column<bool>(type: "bit", nullable: false),
                    CvrNummer = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kunder", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialeLister",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialeLister", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MaterialeType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialeBeskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialeType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medarbejdere",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    MedarbejderNavn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    KodeOrdHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MailAdresse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medarbejdere", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpgaveTyper",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OpgaveBeskrivelse = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpgaveTyper", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ServiceTeknikkere",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeknikkerNavn = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TelefonNummer = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceTeknikkere", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Maskiner",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    KundeId = table.Column<int>(type: "int", nullable: true),
                    SerieNummer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Producent = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaskineType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maskiner", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maskiner_Kunder_KundeId",
                        column: x => x.KundeId,
                        principalTable: "Kunder",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "MaterialeLinjer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Antal = table.Column<int>(type: "int", nullable: false),
                    Information = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MaterialeTypeId = table.Column<int>(type: "int", nullable: true),
                    MatrialeTypeId = table.Column<int>(type: "int", nullable: true),
                    MaterialeListeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaterialeLinjer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MaterialeLinjer_MaterialeLister_MaterialeListeId",
                        column: x => x.MaterialeListeId,
                        principalTable: "MaterialeLister",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_MaterialeLinjer_MaterialeType_MaterialeTypeId",
                        column: x => x.MaterialeTypeId,
                        principalTable: "MaterialeType",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ServiceOpgaver",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaskineId = table.Column<int>(type: "int", nullable: true),
                    SidstUdførtDato = table.Column<DateOnly>(type: "date", nullable: true),
                    Deadline = table.Column<DateOnly>(type: "date", nullable: false),
                    SidstUdførtNote = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ServiceInterval = table.Column<int>(type: "int", nullable: false),
                    MedarbejderId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ServiceTeknikkerId = table.Column<int>(type: "int", nullable: true),
                    MaterialeListeId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOpgaver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceOpgaver_Maskiner_MaskineId",
                        column: x => x.MaskineId,
                        principalTable: "Maskiner",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceOpgaver_MaterialeLister_MaterialeListeId",
                        column: x => x.MaterialeListeId,
                        principalTable: "MaterialeLister",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceOpgaver_Medarbejdere_MedarbejderId",
                        column: x => x.MedarbejderId,
                        principalTable: "Medarbejdere",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ServiceOpgaver_ServiceTeknikkere_ServiceTeknikkerId",
                        column: x => x.ServiceTeknikkerId,
                        principalTable: "ServiceTeknikkere",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AfsluttedeServices",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UdførtDato = table.Column<DateOnly>(type: "date", nullable: false),
                    ServiceOpgaveId = table.Column<int>(type: "int", nullable: true),
                    MaskineId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AfsluttedeServices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AfsluttedeServices_Maskiner_MaskineId",
                        column: x => x.MaskineId,
                        principalTable: "Maskiner",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AfsluttedeServices_ServiceOpgaver_ServiceOpgaveId",
                        column: x => x.ServiceOpgaveId,
                        principalTable: "ServiceOpgaver",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Påmindelser",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PåmindelsesDato = table.Column<DateOnly>(type: "date", nullable: false),
                    ServiceOpgaveId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Påmindelser", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Påmindelser_ServiceOpgaver_ServiceOpgaveId",
                        column: x => x.ServiceOpgaveId,
                        principalTable: "ServiceOpgaver",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    Servicetype = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_ServiceOpgaver_Id",
                        column: x => x.Id,
                        principalTable: "ServiceOpgaver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SikkerhedsEftersyn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SikkerhedsEftersyn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SikkerhedsEftersyn_ServiceOpgaver_Id",
                        column: x => x.Id,
                        principalTable: "ServiceOpgaver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceOpgaveTypeLinks",
                columns: table => new
                {
                    OpgaveTypeListeId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceOpgaveTypeLinks", x => new { x.OpgaveTypeListeId, x.ServiceId });
                    table.ForeignKey(
                        name: "FK_ServiceOpgaveTypeLinks_OpgaveTyper_OpgaveTypeListeId",
                        column: x => x.OpgaveTypeListeId,
                        principalTable: "OpgaveTyper",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceOpgaveTypeLinks_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EftersynsRegelLinks",
                columns: table => new
                {
                    EftersynsRegelListeId = table.Column<int>(type: "int", nullable: false),
                    SikkerhedsEftersynId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EftersynsRegelLinks", x => new { x.EftersynsRegelListeId, x.SikkerhedsEftersynId });
                    table.ForeignKey(
                        name: "FK_EftersynsRegelLinks_EftersynsRegler_EftersynsRegelListeId",
                        column: x => x.EftersynsRegelListeId,
                        principalTable: "EftersynsRegler",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EftersynsRegelLinks_SikkerhedsEftersyn_SikkerhedsEftersynId",
                        column: x => x.SikkerhedsEftersynId,
                        principalTable: "SikkerhedsEftersyn",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AfsluttedeServices_MaskineId",
                table: "AfsluttedeServices",
                column: "MaskineId");

            migrationBuilder.CreateIndex(
                name: "IX_AfsluttedeServices_ServiceOpgaveId",
                table: "AfsluttedeServices",
                column: "ServiceOpgaveId");

            migrationBuilder.CreateIndex(
                name: "IX_EftersynsRegelLinks_SikkerhedsEftersynId",
                table: "EftersynsRegelLinks",
                column: "SikkerhedsEftersynId");

            migrationBuilder.CreateIndex(
                name: "IX_Maskiner_KundeId",
                table: "Maskiner",
                column: "KundeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialeLinjer_MaterialeListeId",
                table: "MaterialeLinjer",
                column: "MaterialeListeId");

            migrationBuilder.CreateIndex(
                name: "IX_MaterialeLinjer_MaterialeTypeId",
                table: "MaterialeLinjer",
                column: "MaterialeTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Påmindelser_ServiceOpgaveId",
                table: "Påmindelser",
                column: "ServiceOpgaveId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOpgaver_MaskineId",
                table: "ServiceOpgaver",
                column: "MaskineId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOpgaver_MaterialeListeId",
                table: "ServiceOpgaver",
                column: "MaterialeListeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOpgaver_MedarbejderId",
                table: "ServiceOpgaver",
                column: "MedarbejderId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOpgaver_ServiceTeknikkerId",
                table: "ServiceOpgaver",
                column: "ServiceTeknikkerId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceOpgaveTypeLinks_ServiceId",
                table: "ServiceOpgaveTypeLinks",
                column: "ServiceId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AfsluttedeServices");

            migrationBuilder.DropTable(
                name: "EftersynsRegelLinks");

            migrationBuilder.DropTable(
                name: "MaterialeLinjer");

            migrationBuilder.DropTable(
                name: "Påmindelser");

            migrationBuilder.DropTable(
                name: "ServiceOpgaveTypeLinks");

            migrationBuilder.DropTable(
                name: "EftersynsRegler");

            migrationBuilder.DropTable(
                name: "SikkerhedsEftersyn");

            migrationBuilder.DropTable(
                name: "MaterialeType");

            migrationBuilder.DropTable(
                name: "OpgaveTyper");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DropTable(
                name: "ServiceOpgaver");

            migrationBuilder.DropTable(
                name: "Maskiner");

            migrationBuilder.DropTable(
                name: "MaterialeLister");

            migrationBuilder.DropTable(
                name: "Medarbejdere");

            migrationBuilder.DropTable(
                name: "ServiceTeknikkere");

            migrationBuilder.DropTable(
                name: "Kunder");
        }
    }
}
