﻿// <auto-generated />
using System;
using AppMinhaBahia.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AppMinhaBahia.Migrations
{
    [DbContext(typeof(AppMinhaBahiaContext))]
    partial class AppMinhaBahiaContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AppMinhaBahia.Models.Intervencao", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("OcorrenciaId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.HasIndex("OcorrenciaId");

                    b.ToTable("Intervencao");
                });

            modelBuilder.Entity("AppMinhaBahia.Models.Ocorrencia", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double?>("Custo")
                        .HasColumnType("double");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("Funcionarios")
                        .HasColumnType("int");

                    b.Property<string>("LocalEspecifico")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int?>("PrefeituraId")
                        .HasColumnType("int");

                    b.Property<int?>("SetorId")
                        .HasColumnType("int");

                    b.Property<string>("Status")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Tipo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrefeituraId");

                    b.HasIndex("SetorId");

                    b.HasIndex("UsuarioId");

                    b.ToTable("Ocorrencia");
                });

            modelBuilder.Entity("AppMinhaBahia.Models.Prefeitura", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("NomeCidade")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Prefeituras");
                });

            modelBuilder.Entity("AppMinhaBahia.Models.Requisicao", b =>
                {
                    b.Property<int>("SetorId")
                        .HasColumnType("int");

                    b.Property<int>("PrefeituraId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<int?>("Funcionarios")
                        .HasColumnType("int");

                    b.Property<double?>("Fundos")
                        .HasColumnType("double");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("Tipo")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("SetorId", "PrefeituraId");

                    b.HasIndex("PrefeituraId");

                    b.ToTable("Requisicao");
                });

            modelBuilder.Entity("AppMinhaBahia.Models.Setor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<double>("Fundos")
                        .HasColumnType("double");

                    b.Property<string>("Nome")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<int>("PrefeituraId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("PrefeituraId");

                    b.ToTable("Setor");
                });

            modelBuilder.Entity("AppMinhaBahia.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CPF")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.Property<string>("NomeCompleto")
                        .HasColumnType("longtext CHARACTER SET utf8mb4");

                    b.HasKey("Id");

                    b.ToTable("Usuario");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Usuario");
                });

            modelBuilder.Entity("AppMinhaBahia.Models.Funcionario", b =>
                {
                    b.HasBaseType("AppMinhaBahia.Models.Usuario");

                    b.Property<bool>("Disponivel")
                        .HasColumnType("tinyint(1)");

                    b.Property<int?>("IntervencaoId")
                        .HasColumnType("int");

                    b.Property<int>("SetorId")
                        .HasColumnType("int");

                    b.HasIndex("IntervencaoId");

                    b.HasIndex("SetorId");

                    b.HasDiscriminator().HasValue("Funcionario");
                });

            modelBuilder.Entity("AppMinhaBahia.Models.Intervencao", b =>
                {
                    b.HasOne("AppMinhaBahia.Models.Ocorrencia", "Ocorrencia")
                        .WithMany()
                        .HasForeignKey("OcorrenciaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppMinhaBahia.Models.Ocorrencia", b =>
                {
                    b.HasOne("AppMinhaBahia.Models.Prefeitura", null)
                        .WithMany("Ocorrencias")
                        .HasForeignKey("PrefeituraId");

                    b.HasOne("AppMinhaBahia.Models.Setor", null)
                        .WithMany("Ocorrencias")
                        .HasForeignKey("SetorId");

                    b.HasOne("AppMinhaBahia.Models.Usuario", "Usuario")
                        .WithMany("Ocorrencias")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppMinhaBahia.Models.Requisicao", b =>
                {
                    b.HasOne("AppMinhaBahia.Models.Prefeitura", "Prefeitura")
                        .WithMany("Requisicoes")
                        .HasForeignKey("PrefeituraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AppMinhaBahia.Models.Setor", "Setor")
                        .WithMany("Requisicoes")
                        .HasForeignKey("SetorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppMinhaBahia.Models.Setor", b =>
                {
                    b.HasOne("AppMinhaBahia.Models.Prefeitura", "Prefeitura")
                        .WithMany("Setores")
                        .HasForeignKey("PrefeituraId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AppMinhaBahia.Models.Funcionario", b =>
                {
                    b.HasOne("AppMinhaBahia.Models.Intervencao", "Intervencao")
                        .WithMany("MaoDeObra")
                        .HasForeignKey("IntervencaoId");

                    b.HasOne("AppMinhaBahia.Models.Setor", "Setor")
                        .WithMany("Funcionarios")
                        .HasForeignKey("SetorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
