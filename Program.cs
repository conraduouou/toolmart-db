using ToolMart.Models;
using ToolMart.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Different collections integration...
builder.Services.Configure<CollectionSettings>(builder.Configuration.GetSection("ItemsCollection"));
builder.Services.AddSingleton<Service<Item>>();
builder.Services.Configure<CollectionSettings>(builder.Configuration.GetSection("UsersCollection"));
builder.Services.AddSingleton<Service<User>>();
builder.Services.Configure<CollectionSettings>(builder.Configuration.GetSection("TransactionsCollection"));
builder.Services.AddSingleton<Service<Transaction>>();
builder.Services.Configure<CollectionSettings>(builder.Configuration.GetSection("ReviewsCollection"));
builder.Services.AddSingleton<ReviewServices>();

/*
string basePath = Directory.GetCurrentDirectory();

builder.Configuration.SetBasePath(basePath);
builder.Configuration.AddEnvironmentVariables();
*/

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
