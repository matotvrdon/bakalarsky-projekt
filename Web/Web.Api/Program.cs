using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Web.DataAccess.Data;
using Web.IoC;
using Web.Services.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation();
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(x => x.Value?.Errors.Count > 0)
            .ToDictionary(
                kvp => kvp.Key,
                kvp => kvp.Value!.Errors.Select(e => e.ErrorMessage).ToArray()
            );

        return new BadRequestObjectResult(new ValidationProblemDetails(errors)
        {
            Title = "Validation failed",
            Status = StatusCodes.Status400BadRequest
        });
    };
});

var corsPolicy = "AllowAll";
builder.Services.AddCors(options =>
{
    options.AddPolicy(corsPolicy, policy =>
        policy.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
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
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
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

var storageRoot = app.Configuration["FileStorage:RootPath"] ?? "storage";
var resolvedStorageRoot = Path.IsPathRooted(storageRoot)
    ? storageRoot
    : Path.Combine(app.Environment.ContentRootPath, storageRoot);
Directory.CreateDirectory(resolvedStorageRoot);
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(resolvedStorageRoot),
    RequestPath = "/storage"
});

var useHttpsRedirection = app.Configuration.GetValue<bool?>("HttpsRedirection:Enabled") ??
    app.Environment.IsProduction();
if (useHttpsRedirection)
{
    app.UseHttpsRedirection();
}
app.UseCors(corsPolicy);
app.UseAuthorization();
app.MapControllers();
app.Run();
