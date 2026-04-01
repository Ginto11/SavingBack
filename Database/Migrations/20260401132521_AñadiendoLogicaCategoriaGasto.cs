using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SavingBack.Database.Migrations
{
    /// <inheritdoc />
    public partial class AñadiendoLogicaCategoriaGasto : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ahorro_MetaAhorro_MetaAhorroId",
                table: "Ahorro");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Ingreso_Tipo",
                table: "Ingreso");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Egreso_Tipo",
                table: "Egreso");

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 1L);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 2L);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 3L);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 4L);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 5L);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 6L);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 7L);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 8L);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 9L);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 10L);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 11L);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 12L);

            migrationBuilder.AddColumn<int>(
                name: "CategoriaGastoId",
                table: "Egreso",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "CategoriaGasto",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "MetaAhorroId",
                table: "Ahorro",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "CategoriaGasto",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Alimentación" },
                    { 2, "Transporte" },
                    { 3, "Salud" },
                    { 4, "Hogar" },
                    { 5, "Servicios" },
                    { 6, "Educación" },
                    { 7, "Entretenimiento" },
                    { 8, "Mascotas" },
                    { 9, "Ropa" },
                    { 10, "Deudas" },
                    { 11, "Inversiones" },
                    { 12, "Otros" }
                });

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CategoriaGastoId", "FechaRegistro" },
                values: new object[] { 1, new DateTime(2026, 4, 1, 8, 25, 20, 901, DateTimeKind.Local).AddTicks(8471) });

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CategoriaGastoId", "FechaRegistro" },
                values: new object[] { 4, new DateTime(2026, 4, 1, 8, 25, 20, 901, DateTimeKind.Local).AddTicks(8476) });

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CategoriaGastoId", "FechaRegistro" },
                values: new object[] { 10, new DateTime(2026, 4, 1, 8, 25, 20, 901, DateTimeKind.Local).AddTicks(8479) });

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CategoriaGastoId", "FechaRegistro" },
                values: new object[] { 5, new DateTime(2026, 4, 1, 8, 25, 20, 901, DateTimeKind.Local).AddTicks(8482) });

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 1, 8, 25, 20, 901, DateTimeKind.Local).AddTicks(8064));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 1, 8, 25, 20, 901, DateTimeKind.Local).AddTicks(8067));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 1, 8, 25, 20, 901, DateTimeKind.Local).AddTicks(8069));

            migrationBuilder.UpdateData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2026, 4, 1, 8, 25, 20, 901, DateTimeKind.Local).AddTicks(8072));

            migrationBuilder.InsertData(
                table: "Ingreso",
                columns: new[] { "Id", "FechaRegistro", "Monto", "Tipo", "UsuarioId" },
                values: new object[] { 5, new DateTime(2026, 4, 1, 8, 25, 20, 901, DateTimeKind.Local).AddTicks(8074), 150000, "Banco", 2 });

            migrationBuilder.InsertData(
                table: "Egreso",
                columns: new[] { "Id", "CategoriaGastoId", "FechaRegistro", "Monto", "Tipo", "UsuarioId" },
                values: new object[] { 5, 2, new DateTime(2026, 4, 1, 8, 25, 20, 901, DateTimeKind.Local).AddTicks(8484), 180000, "Banco", 2 });

            migrationBuilder.AddCheckConstraint(
                name: "CK_Ingreso_Tipo",
                table: "Ingreso",
                sql: "[Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco')");

            migrationBuilder.CreateIndex(
                name: "IX_Egreso_CategoriaGastoId",
                table: "Egreso",
                column: "CategoriaGastoId");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Egreso_Tipo",
                table: "Egreso",
                sql: "[Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco')");

            migrationBuilder.AddForeignKey(
                name: "FK_Ahorro_MetaAhorro_MetaAhorroId",
                table: "Ahorro",
                column: "MetaAhorroId",
                principalTable: "MetaAhorro",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Egreso_CategoriaGasto_CategoriaGastoId",
                table: "Egreso",
                column: "CategoriaGastoId",
                principalTable: "CategoriaGasto",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ahorro_MetaAhorro_MetaAhorroId",
                table: "Ahorro");

            migrationBuilder.DropForeignKey(
                name: "FK_Egreso_CategoriaGasto_CategoriaGastoId",
                table: "Egreso");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Ingreso_Tipo",
                table: "Ingreso");

            migrationBuilder.DropIndex(
                name: "IX_Egreso_CategoriaGastoId",
                table: "Egreso");

            migrationBuilder.DropCheckConstraint(
                name: "CK_Egreso_Tipo",
                table: "Egreso");

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 10);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 11);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 12);

            migrationBuilder.DeleteData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Ingreso",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "CategoriaGasto",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropColumn(
                name: "CategoriaGastoId",
                table: "Egreso");

            migrationBuilder.AlterColumn<long>(
                name: "Id",
                table: "CategoriaGasto",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "MetaAhorroId",
                table: "Ahorro",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "CategoriaGasto",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1L, "Alimentación" },
                    { 2L, "Transporte" },
                    { 3L, "Salud" },
                    { 4L, "Hogar" },
                    { 5L, "Servicios" },
                    { 6L, "Educación" },
                    { 7L, "Entretenimiento" },
                    { 8L, "Mascotas" },
                    { 9L, "Ropa" },
                    { 10L, "Deudas" },
                    { 11L, "Inversiones" },
                    { 12L, "Otros" }
                });

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 1,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(9394));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 2,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(9397));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 3,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(9399));

            migrationBuilder.UpdateData(
                table: "Egreso",
                keyColumn: "Id",
                keyValue: 4,
                column: "FechaRegistro",
                value: new DateTime(2026, 3, 26, 23, 34, 53, 63, DateTimeKind.Local).AddTicks(9402));

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

            migrationBuilder.AddCheckConstraint(
                name: "CK_Ingreso_Tipo",
                table: "Ingreso",
                sql: "[Tipo] IN ('Efectivo', 'App', 'Nequi')");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Egreso_Tipo",
                table: "Egreso",
                sql: "[Tipo] IN ('Efectivo', 'App', 'Nequi')");

            migrationBuilder.AddForeignKey(
                name: "FK_Ahorro_MetaAhorro_MetaAhorroId",
                table: "Ahorro",
                column: "MetaAhorroId",
                principalTable: "MetaAhorro",
                principalColumn: "Id");
        }
    }
}
