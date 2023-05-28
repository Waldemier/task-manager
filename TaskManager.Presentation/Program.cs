using TaskManager.Application;
using TaskManager.Infrastructure;
using TaskManager.Presentation.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterCustomMiddlewares();

builder.Services
    .RegisterInfrastructure()
    .RegisterContext(builder.Configuration)
    .MigrateDatabase()
    .RegisterRepositories()
    .RegisterServices()
    .RegisterApplication();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCatchErrorMiddleware();

app.MapControllers();

app.Run();