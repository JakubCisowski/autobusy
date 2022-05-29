using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Autobusy.Logic.Migrations
{
    public partial class FixAutobusAndKierowcaNullable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Przejazdy_Autobusy_AutobusId",
                table: "Przejazdy");

            migrationBuilder.DropForeignKey(
                name: "FK_Przejazdy_Kierowcy_KierowcaId",
                table: "Przejazdy");

            migrationBuilder.AlterColumn<int>(
                name: "KierowcaId",
                table: "Przejazdy",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "AutobusId",
                table: "Przejazdy",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Przejazdy_Autobusy_AutobusId",
                table: "Przejazdy",
                column: "AutobusId",
                principalTable: "Autobusy",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Przejazdy_Kierowcy_KierowcaId",
                table: "Przejazdy",
                column: "KierowcaId",
                principalTable: "Kierowcy",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Przejazdy_Autobusy_AutobusId",
                table: "Przejazdy");

            migrationBuilder.DropForeignKey(
                name: "FK_Przejazdy_Kierowcy_KierowcaId",
                table: "Przejazdy");

            migrationBuilder.AlterColumn<int>(
                name: "KierowcaId",
                table: "Przejazdy",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "AutobusId",
                table: "Przejazdy",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Przejazdy_Autobusy_AutobusId",
                table: "Przejazdy",
                column: "AutobusId",
                principalTable: "Autobusy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Przejazdy_Kierowcy_KierowcaId",
                table: "Przejazdy",
                column: "KierowcaId",
                principalTable: "Kierowcy",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
