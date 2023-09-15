using Microsoft.EntityFrameworkCore.Migrations;

namespace Infra.Migrations
{
    public partial class Initial_04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Descricao",
                table: "TBL_Estabelecimento",
                newName: "Descricao 4");

            migrationBuilder.AddColumn<string>(
                name: "Descricao 0",
                table: "TBL_Estabelecimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao 1",
                table: "TBL_Estabelecimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao 2",
                table: "TBL_Estabelecimento",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Descricao 3",
                table: "TBL_Estabelecimento",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Descricao 0",
                table: "TBL_Estabelecimento");

            migrationBuilder.DropColumn(
                name: "Descricao 1",
                table: "TBL_Estabelecimento");

            migrationBuilder.DropColumn(
                name: "Descricao 2",
                table: "TBL_Estabelecimento");

            migrationBuilder.DropColumn(
                name: "Descricao 3",
                table: "TBL_Estabelecimento");

            migrationBuilder.RenameColumn(
                name: "Descricao 4",
                table: "TBL_Estabelecimento",
                newName: "Descricao");
        }
    }
}
