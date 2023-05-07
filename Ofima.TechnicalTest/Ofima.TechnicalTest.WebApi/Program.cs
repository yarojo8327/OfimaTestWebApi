using FluentValidation;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

using NLog;
using NLog.Web;

using Ofima.TechnicalTest.Common;
using Ofima.TechnicalTest.Common.Validators;
using Ofima.TechnicalTest.Infraestructure;
using Ofima.TechnicalTest.Infraestructure.Interfaces;
using Ofima.TechnicalTest.Service;
using Ofima.TechnicalTest.Service.Interfaces;
using Ofima.TechnicalTest.WebApi.Handlers;
using Ofima.TechnicalTest.WebApi.Models;

using System;
using System.Reflection;
using System.Text;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();
logger.Debug("Program has init");

try
{
    var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";
    var builder = WebApplication.CreateBuilder(args);
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    builder.Services.AddCors(options =>
    {
        options.AddPolicy(name: MyAllowSpecificOrigins,
        policy =>
        {
            policy.WithOrigins("*")
            .AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
        });
    });


    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(o =>
    {
        o.SaveToken = true;
        o.TokenValidationParameters = new TokenValidationParameters
        {
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey
            (Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = false,
            ValidateIssuerSigningKey = true,

        };
    });

    builder.Services.AddSwaggerGen(option =>
    {
        option.SwaggerDoc("v1", new OpenApiInfo { Title = "Ofima.TechnicalTest.WebApi", Version = "v1" });
        option.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml"));
        option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Ingrese un token válido",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });
        option.AddSecurityRequirement(new OpenApiSecurityRequirement
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
    });


    // Add services to the container.
    
    builder.Services.AddScoped<IValidator<RegisterPlayer>, PlayerValidator>();

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();

    builder.Services.AddAutoMapper(typeof(MapperProfile));
    builder.Services.AddTransient<ExceptionHandlingMiddleware>();

    builder.Services.AddDbContext<DataBaseContext>(opt => opt.UseSqlServer(
       builder.Configuration.GetConnectionString("ConnectionStringSQLServer")), ServiceLifetime.Transient);

    builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
    builder.Services.AddScoped<UnitOfWork, UnitOfWork>();
    builder.Services.AddScoped<DataBaseContext, DataBaseContext>();
    builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
    builder.Services.AddTransient(typeof(IRepository<>), typeof(Repository<>));
    builder.Services.AddTransient<IPlayerService, PlayerService>();
    builder.Services.AddTransient<IMoveService, MoveService>();
    builder.Services.AddTransient<IGameService, GameService>();



    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
    }


    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseHttpsRedirection();
    app.UseCors(MyAllowSpecificOrigins);

   // app.UseAuthentication();
    app.UseAuthorization();
    app.UseMiddleware<ExceptionHandlingMiddleware>();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    logger.Error(ex, "Program has stopped because rhere was an exception");
    throw;
}
finally
{
    LogManager.Shutdown();
}
