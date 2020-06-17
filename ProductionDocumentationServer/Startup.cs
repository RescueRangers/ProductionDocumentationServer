using Blazor.FileReader;
using Blazored.Toast;
using jsreport.AspNetCore;
using jsreport.Client;
using jsreport.Types;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProductionDocumentationServer.Data.DataAdaptors;
using ProductionDocumentationServer.Data.Repositories;
using ProductionDocumentationServer.Services;
using Syncfusion.Blazor;

namespace ProductionDocumentationServer
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddServerSideBlazor().AddHubOptions(o =>
            {
                o.MaximumReceiveMessageSize = 10 * 1024 * 1024; // 10MB
            });

            var key = Configuration.GetValue<string>("SyncfusionLicense");
            Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense(key);

            services.AddSyncfusionBlazor();
            services.AddFileReaderService();
            services.AddSingleton<IProductionReportsRepository, ProductionReportsRepository>();
            services.AddSingleton<IReportSectionsRepository, ReportSectionsRepository>();
            services.AddSingleton<IItemNamesRepository, ItemNamesRepository>();
            services.AddSingleton<IItemNumbersRepository, ItemNumbersRepository>();
            services.AddSingleton<IOrdersRepository, OrdersRepository>();
            services.AddBlazoredToast();
            services.AddJsReport(new ReportingService("http://172.25.194.30:5488"));
            services.AddSingleton<IReportsService, ReportsService>();
            services.AddSingleton<IPdfService, PdfService>();

            services.AddScoped<OrdersAdaptor>();
            services.AddScoped<ReportsAdaptor>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public static void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}