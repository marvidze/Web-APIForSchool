using School.Application.Interfaces;
using School.Application.Services;
using School.Core.Entities;
using School.Core.Interfaces;
using School.Core.Models;
using School.Persistence;
using School.Persistence.Repositories;

namespace School.API.Extensions
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddMongoDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var collectionNames = new Dictionary<Type, string>
            {
                {typeof(Student), "students" },
                {typeof(Group), "groups" },
            };

            var connectionString = configuration.GetConnectionString("MongoDB");
            var databaseName = configuration["DatabaseName"] ?? "SchoolDB";

            services.AddSingleton<MongoDbContext>(provider =>
                new MongoDbContext(connectionString, databaseName, collectionNames));

            return services;
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<IGroupRepository, GroupRepository>();
           
            return services;
        }

        public static IServiceCollection AddServices(this IServiceCollection services)
        {
            services.AddScoped<IStudentService, StudentService>();
            services.AddScoped<IGroupService, GroupService>();
            
            return services;
        }

        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();
            
            return services;
        }
    }
}
