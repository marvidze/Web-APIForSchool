using School.API.Extensions;
using School.API.GraphQL;
using ChilliCream.Nitro.App;
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
        builder.Services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
            .AddTypeExtension<StudentResolvers>()
            .AddTypeExtension<GroupResolvers>();

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
        app.MapGraphQL()
            .WithOptions(options =>
            {
                options.Tool.ServeMode = ServeMode.Embedded;
            });
        app.MapMetrics();

        app.Run();
    }
}


