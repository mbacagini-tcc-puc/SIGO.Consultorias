﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using SIGO.Consultorias.Data;

namespace SIGO.Consultorias.Data.Migrations
{
    [DbContext(typeof(ConsultoriasContext))]
    [Migration("20210122230426_InitialCreate")]
    partial class InitialCreate
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .UseIdentityByDefaultColumns()
                .HasAnnotation("Relational:MaxIdentifierLength", 63)
                .HasAnnotation("ProductVersion", "5.0.2");

            modelBuilder.Entity("SIGO.Consultorias.Entities.Analise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTimeOffset?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_alteracao");

                    b.Property<DateTimeOffset>("DataInclusao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_inclusao");

                    b.Property<DateTimeOffset?>("DataPublicacao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_publicacao");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("integer")
                        .HasColumnName("id_empresa");

                    b.Property<string>("Resumo")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("resumo");

                    b.Property<string>("Titulo")
                        .HasMaxLength(50)
                        .HasColumnType("character varying(50)")
                        .HasColumnName("titulo");

                    b.Property<string>("UsuarioAlteracao")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("usuario_alteracao");

                    b.Property<int?>("UsuarioAlteracaoId")
                        .HasColumnType("integer")
                        .HasColumnName("id_usuario_alteracao");

                    b.Property<string>("UsuarioInclusao")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("usuario_inclusao");

                    b.Property<int>("UsuarioInclusaoId")
                        .HasColumnType("integer")
                        .HasColumnName("id_usuario_inclusao");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("analises");
                });

            modelBuilder.Entity("SIGO.Consultorias.Entities.Anexo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<int>("AnaliseId")
                        .HasColumnType("integer")
                        .HasColumnName("id_analise");

                    b.Property<string>("Caminho")
                        .HasMaxLength(500)
                        .HasColumnType("character varying(500)")
                        .HasColumnName("caminho");

                    b.Property<DateTimeOffset?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_alteracao");

                    b.Property<DateTimeOffset>("DataInclusao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_inclusao");

                    b.Property<string>("NomeArquivo")
                        .HasMaxLength(256)
                        .HasColumnType("character varying(256)")
                        .HasColumnName("nome_arquivo");

                    b.HasKey("Id");

                    b.HasIndex("AnaliseId");

                    b.ToTable("anexos");
                });

            modelBuilder.Entity("SIGO.Consultorias.Entities.Empresa", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<bool>("ContratoAtivo")
                        .HasMaxLength(18)
                        .HasColumnType("boolean")
                        .HasColumnName("contrato_ativo");

                    b.Property<DateTimeOffset?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_alteracao");

                    b.Property<DateTimeOffset>("DataInclusao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_inclusao");

                    b.Property<string>("NomeFantasia")
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("nome_fantasia");

                    b.Property<string>("RazaoSocial")
                        .HasMaxLength(100)
                        .HasColumnType("character varying(100)")
                        .HasColumnName("razao_social");

                    b.HasKey("Id");

                    b.ToTable("empresas");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ContratoAtivo = true,
                            DataInclusao = new DateTimeOffset(new DateTime(2021, 1, 22, 23, 4, 26, 253, DateTimeKind.Unspecified).AddTicks(7050), new TimeSpan(0, 0, 0, 0, 0)),
                            NomeFantasia = "Consultoria 1",
                            RazaoSocial = "Consultoria 1 LTDA"
                        },
                        new
                        {
                            Id = 2,
                            ContratoAtivo = true,
                            DataInclusao = new DateTimeOffset(new DateTime(2021, 1, 22, 23, 4, 26, 253, DateTimeKind.Unspecified).AddTicks(7598), new TimeSpan(0, 0, 0, 0, 0)),
                            NomeFantasia = "Consultoria 2",
                            RazaoSocial = "Consultoria 2 LTDA"
                        });
                });

            modelBuilder.Entity("SIGO.Consultorias.Entities.EmpresaUsuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("id")
                        .UseIdentityByDefaultColumn();

                    b.Property<DateTimeOffset?>("DataAlteracao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_alteracao");

                    b.Property<DateTimeOffset>("DataInclusao")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("data_inclusao");

                    b.Property<int>("EmpresaId")
                        .HasColumnType("integer")
                        .HasColumnName("id_empresa");

                    b.Property<int>("UsuarioId")
                        .HasColumnType("integer")
                        .HasColumnName("id_usuario");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("empresas_usuarios");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DataInclusao = new DateTimeOffset(new DateTime(2021, 1, 22, 23, 4, 26, 259, DateTimeKind.Unspecified).AddTicks(4027), new TimeSpan(0, 0, 0, 0, 0)),
                            EmpresaId = 1,
                            UsuarioId = 2
                        },
                        new
                        {
                            Id = 2,
                            DataInclusao = new DateTimeOffset(new DateTime(2021, 1, 22, 23, 4, 26, 259, DateTimeKind.Unspecified).AddTicks(4146), new TimeSpan(0, 0, 0, 0, 0)),
                            EmpresaId = 2,
                            UsuarioId = 3
                        });
                });

            modelBuilder.Entity("SIGO.Consultorias.Entities.Analise", b =>
                {
                    b.HasOne("SIGO.Consultorias.Entities.Empresa", "Empresa")
                        .WithMany("Analises")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("SIGO.Consultorias.Entities.Anexo", b =>
                {
                    b.HasOne("SIGO.Consultorias.Entities.Analise", "Analise")
                        .WithMany("Anexos")
                        .HasForeignKey("AnaliseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Analise");
                });

            modelBuilder.Entity("SIGO.Consultorias.Entities.EmpresaUsuario", b =>
                {
                    b.HasOne("SIGO.Consultorias.Entities.Empresa", "Empresa")
                        .WithMany("Usuarios")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("SIGO.Consultorias.Entities.Analise", b =>
                {
                    b.Navigation("Anexos");
                });

            modelBuilder.Entity("SIGO.Consultorias.Entities.Empresa", b =>
                {
                    b.Navigation("Analises");

                    b.Navigation("Usuarios");
                });
#pragma warning restore 612, 618
        }
    }
}
