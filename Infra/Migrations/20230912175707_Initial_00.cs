using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class Initial_00 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBL_Pousada",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Pousada", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Tarifas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Tarifas", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Acomodacao",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IdTarifas = table.Column<int>(type: "int", nullable: false),
                    IdHome = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Acomodacao", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Acomodacao_TBL_Pousada_IdHome",
                        column: x => x.IdHome,
                        principalTable: "TBL_Pousada",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TBL_Acomodacao_TBL_Tarifas_IdTarifas",
                        column: x => x.IdTarifas,
                        principalTable: "TBL_Tarifas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TBL_Fotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IdAcomodacao = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Fotos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Fotos_TBL_Acomodacao_IdAcomodacao",
                        column: x => x.IdAcomodacao,
                        principalTable: "TBL_Acomodacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Acomodacao_IdHome",
                table: "TBL_Acomodacao",
                column: "IdHome");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Acomodacao_IdTarifas",
                table: "TBL_Acomodacao",
                column: "IdTarifas");

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Fotos_IdAcomodacao",
                table: "TBL_Fotos",
                column: "IdAcomodacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_Fotos");

            migrationBuilder.DropTable(
                name: "TBL_Acomodacao");

            migrationBuilder.DropTable(
                name: "TBL_Pousada");

            migrationBuilder.DropTable(
                name: "TBL_Tarifas");
        }
    }
}
