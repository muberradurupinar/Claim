using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using WEBCLAİM1.Context;
using WEBCLAİM1.Models;

namespace WEBCLAİM1
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
            services.AddControllers();

            services.AddDbContext<MyDbContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("DbContext")));

            services.AddAuthorization(config =>
            {
                config.AddPolicy(Policies.Admin, Policies.AdminPolicy());
               
                config.AddPolicy(Policies.User, Policies.UserPolicy());
            });

            services.AddAuthorization(x => x.AddPolicy("AdminPolicy", policy => policy.RequireClaim("UserRole", "admin")));

          

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
             {
                 option.RequireHttpsMetadata = false;
                 option.SaveToken = true;
                 option.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,                                      //hangi sitelerin kullanacağını belirlediğimiz alan
                ValidateIssuer = true,                                        //token değerini kimin dağıttığını ifade edeceğimiz alan
                ValidateLifetime = true,                                      //oken değerinin süresini kontrol edecek olan doğrulama
                ValidateIssuerSigningKey = true,                              //security key verisinin doğrulaması
                ValidIssuer = Configuration["Jwt:Issuer"],                  //tokenın Issuer değerini belirledik
                ValidAudience = Configuration["Jwt:Audience"],              //uygulamadaki tokenın Audience değerini belirledik
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:SecurityKey"])),//Security Key doğrulaması için SymmetricSecurityKey nesnesi aracılığıyla mevcut keyi belirtiyoruz.
                ClockSkew = TimeSpan.Zero                                    //imeSpan.Zero değeri ile token süresinin üzerine ekstra bir zaman eklemeksizin sıfır değerini belirtiyoruz.
            };
        });

            services.AddControllers();
        }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
