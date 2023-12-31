﻿// <auto-generated />
using System;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Migrations
{
    [DbContext(typeof(DataContext))]
    [Migration("20230913135548_Initial_01")]
    partial class Initial_01
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.17")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Domain.Models.Acomodacao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Ativo");

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao");

                    b.Property<int?>("IdHome")
                        .HasColumnType("int")
                        .HasColumnName("IdHome");

                    b.Property<int?>("IdValor")
                        .HasColumnType("int")
                        .HasColumnName("IdTarifas");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.HasIndex("IdHome");

                    b.HasIndex("IdValor");

                    b.ToTable("TBL_Acomodacao");
                });

            modelBuilder.Entity("Domain.Models.Fotos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Ativo");

                    b.Property<int>("IdAcomodacao")
                        .HasColumnType("int")
                        .HasColumnName("IdAcomodacao");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.HasIndex("IdAcomodacao");

                    b.ToTable("TBL_Fotos");
                });

            modelBuilder.Entity("Domain.Models.Home", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao");

                    b.HasKey("Id");

                    b.ToTable("TBL_Pousada");
                });

            modelBuilder.Entity("Domain.Models.Tarifas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Ativo")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bit")
                        .HasDefaultValue(false)
                        .HasColumnName("Ativo");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(18,2)")
                        .HasColumnName("Valor");

                    b.HasKey("Id");

                    b.ToTable("TBL_Tarifas");
                });

            modelBuilder.Entity("Domain.Models.Acomodacao", b =>
                {
                    b.HasOne("Domain.Models.Home", "Home")
                        .WithMany("Acomodacao")
                        .HasForeignKey("IdHome");

                    b.HasOne("Domain.Models.Tarifas", "Tarifas")
                        .WithMany("Acomodacao")
                        .HasForeignKey("IdValor");

                    b.Navigation("Home");

                    b.Navigation("Tarifas");
                });

            modelBuilder.Entity("Domain.Models.Fotos", b =>
                {
                    b.HasOne("Domain.Models.Acomodacao", "Acomodacao")
                        .WithMany("Fotos")
                        .HasForeignKey("IdAcomodacao")
                        .IsRequired();

                    b.Navigation("Acomodacao");
                });

            modelBuilder.Entity("Domain.Models.Acomodacao", b =>
                {
                    b.Navigation("Fotos");
                });

            modelBuilder.Entity("Domain.Models.Home", b =>
                {
                    b.Navigation("Acomodacao");
                });

            modelBuilder.Entity("Domain.Models.Tarifas", b =>
                {
                    b.Navigation("Acomodacao");
                });
#pragma warning restore 612, 618
        }
    }
}
