using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DepoStokProgrami.Migrations
{
    /// <inheritdoc />
    public partial class Guncelleme4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urunler_UrunKategoriler_KategoriId",
                table: "Urunler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Urunler",
                table: "Urunler");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UrunKategoriler",
                table: "UrunKategoriler");

            migrationBuilder.RenameTable(
                name: "Urunler",
                newName: "Urun");

            migrationBuilder.RenameTable(
                name: "UrunKategoriler",
                newName: "UrunKategori");

            migrationBuilder.RenameIndex(
                name: "IX_Urunler_KategoriId",
                table: "Urun",
                newName: "IX_Urun_KategoriId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Urun",
                table: "Urun",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UrunKategori",
                table: "UrunKategori",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "UrunSatis",
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
                    table.PrimaryKey("PK_UrunSatis", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UrunSatis_Urun_UrunId",
                        column: x => x.UrunId,
                        principalTable: "Urun",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UrunSatis_UrunId",
                table: "UrunSatis",
                column: "UrunId");

            migrationBuilder.AddForeignKey(
                name: "FK_Urun_UrunKategori_KategoriId",
                table: "Urun",
                column: "KategoriId",
                principalTable: "UrunKategori",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Urun_UrunKategori_KategoriId",
                table: "Urun");

            migrationBuilder.DropTable(
                name: "UrunSatis");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UrunKategori",
                table: "UrunKategori");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Urun",
                table: "Urun");

            migrationBuilder.RenameTable(
                name: "UrunKategori",
                newName: "UrunKategoriler");

            migrationBuilder.RenameTable(
                name: "Urun",
                newName: "Urunler");

            migrationBuilder.RenameIndex(
                name: "IX_Urun_KategoriId",
                table: "Urunler",
                newName: "IX_Urunler_KategoriId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UrunKategoriler",
                table: "UrunKategoriler",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Urunler",
                table: "Urunler",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Urunler_UrunKategoriler_KategoriId",
                table: "Urunler",
                column: "KategoriId",
                principalTable: "UrunKategoriler",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
