using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nop.Core.Infrastructure;
using Nop.Plugin.Misc.Employee.Areas.Admin.Factories;
using Nop.Plugin.Misc.Employee.Services;

namespace Nop.Plugin.Misc.Employee.Infrastructure
{
    public class EmployeeNopStartup : INopStartup
    {
        public void ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IEmployeeModelFactory, EmployeeModelFactory>();
            services.Configure<RazorViewEngineOptions>(options =>
            {
                options.ViewLocationExpanders.Add(new EmployeeViewLocationExpander());
            });
        }

        public void Configure(IApplicationBuilder application)
        {
        }

        public int Order => 10;
    }
}

