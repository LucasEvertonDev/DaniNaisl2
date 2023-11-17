﻿using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
using Authentication.Application.Domain.Plugins.Cryptography;

namespace Authentication.Application.Mediator.Commands.Usuarios.PostUsuario;

public class PostUsuarioCommandHandler(
    IServiceProvider serviceProvider,
    IPasswordHash passwordHash) : BaseHandler(serviceProvider), IRequestHandler<PostUsuarioCommand, Result>
{
    public async Task<Result> Handle(PostUsuarioCommand request, CancellationToken cancellationToken)
    {
        if (await EmailJaCadastrado(request.Email))
        {
            return Result.Failure<PostUsuarioCommandHandler>(UsuarioFailures.EmailExistente);
        }

        var chaveHash = passwordHash.GeneratePasswordHash();

        var usuario = new Usuario(
            email: request.Email,
            senha: passwordHash.EncryptPassword(request.Senha, chaveHash),
            chave: chaveHash);

        await UnitOfWork.UsuarioRepository.CreateAsync(usuario);

        return Result;
    }

    private async Task<bool> EmailJaCadastrado(string email)
    {
        return (await UnitOfWork.UsuarioRepository
            .FirstOrDefaultAsync(
                usuario => usuario.Email == email)) != null;
    }
}