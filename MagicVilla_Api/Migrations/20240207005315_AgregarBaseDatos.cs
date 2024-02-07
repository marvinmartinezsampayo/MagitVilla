﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MagicVilla_Api.Migrations
{
    /// <inheritdoc />
    public partial class AgregarBaseDatos : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Villas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nombre = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Detalle = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Tarifa = table.Column<double>(type: "BINARY_DOUBLE", nullable: false),
                    Ocupantes = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    MetrosCuadrados = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ImagenUrl = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    Amenidad = table.Column<string>(type: "NVARCHAR2(2000)", nullable: false),
                    FechaCreacion = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false),
                    FechaActualizacion = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Villas", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Villas");
        }
    }
}
