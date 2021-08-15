using Elasticsearch.Net;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Nest;
using SearchApartments.Core.Interfaces;
using SearchApartments.Core.Services;
using System;

namespace SearchApartments.API
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

            services.AddControllers();
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "SearchApartments.API", Version = "v1" });
            });

            var pool = new SingleNodeConnectionPool(new Uri(Configuration["ElasticSearch:ElasticSearchEndpoint"]));
            var settings = new ConnectionSettings(pool)
                .BasicAuthentication(Configuration["ElasticSearch:ElasticSearchUsername"], Configuration["ElasticSearch:ElasticSearchPassword"]);

            services.AddSingleton<IElasticClient>(new ElasticClient(settings));
            services.AddTransient<IFileUploadService, FileUploadService>();
            services.AddTransient<ISearchDataService, SearchDataService>();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "SearchApartments.API v1"));
            }

            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
