using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TecMailAPI.BusinessLogic;

namespace TecMailAPI
{
    public class Startup
    {
        TecMailCommon objCom = new TecMailCommon();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
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
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }
            if (TecMailCommon.LINUX_ROOT_PATH == "")
                TecMailCommon.LINUX_ROOT_PATH = env.ContentRootPath;

            if (TecMailCommon.LINUX_WWW_PATH == "")
                TecMailCommon.LINUX_WWW_PATH = env.WebRootPath;

            TecMailCommon.MailUserName = objCom.readLocalConfig("MailSetting.xml", "MailUserID");
            TecMailCommon.MailUserPassword = objCom.readLocalConfig("MailSetting.xml", "MailUserPassword");
            TecMailCommon.strOSType = objCom.readLocalConfig("OSConfig.xml", "ostype");
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc();
        }
    }
}
