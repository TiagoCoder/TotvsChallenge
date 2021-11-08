using Microsoft.Extensions.DependencyInjection;
using MediatR;
using System.Reflection;

namespace CodeChallenge.Application
{
    public static class ApplicationServiceRegistration
    {
        /// <summary>
        /// Injeção dos serviços da camada aplicacional (Application Layer)
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
