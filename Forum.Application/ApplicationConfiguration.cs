using Forum.Application.Repositories;
using Forum.Application.Services;
using Microsoft.Extensions.DependencyInjection;



namespace Forum.Application
{
    public static class ApplicationConfiguration
    {
        public static IServiceCollection AddApplicationLayer(
            this IServiceCollection services)
        {

            services.AddScoped<CommentService>();
            services.AddScoped<TopicService>();
            services.AddScoped<UserService>();
            services.AddScoped<AuthenticationService>();
            services.AddSingleton<PasswordService>();


            return services;
        }
    }
}
