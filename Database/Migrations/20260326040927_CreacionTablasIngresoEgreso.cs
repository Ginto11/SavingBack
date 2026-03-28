using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SavingBack.Database.Migrations
{
    /// <inheritdoc />
    public partial class CreacionTablasIngresoEgreso : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Egreso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egreso", x => x.Id);
                    table.CheckConstraint("CK_Egreso_Tipo", "[Tipo] IN ('Efectivo', 'App', 'Nequi')");
                });

            migrationBuilder.CreateTable(
                name: "Ingreso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ingreso", x => x.Id);
                    table.CheckConstraint("CK_Ingreso_Tipo", "[Tipo] IN ('Efectivo', 'App', 'Nequi')");
                    table.ForeignKey(
                        name: "FK_Ingreso_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Egreso",
                columns: new[] { "Id", "FechaRegistro", "Monto", "Tipo" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 25, 23, 9, 26, 261, DateTimeKind.Local).AddTicks(230), 100000, "Nequi" },
                    { 2, new DateTime(2026, 3, 25, 23, 9, 26, 261, DateTimeKind.Local).AddTicks(233), 8000, "App" },
                    { 3, new DateTime(2026, 3, 25, 23, 9, 26, 261, DateTimeKind.Local).AddTicks(235), 12000, "Efectivo" },
                    { 4, new DateTime(2026, 3, 25, 23, 9, 26, 261, DateTimeKind.Local).AddTicks(237), 60000, "Efectivo" }
                });

            migrationBuilder.InsertData(
                table: "Ingreso",
                columns: new[] { "Id", "FechaRegistro", "Monto", "Tipo", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 3, 25, 23, 9, 26, 260, DateTimeKind.Local).AddTicks(9775), 10000, "Efectivo", 2 },
                    { 2, new DateTime(2026, 3, 25, 23, 9, 26, 260, DateTimeKind.Local).AddTicks(9778), 80000, "Nequi", 2 },
                    { 3, new DateTime(2026, 3, 25, 23, 9, 26, 260, DateTimeKind.Local).AddTicks(9781), 120000, "App", 2 },
                    { 4, new DateTime(2026, 3, 25, 23, 9, 26, 260, DateTimeKind.Local).AddTicks(9783), 66000, "Efectivo", 2 }
                });

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "FotoPerfil",
                value: "/Uploads/Fotos/default.png");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_UsuarioId",
                table: "Ingreso",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Egreso");

            migrationBuilder.DropTable(
                name: "Ingreso");

            migrationBuilder.UpdateData(
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "FotoPerfil",
                value: "/Fotos/default.png");
        }
    }
}
