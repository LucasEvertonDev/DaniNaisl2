﻿using System.ComponentModel;
using ControlServices.Application.Mediator.Queries.Usuarios.GetUsuarioQuery;
using Microsoft.AspNetCore.Mvc;

namespace ControlServices.Application.Mediator.Queries.Usuarios.GetUsuarioQuerry;
public class GetUsuariosQuery : IRequest<Result>, IHandler<GetUsuariosQueryHandler>
{
    [DefaultValue("1")]
    [FromRoute(Name = "pagenumber")]
    public int PageNumber { get; set; }

    [DefaultValue("10")]
    [FromRoute(Name = "pagesize")]
    public int PageSize { get; set; }

    [FromQuery(Name = "nome")]
    public string Nome { get; set; }

    [FromQuery(Name = "email")]
    public string Email { get; set; }
}