using exercise.wwwapi.EndPoints;
using exercise.wwwapi.Models;
using exercise.wwwapi.Repository;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();

// Add CORS service with policy that allows any origin
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IDatabaseRepository<Todo>, DatabaseRepository<Todo>>();

var app = builder.Build();

// Use the CORS policy
app.UseCors("AllowAll");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.ConfigureTodoAPI();
app.UseHttpsRedirection();
app.MapControllers();
app.Run();