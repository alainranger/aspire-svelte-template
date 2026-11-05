var builder = WebApplication.CreateBuilder(args);

builder.AddServiceDefaults();
builder.Services.AddProblemDetails();
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.MapGet("/", () => Results.Ok(new { name = "api", status = "running" }));

app.MapGet("/api/hello", () => Results.Ok(new
{
    message = "Bonjour depuis ASP.NET Core !"
}))
.WithName("GetHello");

app.MapDefaultEndpoints();

app.Run();
