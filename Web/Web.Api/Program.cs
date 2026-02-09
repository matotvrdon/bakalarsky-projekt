using Web.IoC;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
if (string.IsNullOrWhiteSpace(connectionString))
{
    throw new InvalidOperationException("DefaultConnection is not configured. Set ConnectionStrings:DefaultConnection.");
}
builder.Services.AddInformaticsDi(connectionString);

var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Informatics.Api v1"));
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();
