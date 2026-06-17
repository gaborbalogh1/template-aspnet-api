using Serilog;
Log.Logger = new LoggerConfiguration().WriteTo.Console().CreateLogger();
var builder = WebApplication.CreateBuilder(args);
builder.Host.UseSerilog();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
var app = builder.Build();
if (app.Environment.IsDevelopment()) { app.UseSwagger(); app.UseSwaggerUI(); }
app.MapHealthChecks("/healthz");
app.MapGet("/", () => Results.Ok(new { service = Environment.GetEnvironmentVariable("SERVICE_NAME") ?? "api", version = "1.0.0" }));
app.Run();
