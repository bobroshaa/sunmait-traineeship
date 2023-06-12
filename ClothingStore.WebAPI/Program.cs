using System.Reflection;
using System.Text;
using ClothingStore.Application.Profiles;
using ClothingStore.WebAPI;
using ClothingStore.WebAPI.DependencyInjection;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowOrigin", corsPolicyBuilder =>
        {
            corsPolicyBuilder.WithOrigins("http://localhost:63342")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });
    
    builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
    .AddJwtBearer(jwt =>
        {
            var key = Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JwtConfig:Secret").Value);
            jwt.SaveToken = true;
            jwt.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = true,
                ValidateLifetime = true
            };
        });

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
    app.UseSwaggerUI(options => { options.SwaggerEndpoint("/swagger/v1/swagger.json", "Clothing Store API v1"); });
}

app.UseCors("AllowOrigin");

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.UseStaticFiles();

app.Run();