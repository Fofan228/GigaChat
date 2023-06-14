using GigaChat.Server.Swagger.Settings;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace GigaChat.Server.Swagger;

public static class SwaggerGenConfig
{
    public static void Configure(SwaggerGenOptions options, IConfiguration configuration)
    {
        AddSwaggerDoc(options, configuration);
        AddSecurityDefinition(options);
        options.SupportNonNullableReferenceTypes();
        options.AddSignalRSwaggerGen();
    }

    private static void AddSecurityDefinition(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = JwtBearerDefaults.AuthenticationScheme
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                new string[]{}
            }
        });
    }

    private static void AddSwaggerDoc(SwaggerGenOptions options, IConfiguration configuration)
    {
        var swaggerGenOptions = configuration.GetSection(SwaggerGenSettings.SectionName)
            .Get<SwaggerGenSettings>() ?? throw new NullReferenceException();

        var assemblyName = typeof(SwaggerGenConfig).Assembly.GetName();

        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = swaggerGenOptions.Title,
            Description = swaggerGenOptions.Description,
            Version = assemblyName.Version?.ToString(),
            Contact = new OpenApiContact
            {
                Name = swaggerGenOptions.Contact.Name,
                Url = swaggerGenOptions.Contact.Url,
                Email = swaggerGenOptions.Contact.Email
            }
        });

        var xmlFile = $"{assemblyName.Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);
    }
}