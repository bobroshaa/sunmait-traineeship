using System.Reflection;
using ClothingStore.Application.Profiles;
using ClothingStore.WebAPI;
using ClothingStore.WebAPI.Configuration;
using ClothingStore.WebAPI.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

var corsPolicyConfiguration = builder
    .Configuration
    .GetSection(CorsPolicyConfiguration.SectionName)
    .Get<CorsPolicyConfiguration>();

var jwtConfigurationSection = builder.Configuration.GetSection(JwtConfiguration.SectionName);
var jwtConfiguration = jwtConfigurationSection.Get<JwtConfiguration>();

builder.Services.Configure<JwtConfiguration>(jwtConfigurationSection);

builder.Services
    .AddCustomCors(corsPolicyConfiguration)
    .AddCustomAuthentication(jwtConfiguration)
    .AddCustomAuthorization()
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
builder.Services.AddTransient<JwtGenerator>();

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Clothing Store API v1"); });
}

app.UseCors(corsPolicyConfiguration.Name);

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.UseStaticFiles();

app.Run();