﻿using ControlServices.Application.Domain.Contexts.ControlServicesDb.Custos;
using Notification.Extensions;

namespace ControlServices.Application.Mediator.Commands.Custos.PostCusto;
public class PostCustoCommandHandler(IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<PostCustoCommand, Result>
{
    public async Task<Result> Handle(PostCustoCommand request, CancellationToken cancellationToken)
    {
        var custo = new Custo(
            data: request.Data,
            valor: request.Valor,
            descricao: request.Descricao);
        if (custo.HasFailures())
        {
            return Result.Failure<PostCustoCommandHandler>(custo);
        }

        await UnitOfWork.CustoRepository.CreateAsync(
            domain: custo,
            cancellationToken: cancellationToken);

        return Result.Ok();
    }
}
