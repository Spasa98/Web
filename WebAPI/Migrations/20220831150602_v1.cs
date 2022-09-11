using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebAPI.Migrations
{
    public partial class v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Kola",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    rbrKola = table.Column<int>(type: "int", nullable: false),
                    pocetakKola = table.Column<DateTime>(type: "datetime2", nullable: false),
                    krajkKola = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kola", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Stadioni",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StadionName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stadioni", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Ekipe",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ImeEkipe = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Bodovi = table.Column<int>(type: "int", nullable: false),
                    putanjaSlike = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EkipaStadionID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ekipe", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Ekipe_Stadioni_EkipaStadionID",
                        column: x => x.EkipaStadionID,
                        principalTable: "Stadioni",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Domacin",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EkipaDomacinID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domacin", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Domacin_Ekipe_EkipaDomacinID",
                        column: x => x.EkipaDomacinID,
                        principalTable: "Ekipe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Gost",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EkipaGostID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gost", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Gost_Ekipe_EkipaGostID",
                        column: x => x.EkipaGostID,
                        principalTable: "Ekipe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Igraci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Prezime = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Pozicija = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    BrojNaDresu = table.Column<int>(type: "int", nullable: false),
                    IgracEkipeID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Igraci", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Igraci_Ekipe_IgracEkipeID",
                        column: x => x.IgracEkipeID,
                        principalTable: "Ekipe",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Utakmice",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DatumIVremeUtakmice = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Pobednik = table.Column<int>(type: "int", nullable: false),
                    brGolovaDomacin = table.Column<int>(type: "int", nullable: false),
                    brGolovaGost = table.Column<int>(type: "int", nullable: false),
                    DomacaEkipaID = table.Column<int>(type: "int", nullable: true),
                    GostujucaEkipaID = table.Column<int>(type: "int", nullable: true),
                    UtakmicaKoloID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Utakmice", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Utakmice_Domacin_DomacaEkipaID",
                        column: x => x.DomacaEkipaID,
                        principalTable: "Domacin",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Utakmice_Gost_GostujucaEkipaID",
                        column: x => x.GostujucaEkipaID,
                        principalTable: "Gost",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Utakmice_Kola_UtakmicaKoloID",
                        column: x => x.UtakmicaKoloID,
                        principalTable: "Kola",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Strelci",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    minutGola = table.Column<int>(type: "int", nullable: false),
                    StrelacUtakmiceID = table.Column<int>(type: "int", nullable: true),
                    StrelacIgracID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Strelci", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Strelci_Igraci_StrelacIgracID",
                        column: x => x.StrelacIgracID,
                        principalTable: "Igraci",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Strelci_Utakmice_StrelacUtakmiceID",
                        column: x => x.StrelacUtakmiceID,
                        principalTable: "Utakmice",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Domacin_EkipaDomacinID",
                table: "Domacin",
                column: "EkipaDomacinID");

            migrationBuilder.CreateIndex(
                name: "IX_Ekipe_EkipaStadionID",
                table: "Ekipe",
                column: "EkipaStadionID");

            migrationBuilder.CreateIndex(
                name: "IX_Gost_EkipaGostID",
                table: "Gost",
                column: "EkipaGostID");

            migrationBuilder.CreateIndex(
                name: "IX_Igraci_IgracEkipeID",
                table: "Igraci",
                column: "IgracEkipeID");

            migrationBuilder.CreateIndex(
                name: "IX_Strelci_StrelacIgracID",
                table: "Strelci",
                column: "StrelacIgracID");

            migrationBuilder.CreateIndex(
                name: "IX_Strelci_StrelacUtakmiceID",
                table: "Strelci",
                column: "StrelacUtakmiceID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmice_DomacaEkipaID",
                table: "Utakmice",
                column: "DomacaEkipaID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmice_GostujucaEkipaID",
                table: "Utakmice",
                column: "GostujucaEkipaID");

            migrationBuilder.CreateIndex(
                name: "IX_Utakmice_UtakmicaKoloID",
                table: "Utakmice",
                column: "UtakmicaKoloID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Strelci");

            migrationBuilder.DropTable(
                name: "Igraci");

            migrationBuilder.DropTable(
                name: "Utakmice");

            migrationBuilder.DropTable(
                name: "Domacin");

            migrationBuilder.DropTable(
                name: "Gost");

            migrationBuilder.DropTable(
                name: "Kola");

            migrationBuilder.DropTable(
                name: "Ekipe");

            migrationBuilder.DropTable(
                name: "Stadioni");
        }
    }
}
