using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TwitterClone.DataAccess;
using TwitterClone.DataAccess.Implementations;
using TwitterClone.DataAccess.Interfaces;

namespace TwitterClone.Helpers
{
    public static class DependencyInjectionHelper
    {
        public static void InjectDbContext(IServiceCollection services, string connectionString)
        {
            services.AddDbContext<TwitterDbContext>(options =>
                options.UseSqlServer(connectionString));
        }

        public static void InjectRepositories(IServiceCollection services)
        {
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<ILikeRepository, LikeRepository>();
        }
    }
}


