using Stroytorg.Host.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddServices(builder.Configuration);

builder.Services.AddJwt(builder.Configuration);
builder.Services.AddCors(options => options.AddPolicy(
    name: "Stroytorg",
    policy =>
    {
        policy.WithOrigins("http://localhost").AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition");
    }));

var app = builder.Build();

app.MigrateDatabase();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stroytorg.API v1");
});

app.UseExceptionHandler();

app.UseCors("Stroytorg");

app.UseAuthorization();

app.MapControllers();

app.Run();
