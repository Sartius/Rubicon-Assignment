using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlogBLL;
using BlogDAL;
using AutoMapper;
using Microsoft.AspNetCore.Routing;

namespace BlogCoreAPI
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
            services.AddMvc(option => option.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddScoped<IBlogLogic, BlogLogic>();
            services.AddScoped<ISlugfyHelper, SlugfyHelper>();
            services.AddScoped<ICurrentTime, CurrentTime>();
            services.AddScoped<IBlogManager, BlogManager>();

            MapperConfiguration mapperConfig = new MapperConfiguration(c => c.AddProfile(new MapperProfiles()));
            IMapper mapper = mapperConfig.CreateMapper();
            services.AddSingleton(mapper);
        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller}/{action=Index}/{id?}");

                routes.MapRoute(
                    name: "slugrouting",
                    template: "{controller}/{action=index}/{slug}");
            
            });
        }
    }
}
