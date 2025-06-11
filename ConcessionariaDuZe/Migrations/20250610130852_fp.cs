using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ConcessionariaDuZe.Migrations
{
    /// <inheritdoc />
    public partial class fp : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_FormaDePagamento_FormaDePagamentoId",
                table: "Usuario");

            migrationBuilder.AlterColumn<Guid>(
                name: "FormaDePagamentoId",
                table: "Usuario",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_FormaDePagamento_FormaDePagamentoId",
                table: "Usuario",
                column: "FormaDePagamentoId",
                principalTable: "FormaDePagamento",
                principalColumn: "FormaDePagamentoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Usuario_FormaDePagamento_FormaDePagamentoId",
                table: "Usuario");

            migrationBuilder.AlterColumn<Guid>(
                name: "FormaDePagamentoId",
                table: "Usuario",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Usuario_FormaDePagamento_FormaDePagamentoId",
                table: "Usuario",
                column: "FormaDePagamentoId",
                principalTable: "FormaDePagamento",
                principalColumn: "FormaDePagamentoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
