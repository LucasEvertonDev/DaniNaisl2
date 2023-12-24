﻿using System.Text;
using Architecture.WebApi.Endpoints;
using Authentication.Application.Domain;
using Authentication.Application.Domain.Structure.Models;
using Authentication.Infra.IoC;
using Authentication.WebApi.Structure.Filters;
using Authentication.WebApi.Structure.Middlewares;
using Authentication.WebApi.Structure.PolicyProviders;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using Prometheus;
using Swashbuckle.AspNetCore.Filters;

namespace Authentication.WebApi.Structure.Startup;

public class Startup(IConfiguration configuration)
{
    public void ConfigureServices(IServiceCollection services)
    {
        var appSettings = new AppSettings();

        services.AddSingleton<ExceptionFilter>();

        configuration.Bind(appSettings);

        services.AddMvc(x =>
        {
            if (appSettings.FilterExceptions)
            {
                x.Filters.Add(typeof(ExceptionFilter));
            }

            x.Filters.Add(new ProducesResponseTypeAttribute(typeof(ResponseError<Dictionary<object, object[]>>), 400));
        }).ConfigureApiBehaviorOptions(x => { x.SuppressModelStateInvalidFilter = true; });

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();

        // Binding model
        services.Configure<ApiBehaviorOptions>(options => options.SuppressInferBindingSourcesForParameters = true);

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Jwt.Key)),
                    ClockSkew = TimeSpan.Zero,
                };
            });

        services.AddSwaggerGen(c =>
        {
            c.RegisterSwaggerDefaultConfig(true, appSettings.Swagger.FlowLogin);
        });

        services.AddSingleton<AppSettings>(appSettings);

        services.AddSingleton<IAuthorizationPolicyProvider, DatabasePolicyProvider>();

        services.AddSwaggerExamples();

        services.AddInfraStructure(appSettings);
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }

        app.UseMiddleware<AuthUnauthorizedMiddleware>();

        app.UseMiddleware<RequestResponseLoggingMiddleware>();

        app.UseHttpsRedirection();

        app.UseRouting();

        app.UseAuthentication();

        app.UseAuthorization();

        app.UseMetricServer();

        app.UseHttpMetrics();

        app.UseCors(x => x
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader());

        app.UseSwagger();

        app.UseSwaggerUI();

        app.UseEndpoints(endpoints =>
        {
            endpoints
                .MapGroup("api/v1/")
                .WithOpenApi()
                .AddAuthEndpoints("auth", "Auth")
                .AddUsuariosEndpoint("usuarios", "Usuarios")
                .AddClientesEndpoint("clientes", "Clientes")
                ;
        });
    }
}
