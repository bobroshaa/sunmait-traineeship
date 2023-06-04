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
    
    builder.Services.AddAutoMapper(
        typeof(BrandProfile),
        typeof(ProductProfile),
        typeof(OrderProfile),
        typeof(OrderItemProfile),
        typeof(AddressProfile),
        typeof(CategoryProfile),
        typeof(OrderHistoryProfile),
        typeof(ReviewProfile),
        typeof(SectionProfile),
        typeof(UserProfile));
    
    builder.Services.AddSwaggerGen(c =>
    {
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        c.IncludeXmlComments(xmlPath);
        c.SwaggerDoc("v1",
            new OpenApiInfo
            {
                Title = "Clothing Store API", Version = "v1",
                Description = "An ASP.NET Core Web API for Clothing Store"
            });
    });
    
    builder.Services.AddTransient<ExceptionHandlingMiddleware>();
}

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"); });
}

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();