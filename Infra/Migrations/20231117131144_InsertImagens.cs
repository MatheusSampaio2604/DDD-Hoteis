using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class InsertImagens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fotos",
                table: "TBL_Acomodacao");

            migrationBuilder.CreateTable(
                name: "TBL_Imagens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdAcomodacao = table.Column<int>(type: "int", nullable: true),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CaminhoImagem = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBL_Imagens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBL_Imagens_TBL_Acomodacao_IdAcomodacao",
                        column: x => x.IdAcomodacao,
                        principalTable: "TBL_Acomodacao",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBL_Imagens_IdAcomodacao",
                table: "TBL_Imagens",
                column: "IdAcomodacao");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_Imagens");

            migrationBuilder.AddColumn<string>(
                name: "Fotos",
                table: "TBL_Acomodacao",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
