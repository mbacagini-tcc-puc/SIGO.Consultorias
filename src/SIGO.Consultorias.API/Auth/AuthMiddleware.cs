using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using SIGO.Consultorias.Application.Services;
using SIGO.Consultorias.Application.TransferObjects;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SIGO.Consultorias.API.Auth
{
    public static class AuthMiddleware
    {
        public static Func<TokenValidatedContext, Task> Execute()
        {
            return ctx =>
            {
                var usuarioAutenticadoService = ctx.HttpContext.RequestServices.GetRequiredService<IUsuarioAutenticadoService>();
                var claims = ctx.Principal?.Claims;

                if (claims?.Any() == true)
                {
                    var id = Convert.ToInt32(claims.FirstOrDefault(c => c.Type.Contains("userid")).Value);
                    var nome = claims.FirstOrDefault(c => c.Type.Contains("name")).Value;

                    usuarioAutenticadoService.Usuario = new UsuarioAutenticado
                    {
                        Id = id,
                        Nome = nome
                    };
                }

                return Task.CompletedTask;
            };
        }

    }
}
