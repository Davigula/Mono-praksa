using Recipe.API;
using Recipe.API.Repository;
using Recipe.API.Repository.Common;
using Recipe.API.Service;
using Recipe.API.Service.Common;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Register services and repositories
builder.Services.AddScoped<IRepository<WeatherForecast>, WeatherForecastRepository>();
builder.Services.AddScoped<IService<WeatherForecast>, WeatherForecastService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
