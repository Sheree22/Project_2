using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShereeGreeffWebApp.Areas.Identity.Data;
using ShereeGreeffWebApp.Data;

[assembly: HostingStartup(typeof(ShereeGreeffWebApp.Areas.Identity.IdentityHostingStartup))]
namespace ShereeGreeffWebApp.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ShereeGreeffWebAppContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ShereeGreeffWebAppContextConnection")));

                services.AddDefaultIdentity<ShereeGreeffWebAppUser>(options =>
                {
                    options.SignIn.RequireConfirmedAccount = false;
                    options.Password.RequireLowercase = false;
                    options.Password.RequireUppercase = false;
                })

                    .AddEntityFrameworkStores<ShereeGreeffWebAppContext>();
            });
        }
    }
}