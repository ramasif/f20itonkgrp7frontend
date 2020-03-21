using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace f20itonkgrp7frontend
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
            services.AddControllersWithViews();

            services.AddHttpClient("backend", c =>
            {
                //var host = Configuration["F20ITONKBACKENDJRT_SERVICE_HOST"];
                //var port = Configuration["F20ITONKBACKENDJRT_PORT_8080_TCP_PORT"];
                //c.BaseAddress = new Uri("https://localhost:44323/"); //local test
                //c.BaseAddress = new Uri("http://" + host + ":" + port + "/"); //Using environment variables
                //c.BaseAddress = new Uri("http://f20itonkbackendjrt:8080/"); //Hard coded K8s Service name
                c.BaseAddress = new Uri("http://f20itonkgrp7backend:8080/"); //Hard coded K8s Service name // har skiftet fra f20itonkbackendgrp7
                //c.BaseAddress = new Uri("http://146.148.126.255:8080/"); <<< ENDPOINT TO GAINZ!
                c.DefaultRequestHeaders.Add("Accept", "application/json");
            });

            //services.AddDbContext<ITONKgrp7FrontendContext>(options =>
            //        options.UseSqlServer(Configuration.GetConnectionString("ITONKgrp7FrontendContext")));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
