using LogParser.Configuration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using LogParser.Features.Pages.LogFileUpload;
using LogParser.Interfaces;
using LogParser.Services;
using Serilog;

namespace LogParser
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();
            LoadOptions(services);

            services.AddRazorPages(options =>
            {
                options.RootDirectory = @"/Features/Pages";
            });
            services.AddServerSideBlazor();
            services.AddScoped<ILogFileUploadViewModel, LogFileUploadViewModel>();
            services.AddSingleton<IFileManagementService, FileManagementService>();
            services.AddScoped<LogFileParserService>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseSerilogRequestLogging();
            
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
        private void LoadOptions(IServiceCollection services)
        {
            services.Configure<FileManagementSettings>(Configuration.GetSection(nameof(FileManagementSettings)));
        }
    }
}