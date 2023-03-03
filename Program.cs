using ToolMart.Models;
using ToolMart.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(options => options.JsonSerializerOptions.PropertyNamingPolicy = null);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Different collections integration...
builder.Services.Configure<ItemsCollectionSettings>(builder.Configuration.GetSection("ItemsCollection"));
builder.Services.AddSingleton<ItemService>();
builder.Services.Configure<UsersCollectionSettings>(builder.Configuration.GetSection("UsersCollection"));
builder.Services.AddSingleton<UserService>();
builder.Services.Configure<TransactionsCollectionSettings>(builder.Configuration.GetSection("TransactionsCollection"));
builder.Services.AddSingleton<TransactionService>();
builder.Services.Configure<ReviewsCollectionSettings>(builder.Configuration.GetSection("ReviewsCollection"));
builder.Services.AddSingleton<ReviewService>();
builder.Services.Configure<TransactionItemsCollectionSettings>(builder.Configuration.GetSection("TransactionItemsCollection"));
builder.Services.AddSingleton<TransactionItemService>();
builder.Services.Configure<CartItemsCollectionSettings>(builder.Configuration.GetSection("CartItemsCollection"));
builder.Services.AddSingleton<CartItemService>();

/*
string basePath = Directory.GetCurrentDirectory();

builder.Configuration.SetBasePath(basePath);
builder.Configuration.AddEnvironmentVariables();
*/

var app = builder.Build();

app.Configuration.GetConnectionString("");

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
app.UseSwagger();
app.UseSwaggerUI();
// }

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
