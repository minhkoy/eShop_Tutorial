var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddCarter();
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(typeof(Program).Assembly);

});
builder.Services.AddMarten(o =>
{
    o.Connection(builder.Configuration.GetConnectionString("Database")!);
}).UseLightweightSessions();

var app = builder.Build();


//app.MapDefaultEndpoints();

//app.MapGet("/", () => "Hello World");

app.MapCarter();

app.Run();
