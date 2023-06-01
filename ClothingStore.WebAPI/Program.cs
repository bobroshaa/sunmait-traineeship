using ClothingStore.Application.Profiles;
using ClothingStore.WebAPI;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddApplicationDependencies()
        .AddInfrastructure();
    builder.Services.AddControllers();
    builder.Services.AddAutoMapper(typeof(BrandProfile), typeof(ProductProfile), typeof(OrderProfile),
        typeof(OrderItemProfile));
    builder.Services.AddSwaggerGen();
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