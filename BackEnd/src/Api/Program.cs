using System;
using Application.Configurations;
using Application.Extensions;
using Infra.EF.Data.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);


var connectionString = builder.Configuration.GetConnectionString("DefaultConnectionString");
Console.WriteLine($"Connection String: {connectionString}");

builder.Services.AddDbContext<AppDataContext>(options =>
{
    options.UseSqlite(connectionString);
});

SettingServices(builder, builder.Configuration);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

static void SettingServices(WebApplicationBuilder builder, IConfiguration configuration)
{
    builder.Services.ConfigurationService();
    builder.Services.ConfigurationRepositories();
    builder.AddJwtConfigurations();
    builder.Services.AddIdentityConfiguration();
}