﻿using Authentication.Application.Domain.Contexts.DbAuth.Clientes;
using Notification.Extensions;

namespace Authentication.Application.Mediator.Commands.Clientes.PutCliente;
public class PutClienteCommandHandler(
    IServiceProvider serviceProvider) : BaseHandler(serviceProvider), IRequestHandler<PutClienteCommand, Result>
{
    public async Task<Result> Handle(PutClienteCommand request, CancellationToken cancellationToken)
    {
        var cliente = await UnitOfWork.ClienteRepository.FirstOrDefaultAsync(
            where: cliente => cliente.Id == request.Id,
            cancellationToken: cancellationToken);

        if (cliente == null)
        {
            return Result.Failure<PutClienteCommandHandler>(ClienteFailures.ClienteNaoEncontrado);
        }

        cliente.UpdateUsuario(
            cpf: request.Body.CPF,
            nome: request.Body.Nome,
            dataNascimento: request.Body.DataNascimento,
            telefone: request.Body.Telefone);

        if (cliente.HasFailures())
        {
            return Result.Failure<PutClienteCommandHandler>(cliente);
        }

        await UnitOfWork.ClienteRepository.UpdateAsync(
            domain: cliente,
            cancellationToken: cancellationToken);

        return Result.Ok();
    }
}
