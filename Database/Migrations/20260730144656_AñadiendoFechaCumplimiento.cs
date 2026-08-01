using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SavingBack.Database.Migrations
{
    /// <inheritdoc />
    public partial class AñadiendoFechaCumplimiento : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "FechaCumplimiento",
                table: "MetaAhorro",
                type: "datetime2",
                nullable: true);

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FechaCumplimiento",
                table: "MetaAhorro");

        }
    }
}
