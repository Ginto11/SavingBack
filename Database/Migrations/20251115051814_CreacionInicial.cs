using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavingBack.Database.Migrations
{
    /// <inheritdoc />
    public partial class CreacionInicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PrimerNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SegundoNombre = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cedula = table.Column<long>(type: "bigint", nullable: false),
                    NombreUsuario = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Correo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FechaNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Contrasena = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AceptaTerminos = table.Column<bool>(type: "bit", nullable: false),
                    Rol = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                    table.CheckConstraint("CK_Usuario_Rol", "Rol IN ('Cliente', 'Admin')");
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
                    MetaAhorroId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ahorro", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ahorro_MetaAhorro_MetaAhorroId",
                        column: x => x.MetaAhorroId,
                        principalTable: "MetaAhorro",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ahorro_Usuario_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuario",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Usuario",
                columns: new[] { "Id", "AceptaTerminos", "Cedula", "Contrasena", "Correo", "FechaNacimiento", "NombreUsuario", "PrimerNombre", "Rol", "SegundoNombre" },
                values: new object[] { 1, true, 1001944317L, "+osxdTMbvrdSIPgNenMgtQ==", "Salinitosnelson@gmail.com", new DateTime(2001, 8, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Root", "Admin", "Admin", "" });

            migrationBuilder.CreateIndex(
                name: "IX_Ahorro_MetaAhorroId",
                table: "Ahorro",
                column: "MetaAhorroId");

            migrationBuilder.CreateIndex(
                name: "IX_Ahorro_UsuarioId",
                table: "Ahorro",
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
                name: "MetaAhorro");

            migrationBuilder.DropTable(
                name: "Usuario");
        }
    }
}
