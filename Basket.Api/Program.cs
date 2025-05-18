var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var services = builder.Services;
services.AddCarter();
services.AddMediatR(config =>
{
    var assembly = typeof(Program).Assembly;
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});


var app = builder.Build();

app.MapCarter();

app.MapGet("/", () => "Hello World!");

app.Run();
