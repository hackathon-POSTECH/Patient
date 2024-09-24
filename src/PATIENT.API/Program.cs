using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using PATIENT.APPLICATION.Consumers;
using PATIENT.APPLICATION.Patient.CreatePatient;
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

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
