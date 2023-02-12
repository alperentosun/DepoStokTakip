using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepoStokProgrami.Migrations
{
    /// <inheritdoc />
    public partial class Guncelleme52 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UrunSatin",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UrunId = table.Column<int>(type: "int", nullable: false),
                    ToplamFiyatId = table.Column<double>(type: "float", nullable: false),
                    AdetId = table.Column<int>(type: "int", nullable: false),
                    MusteriAdi = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UrunNotu = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Tarih = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UrunSatin", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrunSatin_Urun_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrunSatin_UrunId",
                table: "UrunSatin",
                column: "UrunId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UrunSatin");
        }
    }
}
