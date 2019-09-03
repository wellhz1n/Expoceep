using Expoceep.DB;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using Microsoft.AspNetCore.Session;
using Expoceep.DAO.UsuarioDAO;
using Expoceep.Bibliotecas;
using NToastNotify;
using Expoceep.DAO.ProdutoDAO;

namespace Expoceep
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            //INJECAO DE DEPENDENCIA PARA OS DAO
            #region DAO
            services.AddScoped<IUsuarioDAO, UsuarioDAO>();
            services.AddScoped<IProdutoDAO, ProdutoDAO>();
            #endregion
            //FIM DAS INJECOES DE DEPENDENCIAS
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            string conn = Configuration["ConexaoMySql:MySqlConnectionString"];
            services.AddDbContext<ERPDatabaseContext>(o => o.UseMySql(conn));


            #region CACHE
            services.AddDistributedMemoryCache();
            services.AddSession();
            services.AddHttpContextAccessor();
            services.AddScoped<Sessao>(); 
            services.AddScoped<LoginSession>();
            #endregion
            #region Toastr
            services.AddMvc().AddNToastNotifyToastr(new ToastrOptions()
            {
                ProgressBar = false,
                PositionClass = ToastPositions.TopRight
            }); 
            #endregion
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
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseSession();
            app.UseNToastNotify();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Login}/{action=Login}/{id?}");
            });
        }
    }
}
