﻿// <auto-generated />
using System;
using MagicVilla_Api.Datos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Oracle.EntityFrameworkCore.Metadata;

#nullable disable

namespace MagicVilla_Api.Migrations
{
    [DbContext(typeof(ApplicationDBContext))]
    [Migration("20240210215924_AgregarNumeroVillaTabla")]
    partial class AgregarNumeroVillaTabla
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.15")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            OracleModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("MagicVilla_Api.Modelos.NumeroVilla", b =>
                {
                    b.Property<int>("VillaNo")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("DetalleEspecial")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<int>("VillaId")
                        .HasColumnType("NUMBER(10)");

                    b.HasKey("VillaNo");

                    b.HasIndex("VillaId");

                    b.ToTable("NumeroVillas");
                });

            modelBuilder.Entity("MagicVilla_Api.Modelos.Villa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("NUMBER(10)");

                    OraclePropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Amenidad")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<string>("Detalle")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<DateTime>("FechaActualizacion")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<DateTime>("FechaCreacion")
                        .HasColumnType("TIMESTAMP(7)");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("MetrosCuadrados")
                        .HasColumnType("NUMBER(10)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("NVARCHAR2(2000)");

                    b.Property<int>("Ocupantes")
                        .HasColumnType("NUMBER(10)");

                    b.Property<double>("Tarifa")
                        .HasColumnType("BINARY_DOUBLE");

                    b.HasKey("Id");

                    b.ToTable("Villas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Amenidad = "",
                            Detalle = "Detalle de la Villa",
                            FechaActualizacion = new DateTime(2024, 2, 10, 16, 59, 24, 765, DateTimeKind.Local).AddTicks(9687),
                            FechaCreacion = new DateTime(2024, 2, 10, 16, 59, 24, 765, DateTimeKind.Local).AddTicks(9668),
                            ImagenUrl = "",
                            MetrosCuadrados = 50,
                            Nombre = "Villa Real",
                            Ocupantes = 5,
                            Tarifa = 200.0
                        },
                        new
                        {
                            Id = 2,
                            Amenidad = "",
                            Detalle = "Detalle de la Villa de Bogota",
                            FechaActualizacion = new DateTime(2024, 2, 10, 16, 59, 24, 765, DateTimeKind.Local).AddTicks(9690),
                            FechaCreacion = new DateTime(2024, 2, 10, 16, 59, 24, 765, DateTimeKind.Local).AddTicks(9690),
                            ImagenUrl = "",
                            MetrosCuadrados = 90,
                            Nombre = "Villa Bogota",
                            Ocupantes = 15,
                            Tarifa = 900.0
                        });
                });

            modelBuilder.Entity("MagicVilla_Api.Modelos.NumeroVilla", b =>
                {
                    b.HasOne("MagicVilla_Api.Modelos.Villa", "Villa")
                        .WithMany()
                        .HasForeignKey("VillaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Villa");
                });
#pragma warning restore 612, 618
        }
    }
}
