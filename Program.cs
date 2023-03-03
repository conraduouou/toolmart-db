using ToolMart.Models;
using ToolMart.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<ItemsDatabaseSettings>(builder.Configuration.GetSection("ItemsDatabase"));
builder.Services.AddSingleton<ItemsService>();

string basePath = Directory.GetCurrentDirectory();

builder.Configuration.SetBasePath(basePath);
builder.Configuration.AddEnvironmentVariables();

var app = builder.Build();

app.Configuration.GetConnectionString("");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
