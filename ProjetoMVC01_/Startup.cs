using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjetoMVC01_.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoMVC01_
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
            services.AddControllersWithViews(); // habilitar o MVC no projeto!

            // configurando o modo de autenticaçãp do projeto (COOKIES)
            services.Configure<CookiePolicyOptions>(options =>
            { options.MinimumSameSitePolicy = SameSiteMode.None; });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie();

            //configurar para que a classa ProdutoRepository
            //possa receber a string de conexão do banco de dados
            //mapeada no arquivo appsettings.json
            services.AddTransient(map => new ProdutoRepository
                (Configuration.GetConnectionString("_BDProjetoMVC01")));

            services.AddTransient(map => new UsuarioRepository
                (Configuration.GetConnectionString("_BDProjetoMVC01")));

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
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseCookiePolicy(); //habilitando autenticação por meio de cookies..
            
            app.UseAuthentication(); //habilitando autenticação por meio de cookies..

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //configurando a página inicial do projeto: /Home/Index
                endpoints.MapControllerRoute(
                        name: "default", pattern: "{controller=Account}/{action=Login}");
            });
        }
    }
}
