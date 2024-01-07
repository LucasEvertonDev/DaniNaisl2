﻿// <auto-generated />
using System;
using ControlServices.Infra.Data.Contexts.ControlServicesDb;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ControlServices.Infra.Data.Migrations
{
    [DbContext(typeof(ControlServicesDbContext))]
    [Migration("20240106095434_Atendimento")]
    partial class Atendimento
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Atendimento", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("ClienteAtrasado")
                        .HasColumnType("tinyint(1)");

                    b.Property<Guid>("ClienteId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("ObservacaoAtendimento")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<decimal?>("ValorAtendimento")
                        .HasColumnType("decimal(65,30)");

                    b.Property<decimal?>("ValorPago")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("ClienteId");

                    b.ToTable("Atendimentos");
                });

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.Audits.Audit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<byte[]>("AffectedColumns")
                        .HasColumnType("BLOB");

                    b.Property<DateTime>("DateTime")
                        .HasColumnType("datetime(6)");

                    b.Property<byte[]>("NewValues")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("OldValues")
                        .HasColumnType("BLOB");

                    b.Property<byte[]>("PrimaryKey")
                        .HasColumnType("BLOB");

                    b.Property<string>("TableName")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Type")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("UserId")
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.HasKey("Id");

                    b.ToTable("AuditLogs", (string)null);
                });

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Cpf")
                        .HasMaxLength(14)
                        .HasColumnType("varchar(14)");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<DateTime?>("DataNascimento")
                        .IsRequired()
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<string>("Telefone")
                        .HasMaxLength(19)
                        .HasColumnType("varchar(19)");

                    b.HasKey("Id");

                    b.ToTable("Clientes", (string)null);
                });

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos.Custo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime>("Data")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Descricao")
                        .HasColumnType("longtext");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<decimal>("Valor")
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.ToTable("Custos");
                });

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos.MapAtendimentoServico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<Guid>("AtendimentoId")
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<Guid>("ServicoId")
                        .HasColumnType("char(36)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<decimal?>("ValorCobrado")
                        .IsRequired()
                        .HasColumnType("decimal(65,30)");

                    b.HasKey("Id");

                    b.HasIndex("AtendimentoId");

                    b.HasIndex("ServicoId");

                    b.ToTable("MapAtendimentosServicos");
                });

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos.Servico", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Descricao")
                        .HasMaxLength(200)
                        .HasColumnType("varchar(200)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Servicos", (string)null);
                });

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.Usuarios.Usuario", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Chave")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<DateTime?>("DataAtualizacao")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("DataCriacao")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("datetime")
                        .HasDefaultValueSql("CURRENT_TIMESTAMP");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Senha")
                        .IsRequired()
                        .HasMaxLength(300)
                        .HasColumnType("varchar(300)");

                    b.Property<int>("Situacao")
                        .HasColumnType("int");

                    b.Property<DateTime>("UltimoAcesso")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.ToTable("Usuarios", (string)null);
                });

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Atendimento", b =>
                {
                    b.HasOne("ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes.Cliente", "Cliente")
                        .WithMany("Atendimentos")
                        .HasForeignKey("ClienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Cliente");
                });

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos.MapAtendimentoServico", b =>
                {
                    b.HasOne("ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Atendimento", "Atendimento")
                        .WithMany("MapAtendimentosServicos")
                        .HasForeignKey("AtendimentoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos.Servico", "Servico")
                        .WithMany("MapAtendimentoServicos")
                        .HasForeignKey("ServicoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Atendimento");

                    b.Navigation("Servico");
                });

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos.Atendimento", b =>
                {
                    b.Navigation("MapAtendimentosServicos");
                });

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes.Cliente", b =>
                {
                    b.Navigation("Atendimentos");
                });

            modelBuilder.Entity("ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos.Servico", b =>
                {
                    b.Navigation("MapAtendimentoServicos");
                });
#pragma warning restore 612, 618
        }
    }
}
