using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GildedRoseApi.Managers;
using GildedRoseApi.Managers.Contracts;
using GildedRoseData.Contracts;
using GildedRoseData.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.EntityFrameworkCore;
using GildedRoseData.Models;
using GildedRoseData.Repositories;
using GildedRoseData.SeedData;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Swashbuckle.AspNetCore.Swagger;

namespace GildedRoseApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<GildedRoseItemContext>(options =>
                    options.UseInMemoryDatabase("GildedRoseDb"));

            services.AddDbContext<GildedRoseUserContext>(options =>
                options.UseInMemoryDatabase("GildedRoseDb"));

         

            services.AddIdentity<GildedRoseUser, IdentityRole>()
                .AddEntityFrameworkStores<GildedRoseUserContext>()
                .AddDefaultTokenProviders();


            services.AddScoped<IAuthenticationManager, AuthenticationManager>();
            services.AddScoped<IItemsRepository, ItemsRepository>();
            services.AddScoped<IItemsManager, ItemsManager>();
          

           // services.AddScoped<GildedRoseItemContext, GildedRoseItemContext>();

            addConfigSwagger(services);

            addJwtSupport(services);
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
                app.UseHsts();
            }

            SeedDb.Initialize(app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope()
                .ServiceProvider);

            app.UseAuthentication();
            app.UseHttpsRedirection();

            app.UseSwagger();  //json endpoint
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint(Configuration["SwaggerEndPoint"], "Gilded Rose API - v1");
                c.InjectStylesheet("/swagger-ui/stylesheet.css");
            });
            app.UseMvc();
        }


        private void addConfigSwagger(IServiceCollection services)
        {

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Gilded Rose API",
                    Version = "v1"
                });

            });

        }

        private void addJwtSupport(IServiceCollection services)
        {
            services.AddAuthentication(options =>
                {
                    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.SaveToken = true;
                    options.RequireHttpsMetadata = false;

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateIssuerSigningKey = true,
                        ValidAudience = Configuration["jwtIssuer"],
                        ValidIssuer = Configuration["jwtAudience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt8&j^&"])),
                    };
                });
        }
    }
}
