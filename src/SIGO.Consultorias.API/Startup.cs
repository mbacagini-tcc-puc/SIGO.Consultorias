using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SIGO.Consultorias.API.Auth;
using SIGO.Consultorias.Application.Repositories;
using SIGO.Consultorias.Application.Services;
using SIGO.Consultorias.Application.UseCases.Analises.EdicaoAnalise;
using SIGO.Consultorias.Data;
using SIGO.Consultorias.Data.Repositories;
using System;

namespace SIGO.Consultorias.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ConsultoriasContext>(options => options.UseNpgsql(
                                                             Configuration["SIGOConsultoriasConnection"],
                                                             postgresOptions => postgresOptions.CommandTimeout(180)
                                                              ));

            services.AddScoped<IUsuarioAutenticadoService, UsuarioAutenticadoService>();

            services.AddScoped<IAnaliseRepository, AnaliseRepository>();
            services.AddScoped<IEmpresaUsuarioRepository, EmpresaUsuarioRepository>();

            services.AddScoped<IEdicaoAnaliseUseCase, EdicaoAnaliseUseCase>();

            ConfigureAuth(services);

            services.AddControllers();
        }

        private void ConfigureAuth(IServiceCollection services)
        {
            var tokenConfigurations = new TokenConfigurations();
            new ConfigureFromConfigurationOptions<TokenConfigurations>(Configuration.GetSection("TokenConfigurations")).Configure(tokenConfigurations);
            services.AddSingleton(tokenConfigurations);

            services.AddAuthentication(authOptions =>
            {
                authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(bearerOptions =>
            {
                var paramsValidation = bearerOptions.TokenValidationParameters;
                paramsValidation.IssuerSigningKey = tokenConfigurations.SymmetricSecurityKey;
                paramsValidation.ValidAudience = tokenConfigurations.Audience;
                paramsValidation.ValidIssuer = tokenConfigurations.Issuer;
                paramsValidation.ValidateIssuerSigningKey = true;
                paramsValidation.ValidateLifetime = true;
                paramsValidation.ClockSkew = TimeSpan.Zero;

                bearerOptions.Events = new JwtBearerEvents
                {
                    OnTokenValidated = AuthMiddleware.Execute()
                };
            });

            services.AddAuthorization(auth =>
            {
                auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
                    .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser().Build());
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
