var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);

});

var app = builder.Build();

app.MapDefaultEndpoints();

app.MapCarter();
app.Run();
