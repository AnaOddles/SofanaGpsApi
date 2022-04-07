using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using SofanaGPSApi.Models;
using SofanaGPSApi.Services;

namespace SofanaGPSApi
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
            //requires using Microsft.Extensions.Options 
            services.Configure<SofanaGPSDatabaseSettings>(
                Configuration.GetSection(nameof(SofanaGPSDatabaseSettings)));
            
            services.AddSingleton<ISofanaGPSDatabaseSettings>(sp =>
                sp.GetRequiredService<IOptions<SofanaGPSDatabaseSettings>>().Value);

            //Registered LocationService as a singleton 
            services.AddSingleton<LocationService>();

            //Registered UserService as a singleton
            services.AddSingleton<UserService>();

            services.AddControllers()
                .AddNewtonsoftJson(options => options.UseMemberCasing());

            //Register swagger dependecies
            services.AddSwaggerGen();


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();


            app.UseSwagger();

            app.UseSwaggerUI();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }
    }
}
