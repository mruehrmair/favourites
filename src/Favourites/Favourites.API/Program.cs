using Favourites.Data.DbContexts;
using Favourites.Data.Repositories;
using Favourites.Data.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File("logs/bookmarks.txt", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddDbContext<BookmarkDbContext>(
    options => options.UseSqlite(builder.Configuration["ConnectionStrings:BookmarkDbConnectionString"]));
builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBookmarkService, BookmarkService>();

builder.Services.AddControllers(
    options => { options.ReturnHttpNotAcceptable = true; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();