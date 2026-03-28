using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavingBack.Database.Migrations
{
    /// <inheritdoc />
    public partial class ModificandoTablaEgresos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsuarioId",
                table: "Egreso",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaRegistro", "UsuarioId" },
                values: new object[] { new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(9394), 2 });

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaRegistro", "UsuarioId" },
                values: new object[] { new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(9397), 2 });

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaRegistro", "UsuarioId" },
                values: new object[] { new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(9399), 2 });

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaRegistro", "UsuarioId" },
                values: new object[] { new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(9402), 2 });

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(8996));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(8999));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(9002));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(9005));

            migrationBuilder.CreateIndex(
                name: "IX_Egreso_UsuarioId",
                table: "Egreso",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Egreso_Usuario_UsuarioId",
                table: "Egreso",
                column: "UsuarioId",
                principalTable: "Usuario",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Egreso_Usuario_UsuarioId",
                table: "Egreso");

            migrationBuilder.DropIndex(
                name: "IX_Egreso_UsuarioId",
                table: "Egreso");

            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "Egreso");

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 25, 23, 9, 26, 261, DateTimeKind.Local).AddTicks(230));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 25, 23, 9, 26, 261, DateTimeKind.Local).AddTicks(233));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 25, 23, 9, 26, 261, DateTimeKind.Local).AddTicks(235));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 25, 23, 9, 26, 261, DateTimeKind.Local).AddTicks(237));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 25, 23, 9, 26, 260, DateTimeKind.Local).AddTicks(9775));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 25, 23, 9, 26, 260, DateTimeKind.Local).AddTicks(9778));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 25, 23, 9, 26, 260, DateTimeKind.Local).AddTicks(9781));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 25, 23, 9, 26, 260, DateTimeKind.Local).AddTicks(9783));
        }
    }
}
