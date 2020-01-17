using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using VladyslavOrlovPromo.Core.Configs;
using VladyslavOrlovPromo.Repositories;
using VladyslavOrlovPromo.Repositories.Interfaces;
using VladyslavOrlovPromo.Services.Rankings.Factories;
using VladyslavOrlovPromo.Services.Rankings.Interfaces;
using VladyslavOrlovPromo.Services.Rankings.Services;
using VladyslavOrlovPromo.Web.NetCore.Models.Builder;

namespace VladyslavOrlovPromo.Core
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

            services.AddOptions();
            services.Configure<PlayerProfileConfiguration>(Configuration.GetSection("PlayerProfile"));
            services.Configure<SliderStorageConfiguration>(Configuration.GetSection("SliderStorage"));
            services.AddSingleton(Configuration);

            services.AddTransient<IRankingViewModelBuilder, RankingViewModelBuilder>();

            services.AddHttpClient<IRequestRepository, RequestRepository>();
            services.AddTransient<IPlayerOverviewFactory, PlayerOverviewFactory>();

            services.AddTransient<IRankingService, RankingService>();
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
            }
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
