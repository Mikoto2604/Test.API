using PaymentApp.Api.Extentions;
using Microsoft.EntityFrameworkCore;
using PaymentApp.Infrastructure.Extentions;
using PaymentApp.Infrastructure.Drivers.DbContexts;
using PaymentApp.Application.Extentions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApiServices(builder.Configuration);
builder.Services.AddAppication(builder.Configuration);
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();
using(var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<PgDbContext>();
    db.Database.Migrate();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    app.UseReDoc(c =>
    {
        c.SpecUrl = "swagger/v1/swagger.json";
        c.RoutePrefix = "";
        c.DocumentTitle = "API Документация";
    });
}

app.UseRouting();

app.UseCors(opt =>
{
    opt.AllowAnyHeader();
    opt.AllowAnyMethod();
    opt.AllowAnyOrigin();
});

app.UseAuthentication();
app.UseMiddleware<TokenValidationMiddleware>();
app.UseAuthorization();

app.MapControllers();

app.Run();
