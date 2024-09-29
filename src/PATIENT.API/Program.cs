using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PATIENT.APPLICATION.Consumers;
using PATIENT.APPLICATION.Patient.CreatePatient;
using PATIENT.APPLICATION.Patient.GetAllPatient;
using PATIENT.APPLICATION.Patient.GetById;
using PATIENT.INFRA.context;
using PATIENT.INFRA.RabbitMq;
using PATIENT.INFRA.Repositories;
using PATIENT.INFRA.Repositories.Common;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddDbContext<PATIENTCONTEXT>(options =>
      options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddTransient<IRequestHandler<GetAllPatientQuery, IEnumerable<GetAllPatientResponse>>, GetAllPatientQueryHandler>();
builder.Services.AddTransient<IRequestHandler<GetByIdQuery, GetByIdResponse>, GetByIdQueryHandler>();

builder.Services.AddHostedService<CreatePatientConsumer>();
builder.Services.AddScoped<IMediator, Mediator>();
builder.Services.AddScoped<IPatientRepository, PatientRepository>();
builder.Services.AddSingleton<ICreateChannelRabbitMql, CreateChannelRabbitMql>();
builder.Services.AddTransient<IRequestHandler<CreatePatientCommand, CreatePatientResponse>, CreatePatientCommandHandler>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme =
        JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("9ASHDA98H9ah9ha9H9A89n0fasjdhksajhduiwqadskjhkSKJD")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Schedule API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        In = Microsoft.OpenApi.Models.ParameterLocation.Header,
        Description = "Entre com o token JWT",
        Name = "Authorization",
        Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement
    {
        {
            new Microsoft.OpenApi.Models.OpenApiSecurityScheme
            {
                Reference = new Microsoft.OpenApi.Models.OpenApiReference
                {
                    Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
