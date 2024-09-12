using Dagable.Api.Startup;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Dagable.DataAccess.Migrations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterApplicationServices(builder);

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<DagableDbContext>();
    db.Database.Migrate();
}

app.ConfigureMiddleware();
app.RegisterEndpoints();

app.Run();