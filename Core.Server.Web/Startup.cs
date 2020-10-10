using System;
using System.Linq;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Core.Server.Common;
using Unity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using Core.Server.Shared.Resources;
using Core.Server.Application;
using AutoMapper;
using Core.Server.Web.Utils;
using Core.Server.Application.Mappers;
using Core.Server.Application.Helpers;
using Core.Server.Common.Config;

namespace Core.Server.Web
{
    public class Startup
    {
        private IReflactionHelper reflactionHelper;

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            reflactionHelper = new ReflactionHelper(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDBConfig>(
                Configuration.GetSection(Config.MongoDbSection));

            services.AddSingleton<IMongoDBConfig>(sp =>
                sp.GetRequiredService<IOptions<MongoDBConfig>>().Value);

            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();

            ConfigureJwtAuthentication(services);

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

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        private void ConfigureJwtAuthentication(IServiceCollection services)
        {
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection(Config.AppSettingsSection);
            services.Configure<AppSettings>(appSettingsSection);

            // configure jwt authentication
            var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes(appSettings.Secret);
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