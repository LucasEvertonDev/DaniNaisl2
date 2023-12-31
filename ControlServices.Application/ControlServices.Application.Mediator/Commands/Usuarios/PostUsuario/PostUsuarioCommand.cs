﻿using ControlServices.Application.Domain.Structure.Models;

namespace ControlServices.Application.Mediator.Commands.Usuarios.PostUsuario;

public class PostUsuarioCommand : IRequest<Result>, IHandler<PostUsuarioCommandHandler>, IValidationAsync<PostUsuarioCommandValidator>
{
    public string Email { get; set; }

    public string Senha { get; set; }

    public string Nome { get; set; }
}
