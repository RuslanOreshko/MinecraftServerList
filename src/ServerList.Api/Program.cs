using Scalar.AspNetCore;
using ServerList.Application;
using ServerList.Infrastructure;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOpenApi();
builder.Services.AddControllers();

// Build in other DI
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);


var app = builder.Build();


if (app.Environment.IsDevelopment())
{   
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.MapControllers();
app.Run();

