using System.Text;
using API.Configurations;
using API.Middlewares;
using Application.Contracts;
using Application.Services;
using AspNetCoreRateLimit;
using Domain.ConfigurationModels;
using Domain.Entities;
using Infrastructure;
using Infrastructure.Contracts;
using Infrastructure.Data.Persistence;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace API.Extensions;

public static class ServiceExtensions
{
    public static void ConfigureCors(this IServiceCollection serviceCollection) => 
        serviceCollection.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy", builder =>
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .WithExposedHeaders("X-Pagination"));
        });

    public static void ConfigureIisIntegration(this IServiceCollection serviceCollection) =>
        serviceCollection.Configure<IISOptions>(options => { });

    public static void ConfigureLoggerService(this IServiceCollection serviceCollection) =>
        serviceCollection.AddSingleton<ILoggerManager, LoggerManager>();

    public static void ConfigureRepositoryManager(this IServiceCollection serviceCollection) =>
        serviceCollection.AddScoped<IRepositoryManager, RepositoryManager>();
    
    public static void ConfigureServiceManager(this IServiceCollection serviceCollection) =>
        serviceCollection.AddScoped<IServiceManager, ServiceManager>();

    public static void ConfigureSqlContext(this IServiceCollection serviceCollection, IConfiguration configuration) =>
        serviceCollection.AddDbContext<AppDbContext>(
            opts =>
            {
                opts.UseNpgsql(configuration.GetConnectionString("DefaultConnection"));
            });

    public static void ConfigureMvc(this IServiceCollection services)
    {
        //services.AddMvc()
        //    .ConfigureApiBehaviorOptions(o =>
        //    {
        //        o.InvalidModelStateResponseFactory = context => new ValidationFailedResult(context.ModelState);
        //    }).AddFluentValidation(x => {
        //        x.RegisterValidatorsFromAssemblyContaining<UserValidator>();
        //        x.ImplicitlyValidateChildProperties = true;
        //        x.ImplicitlyValidateRootCollectionElements = true;
        //    }
        //    );
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    }
    public static void ConfigureApiVersioning(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddApiVersioning(opt =>
        {
            opt.AssumeDefaultVersionWhenUnspecified = true;
            opt.DefaultApiVersion = new ApiVersion(1, 0);
            opt.ReportApiVersions = true;
        });
        services.AddVersionedApiExplorer(opt =>
        {
            opt.GroupNameFormat = "'v'VVV";
            opt.SubstituteApiVersionInUrl = true;
        });
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
        services.AddMvcCore().AddApiExplorer();
    }

    public static void ConfigureResponseCaching(this IServiceCollection services) => 
        services.AddResponseCaching();

    public static void ConfigureHttpCacheHeaders(this IServiceCollection services) => 
        services.AddHttpCacheHeaders(expirationOpt =>
        {
            expirationOpt.MaxAge = 65;
            expirationOpt.CacheLocation = CacheLocation.Private;
        }, validationOpt =>
        {

            validationOpt.MustRevalidate = true;
        });

    public static void ConfigureRateLimitingOptions(this IServiceCollection services)
    {
        var rateLimitingRules = new List<RateLimitRule>
        {
            new RateLimitRule
            {
                Endpoint = "*",
                Limit = 10,
                Period = "5m"
            }
        };

        services.Configure<IpRateLimitOptions>(opts =>
        {
            opts.GeneralRules = rateLimitingRules;
        });
        services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
        services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
        services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();
        services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
    }

   

    public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
    {
        var jwtConfiguration = new JwtConfiguration();
        configuration.Bind(jwtConfiguration.Section, jwtConfiguration);
        // var jwtSettings = configuration.GetSection("JwtSettings");
        // var secretKey = Environment.GetEnvironmentVariable("JWTSECRET");
        // var secretKey = jwtSettings["secret"];
        var secretKey = jwtConfiguration.Secret;

        services.AddAuthentication(opt =>
        {
            opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtConfiguration.ValidIssuer,
                ValidAudience = jwtConfiguration.ValidAudience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfiguration.Secret))
            };
        });
    }

    public static void ConfigureSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(s =>
        {
            s.OperationFilter<RemoveVersionFromParameter>();
            s.SwaggerDoc("v2", new OpenApiInfo { Title = "Shop RU webapi", Version = "v2" });

            var xmlFile = $"{typeof(Presentation.AssemblyReference).Assembly.GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            //s.IncludeXmlComments(xmlPath);

            s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Description = "Add JWT with Bearer",
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer"
            });

            s.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Name = "Bearer"
                    },
                    new List<string>()
                }
            });
        });
    }

}