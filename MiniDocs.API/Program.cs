using AspNetCoreRateLimit;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using MiniDocs.API.Hubs;
using MiniDocs.API.Services;
using MiniDocs.Application.Documents.Commands;
using MiniDocs.Application.Repos;
using MiniDocs.Infrastructure.Database;
using MiniDocs.Infrastructure.Repos;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IDocumentRepository, DocumentRepository>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateDocumentCommandHandler).Assembly));

builder.Services.AddSignalR();
builder.Services.AddHostedService<DocumentAutosaveService>();
// builder.Services.AddHostedService<AutoArchiverService>();
// builder.Services.AddSingleton<BlobStorageService>();

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//    .AddJwtBearer(options =>
//    {
//        options.Authority = "https://your-auth-server/";
//        options.Audience = "MiniDocsAPI";
//    });

builder.Services.Configure<IpRateLimitOptions>(options =>
{
    options.GeneralRules = new List<RateLimitRule>
    {
        new RateLimitRule
        {
            Endpoint = "*",
            Limit = 1000,
            Period = "1h"
        },
        new RateLimitRule
        {
            Endpoint = "*",
            Limit = 200,
            Period = "1m"
        }
    };
});

builder.Services.AddMemoryCache();

builder.Services.AddInMemoryRateLimiting();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapHub<DocumentHub>("hubs/document");

app.Run();
