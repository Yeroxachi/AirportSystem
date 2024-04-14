﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using Persistence.Context;

#nullable disable

namespace Persistence.Migrations
{
    [DbContext(typeof(AirportContext))]
    partial class AirportContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.29")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Models.City", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(58)
                        .HasColumnType("character varying(58)");

                    b.HasKey("Id");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("Domain.Models.Flight", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("Arrival")
                        .HasColumnType("timestamp with time zone");

                    b.Property<DateTime>("Departure")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DestinationId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("OriginId")
                        .HasColumnType("uuid");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DestinationId");

                    b.HasIndex("OriginId");

                    b.ToTable("Flights");
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("UserRole")
                        .HasColumnType("integer");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasMaxLength(55)
                        .HasColumnType("character varying(55)");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FlightUser", b =>
                {
                    b.Property<Guid>("ClientsId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("FlightsId")
                        .HasColumnType("uuid");

                    b.HasKey("ClientsId", "FlightsId");

                    b.HasIndex("FlightsId");

                    b.ToTable("FlightUser");
                });

            modelBuilder.Entity("Domain.Models.Flight", b =>
                {
                    b.HasOne("Domain.Models.City", "Destination")
                        .WithMany("DestinationFlights")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.City", "Origin")
                        .WithMany("OriginFlights")
                        .HasForeignKey("OriginId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");

                    b.Navigation("Origin");
                });

            modelBuilder.Entity("FlightUser", b =>
                {
                    b.HasOne("Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("ClientsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.Flight", null)
                        .WithMany()
                        .HasForeignKey("FlightsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Domain.Models.City", b =>
                {
                    b.Navigation("DestinationFlights");

                    b.Navigation("OriginFlights");
                });
#pragma warning restore 612, 618
        }
    }
}
