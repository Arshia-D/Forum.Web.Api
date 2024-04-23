using Forum.Application;
using Forum.Application.Repositories;
using Forum.Infrastructure;
using Forum.Infrastructure.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Forum.Web.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddScoped<ICommentRepository>(provider =>
    RepositoryFactory.CreateCommentRepository(provider.GetRequiredService<DatabaseContext>()));

            builder.Services.AddScoped<ITopicRepository>(provider =>
                RepositoryFactory.CreateTopicRepository(provider.GetRequiredService<DatabaseContext>()));


            builder.Configuration.AddEnvironmentVariables();

            builder.Services.AddControllers();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddApplicationLayer();
            builder.Services.AddDatabase(builder.Configuration);

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseAuthorization();
            app.MapControllers();

            app.UseMigrations();
            app.Run();
        }
    }
}