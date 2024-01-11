using Business.Repositories;
using Business.Entities;
using infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using UsuarioProject.Services;
using AutoMapper;
using Business;
using UsuarioProject.Settings;
using Business.Setting;
using System.Security.Cryptography;
using PopsyMarket.Business;
using UsuarioProject.Business;

var builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuration = builder.Configuration;

builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();
builder.Services.AddScoped<IBusinessRepository, BusinessRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<ILoginBusiness, LoginBusiness>();
builder.Services.AddScoped<IEncryptService, EncryptService>();

//Bearer settings
builder.Services.AddAuthorization();
builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.Authority = "https://localhost:7291"; // https://localhost:7291
        options.Audience = "api-resource"; 
    });

//Token configuration settings
builder.Services.AddSingleton<TokenSettings>();
TokenSettings tokenSettings = builder.Services.BuildServiceProvider().GetRequiredService<TokenSettings>();
configuration.GetSection("TokenSettings").Bind(tokenSettings);

// Random key generation
byte[] randomKey = new byte[32];
using (var rng = RandomNumberGenerator.Create())
{
    rng.GetBytes(randomKey);
}
builder.Services.AddSingleton(randomKey);

builder.Services.AddDbContext<UsuarioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("UsuarioContext")))
    .AddAutoMapper(typeof(AplicationServiceExtension))
    .AddScoped<CredencialesKeys>()
    .AddScoped<IJwtService, JwtService>()
    .AddScoped<IEncryptService, EncryptService>()
    .AddScoped<ILoginBusiness, LoginBusiness>()
    .AddSingleton(tokenSettings);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.UseAuthentication();
app.MapControllers();
app.Run();
