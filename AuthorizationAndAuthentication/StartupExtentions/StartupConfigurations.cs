using AuthorizationAndAuthentication.Common.Models;
using AuthorizationAndAuthentication.Repositories.Abstraction.Contracts;
using AuthorizationAndAuthentication.Repositories.Context.Context;
using AuthorizationAndAuthentication.Repositories.Implementation.Concrete;
using AuthorizationAndAuthentication.Services.Abstraction.Contracts;
using AuthorizationAndAuthentication.Services.Implementation.Concrete;
using Microsoft.EntityFrameworkCore;

namespace AuthorizationAndAuthentication.StartupExtentions
{
    public static class StartupConfigurations
    {
        public static IServiceCollection ConfiguringRepositories(this IServiceCollection services)
        {
            services.AddScoped<ITokenRepository, TokenRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            return services;
        }
        public static IServiceCollection ConfiguringServices(this IServiceCollection services)
        {
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IAuthenticationService, AuthenticationService>();
            return services;
        
        }
        public static IServiceCollection ConfiguringDatabaseServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<SqlContext>(options => options.UseSqlServer(configuration.GetSection("Data").GetSection("AuthorizationDatabase").Value), ServiceLifetime.Transient);
            GlobalVar._options = configuration.GetSection("GlobalResources").Get<GlobalResources>();
            return services;

        }
    }
}
