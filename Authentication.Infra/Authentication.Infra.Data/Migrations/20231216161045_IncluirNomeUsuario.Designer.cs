﻿// <auto-generated />
using System;
using Authentication.Infra.Data.Contexts.DbAuth;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Authentication.Infra.Data.Migrations
{
    [DbContext(typeof(DbAuthContext))]
    [Migration("20231216161045_IncluirNomeUsuario")]
    partial class IncluirNomeUsuario
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("Authentication.Application.Domain.Contexts.DbAuth.Audits.Audit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("AffectedColumns")
                        .HasMaxLength(8000)
                        .HasColumnType("varchar");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime2");

                    b.Property<string>("NewValues")
                        .HasMaxLength(8000)
                        .HasColumnType("varchar");

                    b.Property<string>("OldValues")
                        .HasMaxLength(8000)
                        .HasColumnType("varchar");

                    b.Property<string>("PrimaryKey")
                        .HasMaxLength(8000)
                        .HasColumnType("varchar");

                    b.Property<string>("TableName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("Type")
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.Property<string>("UserId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs", (string)null);
                });

            modelBuilder.Entity("Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasMaxLength(50)
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Chave")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime2")
                        .HasDefaultValueSql("getdate()");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("nvarchar(300)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<DateTime>("UltimoAcesso")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
