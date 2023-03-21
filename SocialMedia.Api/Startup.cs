using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SocialMedia.Api.Middleware;
using SocialMedia.Core.Interfaces;
using SocialMedia.Infrastructure.Data;
using SocialMedia.Infrastructure.Repositories;
using System;

namespace SocialMedia.Api
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
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Esta linea dice que cuando se presente un error de referencia circular simplemente lo ignore y muestre la entidad con el nodo siguiente vacio
            services.AddControllers().AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            })
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });



            services.AddDbContext<SocialMediaContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("SocialMedia")));

            //Dependencias
            services.AddTransient<IPostRepository, PostRepository>();
            services.AddTransient<GlobalExceptionHandlerMiddleware>();
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

            app.UseAuthorization();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
