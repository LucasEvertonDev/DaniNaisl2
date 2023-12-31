﻿using ControlServices.Application.Domain;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Atendimentos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Audits;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Audits.Enums;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.MapAtendimentosServicos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Servicos;
using ControlServices.Application.Domain.Contexts.ControlServicesDb.Usuarios;
using ControlServices.Application.Domain.Plugins.JWT.Contants;
using ControlServices.Application.Domain.Structure.Extensions;
using ControlServices.Infra.Data.Structure.AuditLog;
using ControlServices.Infra.Data.Structure.Exceptions;
using Microsoft.AspNetCore.Http;

namespace ControlServices.Infra.Data.Contexts.ControlServicesDb;

public class ControlServicesDbContext : DbContext
{
    private readonly AppSettings _appSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public ControlServicesDbContext(
        DbContextOptions<ControlServicesDbContext> options,
        AppSettings appSettings,
        IHttpContextAccessor httpContextAccessor)
        : base(options)
    {
        _appSettings = appSettings;
        _httpContextAccessor = httpContextAccessor;

        this.Database.Migrate();
    }

    public virtual DbSet<Usuario> Usuarios { get; set; }

    public virtual DbSet<Custo> Custos { get; set; }

    public virtual DbSet<Audit> Audits { get; set; }

    public virtual DbSet<Cliente> Clientes { get; set; }

    public virtual DbSet<Servico> Servicos { get; set; }

    public virtual DbSet<Atendimento> Atendimentos { get; set; }

    public virtual DbSet<MapAtendimentoServico> MapAtendimentosServicos { get; set; }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        await BeforeSaveChanges();
        return await base.SaveChangesAsync(cancellationToken);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        if (!_appSettings.DatabaseInMemory)
        {
            // Apply configurations automatic
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ControlServicesDbContext).Assembly);
        }
    }

    private async Task BeforeSaveChanges()
    {
        try
        {
            List<Audit> audits = [];
            var userId = _httpContextAccessor?.HttpContext?.User?.Identity?.GetUserClaim(JwtUserClaims.Id) ?? "Anonymous";

            ChangeTracker.DetectChanges();

            foreach (var entry in ChangeTracker.Entries())
            {
                if (entry.Entity is Audit || entry.State is EntityState.Detached or EntityState.Unchanged)
                {
                    continue;
                }

                if (entry.Entity is IEntity entity)
                {
                    entity.AtualizarDataDeEstados((Application.Domain.Structure.Enuns.EntityState)entry.State);
                }

                var auditEntry = new AuditEntry(entry) { TableName = entry.Entity.GetType().Name, UserId = userId };

                List<string> ignoreProperties = ["DataCriacao", "DataAtualizacao"];

                foreach (var property in entry.Properties)
                {
                    var propertyName = property.Metadata.Name;

                    if (ignoreProperties.Contains(propertyName))
                    {
                        continue;
                    }

                    if (property.Metadata.IsPrimaryKey())
                    {
                        auditEntry.KeyValues[propertyName] = property.CurrentValue;
                        continue;
                    }

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            auditEntry.AuditType = AuditType.Create;
                            auditEntry.NewValues[propertyName] = property.CurrentValue;
                            break;
                        case EntityState.Deleted:
                            auditEntry.AuditType = AuditType.Delete;
                            auditEntry.OldValues[propertyName] = property.OriginalValue;
                            break;
                        case EntityState.Modified:
                            if (!Equals(property.OriginalValue, property.CurrentValue))
                            {
                                auditEntry.ChangedColumns.Add(propertyName);
                                auditEntry.AuditType = AuditType.Update;
                                auditEntry.OldValues[propertyName] = property.OriginalValue;
                                auditEntry.NewValues[propertyName] = property.CurrentValue;
                            }

                            break;
                    }
                }

                audits.Add(auditEntry.ToAudit());
            }

            await Audits.AddRangeAsync(audits);
        }
        catch (Exception ex)
        {
            throw new AuditLogException(ex.Message);
        }
    }
}
