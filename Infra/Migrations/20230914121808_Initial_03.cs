using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class Initial_03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBL_Fotos");

            migrationBuilder.AddColumn<string>(
                name: "Fotos",
                table: "TBL_Acomodacao",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Fotos",
                table: "TBL_Acomodacao");

            migrationBuilder.CreateTable(
                name: "TBL_Fotos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Ativo = table.Column<bool>(type: "bit", nullable: false, defaultValue: false),
                    IdAcomodacao = table.Column<int>(type: "int", nullable: false),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
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
                name: "IX_TBL_Fotos_IdAcomodacao",
                table: "TBL_Fotos",
                column: "IdAcomodacao");
        }
    }
}
