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
using RestApi.Common;
using Unity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RestApi.Application.Mapping;

namespace RestApi.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<MongoDBConfig>(
                Configuration.GetSection("MongoDB"));

            services.AddSingleton<IMongoDBConfig>(sp =>
                sp.GetRequiredService<IOptions<MongoDBConfig>>().Value);

            AddAutoMapper(services);

            services.AddCors();
            services.AddControllers().AddNewtonsoftJson();

            ConfigureJwtAuthentication(services);

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_3_0)
                .AddControllersAsServices();
        }

        private static void AddAutoMapper(IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            { cfg.AddProfile(new AutoMapperProfile()); });
            services.AddSingleton(config.CreateMapper());
        }

        public void ConfigureContainer(IUnityContainer container)
        {
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
                AddAssembly(assembly, container);
        }

        private void AddAssembly(Assembly assembly, IUnityContainer container)
        {
            var classTypes = assembly.GetTypes().Where(t => t.GetCustomAttribute<InjectAttribute>() != null);
            foreach (var classType in classTypes)
                foreach (var interfaceType in classType.GetInterfaces())
                    container.RegisterType(interfaceType, classType);
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

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }

        private void ConfigureJwtAuthentication(IServiceCollection services)
        {
            // configure strongly typed settings objects
            var appSettingsSection = Configuration.GetSection("AppSettings");
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
