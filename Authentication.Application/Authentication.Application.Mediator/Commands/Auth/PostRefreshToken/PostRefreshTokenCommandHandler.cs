﻿using Authentication.Application.Domain.Contexts.DbAuth.Usuarios;
using Authentication.Application.Domain.Contexts.DbAuth.Usuarios.Models;
using Authentication.Application.Domain.Plugins.JWT.Conts;
using Authentication.Application.Domain.Structure.Extensions;

namespace Authentication.Application.Mediator.Commands.Auth.PostRefreshToken;

public class PostRefreshTokenCommandHandler(
    IServiceProvider serviceProvider,
    ITokenService tokenService) : BaseHandler(serviceProvider), IRequestHandler<PostRefreshTokenCommand, Result>
{
    public async Task<Result> Handle(PostRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var usuario = await UnitOfWork.UsuarioRepository
            .FirstOrDefaultAsync(
                usuario => usuario.Id == new Guid(Identity.GetUserClaim(JwtUserClaims.Id)));

        if (usuario == null)
        {
            return Result.Failure<PostRefreshTokenCommandHandler>(UsuarioFailures.NaoFoiPossivelRecuperarUsuarioLogado);
        }

        var (token, dataExpiracao) = await tokenService.GenerateToken(usuario);

        return Result.Ok(new TokenModel
        {
            AccessToken = token,
            ExpireDate = dataExpiracao,
            ExpiresIn = (dataExpiracao - DateTime.Now).Minutes
        });
    }
}