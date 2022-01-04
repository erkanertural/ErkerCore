using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

using ErkerCore.API;
using ErkerCore.API.Helpers;
using ErkerCore.BusinessLogic.Helpers;
using ErkerCore.Message.Result;
using System;
using System.IO;
using System.Reflection;
using System.Text.Json.Serialization;

namespace ErkerCore.API
{
    public class Startup
    {
        public static IWebHostEnvironment Host;
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
#if DEBUG
            BusinessUtility.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
            BusinessUtility.ConnectionString = BusinessUtility.GetConnStr();
#else
            BusinessUtility.ConnectionString = Configuration.GetConnectionString("DefaultConnection");
#endif
        }



        public IConfiguration Configuration { get; }

        private static string AssemblyName { get; } = Assembly.GetEntryAssembly()?.GetName().Name;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //  var provider = services.BuildServiceProvider();
            //var host = new HostEnvironmentAbstraction(provider);
            //services.AddSingleton<HostEnvironmentAbstraction>(host);
            #region DbContext
            // services.AddDbContext<AppDbContext>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            #endregion

            #region CORS Policy
            services.AddCors(o => o.AddPolicy("CorsApi", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));
            #endregion

            services.AddControllers();


            #region AppSettings
            // configure strongly typed settings object
            services.Configure<Helpers.AppSettings>(Configuration.GetSection("AppSettings"));
            services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.Preserve);
            #endregion

            string xmlCommentsFilePath = System.IO.Path.GetDirectoryName(Directory.GetCurrentDirectory()) + "\\API\\ErkerCore.API.xml";

            #region Swagger configuration
            services.AddSwaggerGen(c =>
            {
                c.IncludeXmlComments(xmlCommentsFilePath);
                c.SwaggerDoc(AssemblyName, Configuration.GetSection("Swagger:Info").Get<OpenApiInfo>());
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please insert JWT with Bearer into field",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
            #endregion
        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint($"/swagger/{AssemblyName}/swagger.json", AssemblyName));
            }
            // global cors policy
            app.UseCors(x =>
            {
                x.AllowAnyOrigin();
                x.AllowAnyMethod();
                x.AllowAnyHeader();
            });
            app.UseRouting();
            Host = env;


            // custom jwt auth middleware
            app.UseMiddleware<JwtMiddleware>();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
            BusinessUtility.IsDevelopment = true;

        }
    }
}
