using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SavingBack.Database.Migrations
{
    /// <inheritdoc />
    public partial class AñadiendoManejoGastosEnUsuario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Rol",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "ManejaGastos",
                table: "Usuario",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "CategoriaGasto",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoriaGasto", x => x.Id);
                });

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
                table: "Usuario",
                keyColumn: "Id",
                keyValue: 1,
                column: "ManejaGastos",
                value: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CategoriaGasto");

            migrationBuilder.DropColumn(
                name: "ManejaGastos",
                table: "Usuario");

            migrationBuilder.AlterColumn<string>(
                name: "Rol",
                table: "Usuario",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);
        }
    }
}
