﻿using ControlServices.Application.Domain.Contexts.ControlServicesDb.Clientes;
using ControlServices.Application.Domain.Structure.Models;
using ControlServices.Application.Domain.Structure.Pagination;
using ControlServices.Application.Mediator.Commands.Custos.PostCusto;
using ControlServices.Application.Mediator.Commands.Custos.PutCusto;
using ControlServices.Application.Mediator.Queries.Clientes.GetClientes;
using ControlServices.Application.Mediator.Queries.Custos;

namespace ControlServices.WebApi.EndPoints;

public static class CustoEndPoints
{
    public static IEndpointRouteBuilder AddCustosEndpoint(this IEndpointRouteBuilder app, string route, string tag)
    {
        var custosEndpoint = app.MapGroup(route).WithTags(tag);

        custosEndpoint.MapPost("/",
            async ([FromServices] IMediator mediator, [FromBody] PostCustoCommand postCustoCommand, CancellationToken cancellationToken) =>
                 await mediator.SendAsync(postCustoCommand, cancellationToken))
            .Authorization<ResponseDto>();

        custosEndpoint.MapGet($"/{Params.GetRoute<GetClientesQuery>()}",
            async ([FromServices] IMediator mediator, [AsParameters] GetCustosQuery getCustosQuery, CancellationToken cancellationToken) =>
                await mediator.SendAsync(getCustosQuery, cancellationToken))
            .Authorization<ResponseDto<PagedResult<Cliente>>>();

        custosEndpoint.MapPut("/{id}",
            async ([FromServices] IMediator mediator, [AsParameters] PutCustoCommand putCustoCommand, CancellationToken cancellationToken) =>
                await mediator.SendAsync(putCustoCommand, cancellationToken))
            .Authorization<ResponseDto>();
        return app;
    }
}
