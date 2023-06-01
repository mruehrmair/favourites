using Favourites.API;
using Favourites.Data.DbContexts;
using Favourites.Data.Repositories;
using Favourites.Data.Services;
using Microsoft.EntityFrameworkCore;
using Serilog;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.Console()
    .WriteTo.File(Path.Combine(Constants.LogDir, Constants.LogFile), rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

//database
var currentPath = AppDomain.CurrentDomain.BaseDirectory;
var dbPath = Path.Combine(currentPath, Constants.DataDir);
Directory.CreateDirectory(dbPath);
dbPath = Path.Combine(dbPath, Constants.DataFile);
var connectionString = $"Data Source={dbPath}";

// Add services to the container.
builder.Services.AddDbContext<BookmarkDbContext>(
    options => options.UseSqlite(connectionString));
builder.Services.AddScoped<IBookmarkRepository, BookmarkRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IBookmarkService, BookmarkService>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
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

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<BookmarkDbContext>();
    db.Database.Migrate();
}

app.Run();