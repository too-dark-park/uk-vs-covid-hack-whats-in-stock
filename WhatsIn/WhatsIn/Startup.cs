using System;
using System.Text;
using dotenv.net;
using dotenv.net.DependencyInjection.Microsoft;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using WhatsIn.Models;
using WhatsIn.Services.Models;
using WhatsIn.Services;
using System.IO;
using WhatsIn.Services.Readers;
using WhatsIn.Services.Writers;

namespace WhatsIn
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
            //string path = AppDomain.CurrentDomain.BaseDirectory;

            ////set up dotenv to grab the env vars
            //DotEnv.Config();
            //services.AddEnv(builder =>
            //{
            //    builder
            //    .AddEnvFile(path + ".env")
            //    .AddThrowOnError(false)
            //    .AddEncoding(Encoding.ASCII);
            //});

            //var connectionString = Environment.GetEnvironmentVariable("DB_CONNECTION_STRING");

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddControllers();

            // add the DB context
            services.AddDbContext<WhatsInContext>(options => options.UseSqlServer(connectionString));

            services.Configure<MapApiSettings>(Configuration);

            services.AddSingleton(Configuration);

            services.AddScoped<IPlaces, GooglePlaces>();
            services.AddScoped<IProductsReader, ProductsReader>();
            services.AddScoped<IProductsWriter, ProductsWriter>();
            services.AddScoped<IProductsService, ProductsService>();
            services.AddScoped<IPosts, Posts>();

            services.AddCors(o => o.AddPolicy("WhatsInPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            }));

            services.AddMemoryCache();

            // add any new services to the collection here
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors("WhatsInPolicy");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
