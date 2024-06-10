using API.Models;
using API.Services;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure MySQL database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register custom services
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<CategoryService>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<OrderDetailService>();
builder.Services.AddScoped<ShipmentService>();
builder.Services.AddScoped<StockLevelService>();
builder.Services.AddScoped<SupplierService>();
builder.Services.AddScoped<WarehouseService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();

// Add CORS policy
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add logging
builder.Services.AddLogging();

var app = builder.Build();

// Custom middleware for logging
app.Use(async (context, next) =>
{
    var logger = app.Services.GetRequiredService<ILogger<Program>>();

    logger.LogInformation("Handling request: {Method} {URL}", context.Request.Method, context.Request.GetDisplayUrl());

    await next.Invoke();

    logger.LogInformation("Finished handling request.");
});

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// Enable CORS
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
