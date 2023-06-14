using GigaChat.Contracts.Common.Routes;
using GigaChat.Core;
using GigaChat.Data;
using GigaChat.Server.Authentication;
using GigaChat.Server.Configuration;
using GigaChat.Server.Cors;
using GigaChat.Server.HealthChecking;
using GigaChat.Server.Logging;
using GigaChat.Server.Mapping;
using GigaChat.Server.ProblemDetails;
using GigaChat.Server.SignalR;
using GigaChat.Server.Swagger;

using Hellang.Middleware.ProblemDetails;

using Serilog;

var builder = WebApplication.CreateBuilder(args);

var configuration = builder.Configuration;
{
    AppConfigurationConfig.Configure(configuration, builder.Environment);
}

var host = builder.Host;
{
    host.UseSerilog(SerilogConfig.Configure);
}

var services = builder.Services;
{
    services.AddCore(configuration, new[] { typeof(Program).Assembly });
    services.AddData(configuration);
    services.AddMapper();
    services.AddHealthChecking();
    services.AddGigaChatAuthentication(configuration);
    services.AddGigaChatSignalR();
    services.AddGigaChatCors();
    services.AddProblemDetails(ProblemDetailsConfig.Configure);
    services.AddControllers();
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(options => SwaggerGenConfig.Configure(options, configuration));
}

var app = builder.Build();
{
    app.UseProblemDetails();

    app.UseGigaChatCors(builder.Environment);

    app.UseStaticFiles();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
        app.UseDeveloperExceptionPage();
    }
    else
    {
        app.UseExceptionHandler(ServerRoutes.Controllers.ErrorController);
    }

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapHealthChecks(ServerRoutes.HealthCheck, HealthCheckingConfig.Options);
    app.MapControllers();
    app.MapHubs();
}

app.Run();