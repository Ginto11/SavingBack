using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavingBack.Database.Migrations
{
    /// <inheritdoc />
    public partial class AñadiendoOpcionNube : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Ingreso_Tipo",
                table: "Ingreso");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Egreso_Tipo",
                table: "Egreso");


            migrationBuilder.AddCheckConstraint(
                name: "CK_Ingreso_Tipo",
                table: "Ingreso",
                sql: "[Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco', 'Nube')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Egreso_Tipo",
                table: "Egreso",
                sql: "[Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco', 'Nube')");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Ingreso_Tipo",
                table: "Ingreso");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Egreso_Tipo",
                table: "Egreso");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Ingreso_Tipo",
                table: "Ingreso",
                sql: "[Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Egreso_Tipo",
                table: "Egreso",
                sql: "[Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco')");
        }
    }
}
