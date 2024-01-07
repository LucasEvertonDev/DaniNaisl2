﻿using ControlServices.Application.Domain.Contexts.DbAuth.Usuarios.Results;
using ControlServices.Application.Domain.Structure.Pagination;
using ControlServices.Application.Domain.Structure.Repositories;

namespace ControlServices.Application.Domain.Contexts.DbAuth.Usuarios;
public interface IUsuarioRepository : IRepository<Usuario>
{
    Task<PagedResult<UsuarioModel>> GetUsuariosAsync(string email, string nome, int pageNumber, int pageSize, CancellationToken cancellationToken = default);
}
