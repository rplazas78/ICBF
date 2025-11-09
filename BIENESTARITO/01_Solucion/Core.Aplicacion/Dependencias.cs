using Core.Aplicacion.CasosUso.Autenticacion;
using Core.Aplicacion.CasosUso.Preguntas;
using Core.Aplicacion.Options;
using Microsoft.Extensions.DependencyInjection;

namespace Core.Aplicacion
{
    public static class Dependencias
    {
        public static IServiceCollection agregarServiciosAplicacion(this IServiceCollection services, Action<authSettings> auth, Action<appSettings> app)
        {
            services.Configure(auth);
            services.Configure(app);
            services.AddScoped<IServicioToken, ServicioToken>();
            services.AddScoped<IPreguntas, Preguntas>();
            return services;
        }
    }
}
