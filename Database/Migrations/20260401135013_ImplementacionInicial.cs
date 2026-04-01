using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SavingBack.Database.Migrations
{
    /// <inheritdoc />
    public partial class ImplementacionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoriaGasto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaGasto", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimerNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PrimerApellido = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cedula = table.Column<long>(type: "bigint", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AceptaTerminos = table.Column<bool>(type: "bit", nullable: false),
                    ManejaGastos = table.Column<bool>(type: "bit", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FotoPerfil = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.CheckConstraint("CK_Usuario_Rol", "Rol IN ('Cliente', 'Admin')");
                });

            migrationBuilder.CreateTable(
                name: "Egreso",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Tipo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaRegistro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Monto = table.Column<int>(type: "int", nullable: false),
                    CategoriaGastoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Egreso", x => x.Id);
                    table.CheckConstraint("CK_Egreso_Tipo", "[Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco')");
                    table.ForeignKey(
                        name: "FK_Egreso_CategoriaGasto_CategoriaGastoId",
                        column: x => x.CategoriaGastoId,
                        principalTable: "CategoriaGasto",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Egreso_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
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
                    table.CheckConstraint("CK_Ingreso_Tipo", "[Tipo] IN ('Efectivo', 'App', 'Nequi', 'Banco')");
                    table.ForeignKey(
                        name: "FK_Ingreso_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MetaAhorro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MontoObjetivo = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    MontoActual = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    FechaCreacion = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MetaAhorro", x => x.Id);
                    table.CheckConstraint("CK_MetaAhorro_Estado", "Estado IN ('Activa', 'Cumplida', 'Cancelada')");
                    table.ForeignKey(
                        name: "FK_MetaAhorro_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Ahorro",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    Monto = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    Fecha = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    MetaAhorroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ahorro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ahorro_MetaAhorro_MetaAhorroId",
                        column: x => x.MetaAhorroId,
                        principalTable: "MetaAhorro",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Ahorro_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "CategoriaGasto",
                columns: new[] { "Id", "Nombre" },
                values: new object[,]
                {
                    { 1, "Alimentos" },
                    { 2, "Transporte" },
                    { 3, "Salud" },
                    { 4, "Hogar" },
                    { 5, "Servicios" },
                    { 6, "Estudios" },
                    { 7, "Entretenimiento" },
                    { 8, "Mascotas" },
                    { 9, "Ropa" },
                    { 10, "Deudas" },
                    { 11, "Inversiones" },
                    { 12, "Otros" }
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "AceptaTerminos", "Cedula", "Contrasena", "Correo", "FechaNacimiento", "FotoPerfil", "ManejaGastos", "NombreUsuario", "PrimerApellido", "PrimerNombre", "Rol" },
                values: new object[] { 1, true, 1001944317L, "+osxdTMbvrdSIPgNenMgtQ==", "Salinitosnelson@gmail.com", new DateTime(2001, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "/Uploads/Fotos/default.png", false, "Root", "", "Admin", "Admin" });

            migrationBuilder.InsertData(
                table: "Egreso",
                columns: new[] { "Id", "CategoriaGastoId", "FechaRegistro", "Monto", "Tipo", "UsuarioId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2026, 4, 1, 8, 50, 12, 987, DateTimeKind.Local).AddTicks(164), 100000, "Nequi", 1 },
                    { 2, 4, new DateTime(2026, 4, 1, 8, 50, 12, 987, DateTimeKind.Local).AddTicks(168), 8000, "App", 1 },
                    { 3, 10, new DateTime(2026, 4, 1, 8, 50, 12, 987, DateTimeKind.Local).AddTicks(170), 12000, "Efectivo", 1 },
                    { 4, 5, new DateTime(2026, 4, 1, 8, 50, 12, 987, DateTimeKind.Local).AddTicks(172), 60000, "Efectivo", 1 },
                    { 5, 2, new DateTime(2026, 4, 1, 8, 50, 12, 987, DateTimeKind.Local).AddTicks(175), 180000, "Banco", 1 }
                });

            migrationBuilder.InsertData(
                table: "Ingreso",
                columns: new[] { "Id", "FechaRegistro", "Monto", "Tipo", "UsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(2026, 4, 1, 8, 50, 12, 986, DateTimeKind.Local).AddTicks(9791), 10000, "Efectivo", 1 },
                    { 2, new DateTime(2026, 4, 1, 8, 50, 12, 986, DateTimeKind.Local).AddTicks(9795), 80000, "Nequi", 1 },
                    { 3, new DateTime(2026, 4, 1, 8, 50, 12, 986, DateTimeKind.Local).AddTicks(9797), 120000, "App", 1 },
                    { 4, new DateTime(2026, 4, 1, 8, 50, 12, 986, DateTimeKind.Local).AddTicks(9799), 66000, "Efectivo", 1 },
                    { 5, new DateTime(2026, 4, 1, 8, 50, 12, 986, DateTimeKind.Local).AddTicks(9801), 150000, "Banco", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Ahorro_MetaAhorroId",
                table: "Ahorro",
                column: "MetaAhorroId");

            migrationBuilder.CreateIndex(
                name: "IX_Ahorro_UsuarioId",
                table: "Ahorro",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Egreso_CategoriaGastoId",
                table: "Egreso",
                column: "CategoriaGastoId");

            migrationBuilder.CreateIndex(
                name: "IX_Egreso_UsuarioId",
                table: "Egreso",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Ingreso_UsuarioId",
                table: "Ingreso",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_MetaAhorro_UsuarioId",
                table: "MetaAhorro",
                column: "UsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Ahorro");

            migrationBuilder.DropTable(
                name: "Egreso");

            migrationBuilder.DropTable(
                name: "Ingreso");

            migrationBuilder.DropTable(
                name: "MetaAhorro");

            migrationBuilder.DropTable(
                name: "CategoriaGasto");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
