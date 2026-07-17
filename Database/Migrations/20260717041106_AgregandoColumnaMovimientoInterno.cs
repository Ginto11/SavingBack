using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavingBack.Database.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoColumnaMovimientoInterno : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<bool>(
                name: "MovimientoInterno",
                table: "Ingreso",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2026, 7, 16, 23, 11, 5, 318, DateTimeKind.Local).AddTicks(6045));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2026, 7, 16, 23, 11, 5, 318, DateTimeKind.Local).AddTicks(6050));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2026, 7, 16, 23, 11, 5, 318, DateTimeKind.Local).AddTicks(6053));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2026, 7, 16, 23, 11, 5, 318, DateTimeKind.Local).AddTicks(6056));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaRegistro",
                value: new DateTime(2026, 7, 16, 23, 11, 5, 318, DateTimeKind.Local).AddTicks(6058));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "FechaRegistro", "MovimientoInterno" },
                values: new object[] { new DateTime(2026, 7, 16, 23, 11, 5, 318, DateTimeKind.Local).AddTicks(5544), false });

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "FechaRegistro", "MovimientoInterno" },
                values: new object[] { new DateTime(2026, 7, 16, 23, 11, 5, 318, DateTimeKind.Local).AddTicks(5548), false });

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "FechaRegistro", "MovimientoInterno" },
                values: new object[] { new DateTime(2026, 7, 16, 23, 11, 5, 318, DateTimeKind.Local).AddTicks(5551), false });

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "FechaRegistro", "MovimientoInterno" },
                values: new object[] { new DateTime(2026, 7, 16, 23, 11, 5, 318, DateTimeKind.Local).AddTicks(5553), false });

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "FechaRegistro", "MovimientoInterno" },
                values: new object[] { new DateTime(2026, 7, 16, 23, 11, 5, 318, DateTimeKind.Local).AddTicks(5556), false });

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "MovimientoInterno",
                table: "Ingreso");

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 10, 16, 35, 28, 899, DateTimeKind.Local).AddTicks(5668));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 10, 16, 35, 28, 899, DateTimeKind.Local).AddTicks(5674));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 10, 16, 35, 28, 899, DateTimeKind.Local).AddTicks(5678));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 10, 16, 35, 28, 899, DateTimeKind.Local).AddTicks(5682));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 10, 16, 35, 28, 899, DateTimeKind.Local).AddTicks(5686));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 10, 16, 35, 28, 899, DateTimeKind.Local).AddTicks(5040));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 10, 16, 35, 28, 899, DateTimeKind.Local).AddTicks(5044));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 10, 16, 35, 28, 899, DateTimeKind.Local).AddTicks(5048));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 10, 16, 35, 28, 899, DateTimeKind.Local).AddTicks(5052));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 5,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 10, 16, 35, 28, 899, DateTimeKind.Local).AddTicks(5055));

        }
    }
}
