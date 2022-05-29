using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autobusy.Logic.Migrations
{
    public partial class TryFixingCyclicIssue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Autobusy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Marka = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NumerRejestracyjny = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataProdukcji = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StanAutobusu = table.Column<int>(type: "int", nullable: false),
                    SpalanieNa100 = table.Column<double>(type: "float(4)", precision: 4, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Autobusy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Kierowcy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Imie = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nazwisko = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Doswiadczenie = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kierowcy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Linie",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Typ = table.Column<int>(type: "int", nullable: false),
                    DlugoscWKm = table.Column<double>(type: "float(4)", precision: 4, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Linie", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Przystanki",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Numer = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Nazwa = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Adres = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przystanki", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Serwisy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Typ = table.Column<int>(type: "int", nullable: false),
                    Cena = table.Column<decimal>(type: "decimal(10,2)", precision: 10, scale: 2, nullable: false),
                    Opis = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AutobusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serwisy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Serwisy_Autobusy_AutobusId",
                        column: x => x.AutobusId,
                        principalTable: "Autobusy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Kursy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DzienTygodnia = table.Column<int>(type: "int", nullable: false),
                    GodzinaRozpoczecia = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LiniaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kursy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Kursy_Linie_LiniaId",
                        column: x => x.LiniaId,
                        principalTable: "Linie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PrzystankiWLinii",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LiczbaPorzadkowa = table.Column<int>(type: "int", nullable: false),
                    PrzystanekId = table.Column<int>(type: "int", nullable: false),
                    LiniaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrzystankiWLinii", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PrzystankiWLinii_Linie_LiniaId",
                        column: x => x.LiniaId,
                        principalTable: "Linie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PrzystankiWLinii_Przystanki_PrzystanekId",
                        column: x => x.PrzystanekId,
                        principalTable: "Przystanki",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Przejazdy",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IloscSpalonegoPaliwa = table.Column<double>(type: "float(4)", precision: 4, scale: 2, nullable: false),
                    IloscSkasowanychBiletow = table.Column<int>(type: "int", nullable: false),
                    Data = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KursId = table.Column<int>(type: "int", nullable: false),
                    KierowcaId = table.Column<int>(type: "int", nullable: false),
                    AutobusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Przejazdy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Przejazdy_Autobusy_AutobusId",
                        column: x => x.AutobusId,
                        principalTable: "Autobusy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Przejazdy_Kierowcy_KierowcaId",
                        column: x => x.KierowcaId,
                        principalTable: "Kierowcy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Przejazdy_Kursy_KursId",
                        column: x => x.KursId,
                        principalTable: "Kursy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PlanyKursu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanowaGodzina = table.Column<DateTime>(type: "datetime2", nullable: false),
                    KursId = table.Column<int>(type: "int", nullable: false),
                    PrzystanekWLiniiId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanyKursu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PlanyKursu_Kursy_KursId",
                        column: x => x.KursId,
                        principalTable: "Kursy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PlanyKursu_PrzystankiWLinii_PrzystanekWLiniiId",
                        column: x => x.PrzystanekWLiniiId,
                        principalTable: "PrzystankiWLinii",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "RealizacjePrzejazdu",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaktycznaGodzina = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PlanKursuId = table.Column<int>(type: "int", nullable: false),
                    PrzejazdId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RealizacjePrzejazdu", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RealizacjePrzejazdu_PlanyKursu_PlanKursuId",
                        column: x => x.PlanKursuId,
                        principalTable: "PlanyKursu",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RealizacjePrzejazdu_Przejazdy_PrzejazdId",
                        column: x => x.PrzejazdId,
                        principalTable: "Przejazdy",
                        principalColumn: "Id");
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
                name: "IX_PlanyKursu_PrzystanekWLiniiId",
                table: "PlanyKursu",
                column: "PrzystanekWLiniiId");

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
                name: "IX_RealizacjePrzejazdu_PlanKursuId",
                table: "RealizacjePrzejazdu",
                column: "PlanKursuId");

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
                name: "RealizacjePrzejazdu");

            migrationBuilder.DropTable(
                name: "Serwisy");

            migrationBuilder.DropTable(
                name: "PlanyKursu");

            migrationBuilder.DropTable(
                name: "Przejazdy");

            migrationBuilder.DropTable(
                name: "PrzystankiWLinii");

            migrationBuilder.DropTable(
                name: "Autobusy");

            migrationBuilder.DropTable(
                name: "Kierowcy");

            migrationBuilder.DropTable(
                name: "Kursy");

            migrationBuilder.DropTable(
                name: "Przystanki");

            migrationBuilder.DropTable(
                name: "Linie");
        }
    }
}
