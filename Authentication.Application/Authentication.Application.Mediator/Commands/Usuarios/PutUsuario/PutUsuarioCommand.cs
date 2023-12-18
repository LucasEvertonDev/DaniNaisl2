﻿using System.Text.Json.Serialization;
using Authentication.Application.Domain.Structure.Models;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Application.Mediator.Commands.Usuarios.PutUsuario;
public class PutUsuarioCommand : IRequest<Result>, IHandler<PutUsuarioCommandHandler>, IValidationAsync<PutUsuarioCommandValidator>
{
    [JsonIgnore]
    [FromRoute(Name = "id")]
    public virtual Guid Id { get; set; }

    [FromBody]
    public UsuarioEdit Body { get; set; }
}

public class UsuarioEdit
{
    public string Email { get; set; }

    public string Nome { get; set; }
}
