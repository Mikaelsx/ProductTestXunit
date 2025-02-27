using ApiProducts.Interface;
using ApiProducts.Repositories;
using ApiProducts.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Dependence Injection
builder.Services.AddScoped<IProductsRepository, ProductRepository>();

builder.Services.AddSingleton<MongoDbServiceImplementation>();
//builder.Services.AddScoped<IProductsRepository>(sp =>
//{
//    var connectionString = builder.Configuration["ConnectionStrings:MongoDB"];
//    var databaseName = builder.Configuration["MongoDB:Database"];
//    return new ProductRepository(connectionString, databaseName);
//});


// Cors
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
            .AllowAnyHeader()
            .AllowAnyMethod();
        });
});

//Swagger
builder.Services.AddSwaggerGen(options =>
{

    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "API Minimal with MongoDb",
        Description = "Backend API",
        Contact = new OpenApiContact
        {
            Name = "Senai Informática"
        }
    });
});

var app = builder.Build();

// Swagger
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Use Services
app.UseCors("CorsPolicy");
app.UseHttpsRedirection();
app.UseAuthorization();

//Map Controller
app.MapControllers();

// Run Project
app.Run();
