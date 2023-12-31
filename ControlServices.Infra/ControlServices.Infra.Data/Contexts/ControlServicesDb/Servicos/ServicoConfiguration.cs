﻿using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ControlServices.Infra.Data.Contexts.ControlServicesDb.Servicos;
public class ServicoConfiguration : IEntityTypeConfiguration<Servico>
{
    public void Configure(EntityTypeBuilder<Servico> builder)
    {
        builder.ToTable("Servicos");

        builder.HasKey(u => u.Id);

        builder.Property(u => u.Id)
            .ValueGeneratedOnAdd()
            .IsRequired();

        builder.Property(u => u.Nome)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(u => u.Descricao)
            .HasMaxLength(200);

        builder.Property(u => u.Situacao)
            .HasConversion<int?>()
            .IsRequired();

        builder.Property(u => u.DataCriacao)
            .HasColumnType("datetime")
            .HasDefaultValueSql("CURRENT_TIMESTAMP");

        builder.Property(u => u.DataAtualizacao);
    }
}