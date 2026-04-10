using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavingBack.Database.Migrations
{
    /// <inheritdoc />
    public partial class AñadiendoLogicaEnAhorro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TipoAhorro",
                table: "Ahorro",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "Efectivo");

            migrationBuilder.InsertData(
                table: "CategoriaGasto",
                columns: new[] { "Id", "Nombre" },
                values: new object[] { 13, "Movimiento Interno" });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Ahorro_TipoAhorro",
                table: "Ahorro",
                sql: "[TipoAhorro] IN ('Efectivo', 'Nequi', 'Banco')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Ahorro_Tipo",
                table: "Ahorro");

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 13);

            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Ahorro");

        }
    }
}
