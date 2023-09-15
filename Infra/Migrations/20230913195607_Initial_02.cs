using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class Initial_02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Acomodacao_TBL_Pousada_IdHome",
                table: "TBL_Acomodacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBL_Pousada",
                table: "TBL_Pousada");

            migrationBuilder.RenameTable(
                name: "TBL_Pousada",
                newName: "TBL_Estabelecimento");

            migrationBuilder.AddColumn<string>(
                name: "Nome",
                table: "TBL_Estabelecimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBL_Estabelecimento",
                table: "TBL_Estabelecimento",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Acomodacao_TBL_Estabelecimento_IdHome",
                table: "TBL_Acomodacao",
                column: "IdHome",
                principalTable: "TBL_Estabelecimento",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TBL_Acomodacao_TBL_Estabelecimento_IdHome",
                table: "TBL_Acomodacao");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TBL_Estabelecimento",
                table: "TBL_Estabelecimento");

            migrationBuilder.DropColumn(
                name: "Nome",
                table: "TBL_Estabelecimento");

            migrationBuilder.RenameTable(
                name: "TBL_Estabelecimento",
                newName: "TBL_Pousada");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TBL_Pousada",
                table: "TBL_Pousada",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TBL_Acomodacao_TBL_Pousada_IdHome",
                table: "TBL_Acomodacao",
                column: "IdHome",
                principalTable: "TBL_Pousada",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
