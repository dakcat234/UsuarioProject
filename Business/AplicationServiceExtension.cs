using Business.Repositories;
using Microsoft.Extensions.DependencyInjection;
using PopsyMarket.Business;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsuarioProject.Business;

namespace Business
{
    public static class AplicationServiceExtension
    {
        public static IServiceCollection aplication(this IServiceCollection services)
            => services.AddScoped<IUsuarioRepository, UsuarioRepository>()
            .AddScoped<IBusinessRepository, BusinessRepository>()
            .AddScoped<IEncryptService, EncryptService>()
            .AddScoped<ILoginBusiness, LoginBusiness>()
            .AddScoped<IJwtService, JwtService>();
    }
}
