using ClothingStore;
using ClothingStore.Profiles;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAutoMapper(typeof(BrandProfile)); 
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();