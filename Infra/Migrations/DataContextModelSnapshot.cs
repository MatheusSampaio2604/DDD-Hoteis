﻿// <auto-generated />
using System;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Infra.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("Domain.Models.Home", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Descricao_0")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao 0");

                    b.Property<string>("Descricao_1")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao 1");

                    b.Property<string>("Descricao_2")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao 2");

                    b.Property<string>("Descricao_3")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao 3");

                    b.Property<string>("Descricao_4")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Descricao 4");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.HasKey("Id");

                    b.ToTable("TBL_Estabelecimento");
                });

            modelBuilder.Entity("Domain.Models.Imagens", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("Id_Acomodacao")
                        .HasColumnType("int")
                        .HasColumnName("IdAcomodacao");

                    b.Property<string>("Nome")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Nome");

                    b.Property<string>("RotaImagem")
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("CaminhoImagem");

                    b.HasKey("Id");

                    b.HasIndex("Id_Acomodacao");

                    b.ToTable("TBL_Imagens");
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
                        .HasPrecision(18, 2)
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

            modelBuilder.Entity("Domain.Models.Imagens", b =>
                {
                    b.HasOne("Domain.Models.Acomodacao", "Acomodacao")
                        .WithMany("Imagens")
                        .HasForeignKey("Id_Acomodacao");

                    b.Navigation("Acomodacao");
                });

            modelBuilder.Entity("Domain.Models.Acomodacao", b =>
                {
                    b.Navigation("Imagens");
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
