using School.API.Extensions;
using Prometheus;

namespace School.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();
        builder.Services.AddMongoDbContext(builder.Configuration);
        builder.Services.AddRepositories();
        builder.Services.AddServices();
        builder.Services.AddSwagger();

        var app = builder.Build();

        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpMetrics();

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.MapControllers();
        app.MapMetrics();

        app.Run();
    }
}


