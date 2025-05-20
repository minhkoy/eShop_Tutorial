var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();

var services = builder.Services;
services.AddCarter();

var assembly = typeof(Program).Assembly;
services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));
});
services.AddValidatorsFromAssembly(assembly);

var app = builder.Build();

app.MapCarter();

app.Run();
