﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MagicVilla_Api.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "", "Detalle de la Villa", new DateTime(2024, 2, 6, 20, 8, 10, 275, DateTimeKind.Local).AddTicks(3569), new DateTime(2024, 2, 6, 20, 8, 10, 275, DateTimeKind.Local).AddTicks(3551), "", 50, "Villa Real", 5, 200.0 },
                    { 2, "", "Detalle de la Villa de Bogota", new DateTime(2024, 2, 6, 20, 8, 10, 275, DateTimeKind.Local).AddTicks(3572), new DateTime(2024, 2, 6, 20, 8, 10, 275, DateTimeKind.Local).AddTicks(3571), "", 90, "Villa Bogota", 15, 900.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
