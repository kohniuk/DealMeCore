using AutoMapper;
using DealMeCore.BusinessLogic.Services;
using DealMeCore.BusinessLogic.Services.Implementation;
using DealMeCore.DataAccess.Cache;
using DealMeCore.DataAccess.Cache.Redis;
using DealMeCore.DataAccess.DB;
using DealMeCore.DataAccess.DB.EF;
using DealMeCore.DB.Infrastructure;
using DealMeCore.Logging.NLog;
using DealMeCore.Validation;
using DealMeCore.WebApi.Middlewares;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.IO;
using System.Reflection;
using DealMeCore.WebApi.Utils;

namespace DealMeCore.WebApi
{
    /// <summary>
    /// Startup class.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="env">The hosting environment.</param>
        public Startup(IHostingEnvironment env)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();

            Configuration = builder.Build();
        }

        /// <summary>
        /// The configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940/
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();
            services.AddMvc();

            services.AddResponseCompression(options =>
            {
                options.Providers.Add<GzipCompressionProvider>();
            });

            services.AddAutoMapper();

            // Register the Swagger generator, defining one or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "DealMeCore API", Version = "v1" });

                string xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                string xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                c.IncludeXmlComments(xmlPath);
            });

            services
                .AddLogging(
                    builder => builder
                        .AddConfiguration(Configuration.GetSection("Logging"))
                );

            services.AddSingleton<IConfiguration>(Configuration);
            services.AddScoped<IValidationContext, Validation.ValidationContext>();

            services.AddSingleton(typeof(Logging.ILogger<>), typeof(NLogAdapter<>));

            services.AddDbContext<DealMeCoreDbContext>(
                options =>
                {
                    options.UseSqlServer(
                        Configuration.GetConnectionString("DealMeCore"),
                        sqlOptions =>
                        {
                            sqlOptions.MigrationsAssembly(typeof(DealMeCoreDbContext).GetTypeInfo().Assembly.GetName().Name);
                            sqlOptions.EnableRetryOnFailure(maxRetryCount: 10, maxRetryDelay: TimeSpan.FromSeconds(30), errorNumbersToAdd: null);
                        }
                    );

                    options.ConfigureWarnings(warnings => warnings.Throw(RelationalEventId.QueryClientEvaluationWarning));
                }
            );
            services.AddScoped<DbContext, DealMeCoreDbContext>(e => e.GetService<DealMeCoreDbContext>());
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped(typeof(IBrandService), typeof(BrandService));
            services.AddScoped(typeof(IDealService), typeof(DealService));
            services.AddScoped(typeof(IStoreService), typeof(StoreService));
            services.AddScoped(typeof(ICacheProvider), typeof(StackExchangeRedisCacheProvider));
            services.AddTransient(typeof(IImageWriter), typeof(ImageWriter));
        }

        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        /// <param name="loggerFactory"></param>
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsEnvironment("Debug") || env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMiddleware<ExceptionMiddleware>(env);

            // Enable middleware to serve generated Swagger as a JSON endpoint.
            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration["SwaggerEndpoint"], "DealMeCore API");
            });

            app.UseCors(
                e => e
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin()
                    .AllowCredentials()
            );

            app.UseHttpsRedirection();

            loggerFactory.AddNLog();
            NLog.LogManager.LoadConfiguration("nlog.config");

            app.UseMvc();
        }
    }
}
