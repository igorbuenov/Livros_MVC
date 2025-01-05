using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Migrations.Operations;

#nullable disable

namespace LivrosMVC.Migrations
{
    public partial class RenameDataUltimaAtualizacaoToDataEmprestimo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataUltimaAtualizacao",
                table: "Emprestimos",
                newName: "DataEmprestimo"
            );
        }


        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataEmprestimo",
                table: "Emprestimos",
                newName: "DataUltimaAtualizacao"
            );
        }
    }
}
