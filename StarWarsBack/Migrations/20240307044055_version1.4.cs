using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StarWarsBack.Migrations
{
    /// <inheritdoc />
    public partial class version14 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "createdAt",
                table: "usuario",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "updatedAt",
                table: "usuario",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "createdAt",
                table: "usuario");

            migrationBuilder.DropColumn(
                name: "updatedAt",
                table: "usuario");
        }
    }
}
