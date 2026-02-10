using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.EntityFrameworkCore;
using Web.DataAccess.Data;
using Web.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
var corsOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();

builder.Services.AddCors(o =>
{
    o.AddPolicy(CorsPolicy, p =>
        p.WithOrigins(
            "https://tvojadomena.sk",
            "http://tvojadomena.sk"
        )
        .AllowAnyHeader()
        .AllowAnyMethod()
    );
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("DefaultConnection is not configured. Set ConnectionStrings:DefaultConnection.");
}
builder.Services.AddInformaticsDi(connectionString);

var app = builder.Build();

var shouldMigrate = app.Environment.IsDevelopment() ||
    app.Configuration.GetValue<bool>("Database:AutoMigrate");
if (shouldMigrate)
{
    using var scope = app.Services.CreateScope();
    var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    dbContext.Database.Migrate();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto,
    KnownNetworks = { },
    KnownProxies = { }
});

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Informatics.Api v1"));

var useHttpsRedirection = app.Configuration.GetValue<bool?>("HttpsRedirection:Enabled") ??
    app.Environment.IsProduction();
if (useHttpsRedirection)
{
    app.UseHttpsRedirection();
}
app.UseCors(CorsPolicy);
app.UseAuthorization();
app.MapControllers();
app.Run();
