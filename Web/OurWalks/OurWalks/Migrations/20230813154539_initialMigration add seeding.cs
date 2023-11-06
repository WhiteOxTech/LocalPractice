using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OurWalks.Migrations
{
    /// <inheritdoc />
    public partial class initialMigrationaddseeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Regions",
                columns: new[] { "Id", "Code", "Name", "RegionImageURL" },
                values: new object[] { new Guid("d1dcc70b-31af-4fad-859e-218210a6b77e"), "BENG", "Bengalore", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Regions",
                keyColumn: "Id",
                keyValue: new Guid("d1dcc70b-31af-4fad-859e-218210a6b77e"));
        }
    }
}
