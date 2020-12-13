using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Unity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using Core.Server.Web.Utils;
using Core.Server.Application.Mappers;
using Core.Server.Common.Config;
using Core.Server.Injection.Interfaces;
using Core.Server.Injection.Unity;
using Core.Server.Injection.Reflaction;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Policy;
using Core.Server.Web.Authorization;

namespace Core.Server.Web
{
    public class Startup
    {
        private readonly IReflactionHelper reflactionHelper;

        public Config Config { get; }

        public Startup(IConfiguration configuration)
        {
            Config = configuration.Get<Config>();
            reflactionHelper = new ReflactionHelper(Config.Assemblies);
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton(Config.MongoDB);
            services.AddSingleton(Config.AppSettings);
            services.AddSingleton(Config.Cache);
            services.AddSingleton(Config.Logging);

            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();

            ConfigureJwtAuthentication(services);

            services.AddSingleton<IAuthorizationService, DocumentAuthorizationHandler>();

            services.
                AddMvc(o => o.Conventions.Add(
                    new GenericControllerRouteConvention()
                )).
                ConfigureApplicationPartManager(m =>
                    m.FeatureProviders.Add(new GenericTypeControllerFeatureProvider(reflactionHelper)
                ))
                 .AddControllersAsServices();
        }

        public virtual void ConfigureContainer(IUnityContainer container)
        {
            new UnityContainerBuilder(container, reflactionHelper).ConfigureContainer();
            AddAutoMapper(container);
            container.RegisterInstance(typeof(IReflactionHelper), reflactionHelper);
        }

        private void AddAutoMapper(IUnityContainer container)
        {
            var config = new MapperConfiguration(cfg =>
            {
                var profile = new AutoMapperProfile(container);
                cfg.AddProfile(profile);
            });
            var mapper = config.CreateMapper();
            container.RegisterInstance(typeof(IMapper), mapper);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            // global cors policy

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureJwtAuthentication(IServiceCollection services)
        {
            // configure jwt authentication
            var key = Encoding.ASCII.GetBytes(Config.AppSettings.Secret);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

    }
}