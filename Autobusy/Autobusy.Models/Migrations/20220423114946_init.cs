using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autobusy.Models.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autobusy",
                columns: table => new
                {
                    AutobusId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NumerRejestracyjny = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataProdukcji = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StanAutobusu = table.Column<int>(type: "int", nullable: false),
                    SpalanieNa100 = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autobusy", x => x.AutobusId);
                });

            migrationBuilder.CreateTable(
                name: "Kierowcy",
                columns: table => new
                {
                    KierowcaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Doswiadczenie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kierowcy", x => x.KierowcaId);
                });

            migrationBuilder.CreateTable(
                name: "Linie",
                columns: table => new
                {
                    LiniaId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Typ = table.Column<int>(type: "int", nullable: false),
                    DlugoscWKm = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linie", x => x.LiniaId);
                });

            migrationBuilder.CreateTable(
                name: "Przystanki",
                columns: table => new
                {
                    PrzystanekId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numer = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przystanki", x => x.PrzystanekId);
                });

            migrationBuilder.CreateTable(
                name: "Serwisy",
                columns: table => new
                {
                    SerwisId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Typ = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<double>(type: "float", nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AutobusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serwisy", x => x.SerwisId);
                    table.ForeignKey(
                        name: "FK_Serwisy_Autobusy_AutobusId",
                        column: x => x.AutobusId,
                        principalTable: "Autobusy",
                        principalColumn: "AutobusId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kursy",
                columns: table => new
                {
                    KursId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DzienOdbycia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LiniaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kursy", x => x.KursId);
                    table.ForeignKey(
                        name: "FK_Kursy_Linie_LiniaId",
                        column: x => x.LiniaId,
                        principalTable: "Linie",
                        principalColumn: "LiniaId");
                });

            migrationBuilder.CreateTable(
                name: "PrzystankiWLinii",
                columns: table => new
                {
                    PrzystanekWLiniiId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrzystanekId = table.Column<int>(type: "int", nullable: false),
                    LiniaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzystankiWLinii", x => x.PrzystanekWLiniiId);
                    table.ForeignKey(
                        name: "FK_PrzystankiWLinii_Linie_LiniaId",
                        column: x => x.LiniaId,
                        principalTable: "Linie",
                        principalColumn: "LiniaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrzystankiWLinii_Przystanki_PrzystanekId",
                        column: x => x.PrzystanekId,
                        principalTable: "Przystanki",
                        principalColumn: "PrzystanekId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Przejazdy",
                columns: table => new
                {
                    PrzejazdId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IloscSpalonegoPaliwa = table.Column<double>(type: "float", nullable: false),
                    IloscSkasowanychBiletow = table.Column<int>(type: "int", nullable: false),
                    KursId = table.Column<int>(type: "int", nullable: false),
                    KierowcaId = table.Column<int>(type: "int", nullable: false),
                    AutobusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przejazdy", x => x.PrzejazdId);
                    table.ForeignKey(
                        name: "FK_Przejazdy_Autobusy_AutobusId",
                        column: x => x.AutobusId,
                        principalTable: "Autobusy",
                        principalColumn: "AutobusId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Przejazdy_Kierowcy_KierowcaId",
                        column: x => x.KierowcaId,
                        principalTable: "Kierowcy",
                        principalColumn: "KierowcaId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Przejazdy_Kursy_KursId",
                        column: x => x.KursId,
                        principalTable: "Kursy",
                        principalColumn: "KursId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RealizacjePrzejazdu",
                columns: table => new
                {
                    RealizacjaPrzejazduId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaktycznaGodzina = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanKursuId = table.Column<int>(type: "int", nullable: false),
                    PrzejazdId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealizacjePrzejazdu", x => x.RealizacjaPrzejazduId);
                    table.ForeignKey(
                        name: "FK_RealizacjePrzejazdu_Przejazdy_PrzejazdId",
                        column: x => x.PrzejazdId,
                        principalTable: "Przejazdy",
                        principalColumn: "PrzejazdId");
                });

            migrationBuilder.CreateTable(
                name: "PlanyKursu",
                columns: table => new
                {
                    PlanKursuId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanowaGodzina = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KursId = table.Column<int>(type: "int", nullable: false),
                    PrzystanekId = table.Column<int>(type: "int", nullable: false),
                    RealizacjaPrzejazduId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanyKursu", x => x.PlanKursuId);
                    table.ForeignKey(
                        name: "FK_PlanyKursu_Kursy_KursId",
                        column: x => x.KursId,
                        principalTable: "Kursy",
                        principalColumn: "KursId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanyKursu_Przystanki_PrzystanekId",
                        column: x => x.PrzystanekId,
                        principalTable: "Przystanki",
                        principalColumn: "PrzystanekId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanyKursu_RealizacjePrzejazdu_RealizacjaPrzejazduId",
                        column: x => x.RealizacjaPrzejazduId,
                        principalTable: "RealizacjePrzejazdu",
                        principalColumn: "RealizacjaPrzejazduId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Kursy_LiniaId",
                table: "Kursy",
                column: "LiniaId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanyKursu_KursId",
                table: "PlanyKursu",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanyKursu_PrzystanekId",
                table: "PlanyKursu",
                column: "PrzystanekId");

            migrationBuilder.CreateIndex(
                name: "IX_PlanyKursu_RealizacjaPrzejazduId",
                table: "PlanyKursu",
                column: "RealizacjaPrzejazduId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Przejazdy_AutobusId",
                table: "Przejazdy",
                column: "AutobusId");

            migrationBuilder.CreateIndex(
                name: "IX_Przejazdy_KierowcaId",
                table: "Przejazdy",
                column: "KierowcaId");

            migrationBuilder.CreateIndex(
                name: "IX_Przejazdy_KursId",
                table: "Przejazdy",
                column: "KursId");

            migrationBuilder.CreateIndex(
                name: "IX_PrzystankiWLinii_LiniaId",
                table: "PrzystankiWLinii",
                column: "LiniaId");

            migrationBuilder.CreateIndex(
                name: "IX_PrzystankiWLinii_PrzystanekId",
                table: "PrzystankiWLinii",
                column: "PrzystanekId");

            migrationBuilder.CreateIndex(
                name: "IX_RealizacjePrzejazdu_PrzejazdId",
                table: "RealizacjePrzejazdu",
                column: "PrzejazdId");

            migrationBuilder.CreateIndex(
                name: "IX_Serwisy_AutobusId",
                table: "Serwisy",
                column: "AutobusId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PlanyKursu");

            migrationBuilder.DropTable(
                name: "PrzystankiWLinii");

            migrationBuilder.DropTable(
                name: "Serwisy");

            migrationBuilder.DropTable(
                name: "RealizacjePrzejazdu");

            migrationBuilder.DropTable(
                name: "Przystanki");

            migrationBuilder.DropTable(
                name: "Przejazdy");

            migrationBuilder.DropTable(
                name: "Autobusy");

            migrationBuilder.DropTable(
                name: "Kierowcy");

            migrationBuilder.DropTable(
                name: "Kursy");

            migrationBuilder.DropTable(
                name: "Linie");
        }
    }
}
