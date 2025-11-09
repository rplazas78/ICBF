using Infra.Persistencia.Contexto;
using Infra.Persistencia.Options;
using Microsoft.Extensions.DependencyInjection;



namespace Infra.Persistencia
{
    public static class Dependencias
    {
        public static IServiceCollection agregarRepositorios(this IServiceCollection services, Action <DBOptions> configurarDBOptions) 
        {
            services.AddDbContext<ContextoBD>();
            services.Configure(configurarDBOptions);
            ///services.AddScoped<IPreguntas, Preguntas>();
            

            return services;
        }



    }
}
