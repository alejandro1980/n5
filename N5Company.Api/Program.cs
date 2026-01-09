//using N5Company.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using N5Company.Application.Elastic;
using N5Company.Application.Handlers;
using N5Company.Application.Interfaces;
using N5Company.Infrastructure;
using N5Company.Infrastructure.Elasticsearch;
using N5Company.Infrastructure.Persistence;
using N5Company.Infrastructure.Repositories;
using Nest;



var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<PermissionsDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IPermissionRepository, PermissionRepository>();
builder.Services.AddScoped<RequestPermissionHandler>();
builder.Services.AddScoped<ModifyPermissionHandler>();
builder.Services.AddSingleton<ElasticsearchService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddSingleton<IElasticClient>(_ =>
{
    var settings = new ConnectionSettings(
        new Uri("http://localhost:9200"))
        .DefaultIndex("permissions-operations");

    return new ElasticClient(settings);
});

builder.Services.AddScoped<IElasticLogger, ElasticLogger>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("ReactLocal", policy =>
        policy
            .AllowAnyHeader()
            .AllowAnyMethod()
            .WithOrigins("http://localhost:5173"));
});

var app = builder.Build();




if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("ReactLocal");
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
