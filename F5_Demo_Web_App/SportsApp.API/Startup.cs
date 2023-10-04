using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
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
using SportsApp.API.Data;
using Microsoft.AspNetCore.HttpOverrides;

namespace SportsApp.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        readonly string _myPolicy = "Allow All Headers & Origins";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
	    services.Configure<ForwardedHeadersOptions>(options =>
            {
            		options.ForwardedHeaders =
                	ForwardedHeaders.XForwardedFor | ForwardedHeaders.XForwardedProto;
            });
            //setup database connection string from appsettings.json
            services.AddDbContext<DataContext>(lam => lam.UseSqlite(Configuration.GetConnectionString("DefaultConnection")));
            //setup Controllers
            services.AddControllers();
            //setup cors to activate non-local ip addresses acessing the client
            services.AddCors(x => x.AddPolicy(_myPolicy, options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()));
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
            AddJwtBearer(options => {
                options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                    ValidateIssuer = false, 
                    ValidateAudience = false
                };
                });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
		app.UseForwardedHeaders();
            }
	    else
	    {
	    	app.UseHsts();
	    }
   
            // app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(_myPolicy);

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
