using System.Reflection;
using ClothingStore.Application.Profiles;
using ClothingStore.WebAPI;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplicationDependencies()
        .AddInfrastructure();
    builder.Services.AddControllers();
    builder.Services.AddAutoMapper(typeof(BrandProfile), typeof(ProductProfile), typeof(OrderProfile),
        typeof(OrderItemProfile));
    builder.Services.AddSwaggerGen(c =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clothing Store API", Version = "v1" });
    });
    builder.Services.AddTransient<ExceptionHandlingMiddleware>();
}

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.MapControllers();
app.Run();

