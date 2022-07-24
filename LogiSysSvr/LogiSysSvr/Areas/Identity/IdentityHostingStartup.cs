using System;
using LogiSysSvr.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(LogiSysSvr.Areas.Identity.IdentityHostingStartup))]
namespace LogiSysSvr.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            //builder.ConfigureServices((context, services) => {
            //});
            builder.ConfigureServices((context, services) => {
                //services.AddDbContext<LogiSysSvrContext>(options =>
                //    options.UseSqlServer(
                //        context.Configuration.GetConnectionString("LogiSysSvrContextConnection")));
                //
                //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                //    .AddEntityFrameworkStores<LogiSysSvrContext>();
                services.AddDbContext<ApplicationDbContext>(options =>
                        options.UseNpgsql(
                            context.Configuration.GetConnectionString("DefaultConnection")));

                services.AddDefaultIdentity<MyIdentifyUser>()
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<ApplicationDbContext>();
                //services.AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
                // .AddDefaultUI()
                // .AddEntityFrameworkStores<ApplicationDbContext>()
                // .AddDefaultTokenProviders();
                //services.AddControllersWithViews();
                //services.AddRazorPages();

                // add policy
                services.AddAuthorization(options => {
                    //options.AddPolicy("readpolicy",
                    //    builder => builder.RequireRole("SystemManager", "QAManager", "User"));
                    options.AddPolicy("writepolicy",
                        builder => builder.RequireRole("SystemManager", "QAManager"));
                });
            });
        }
    }
}