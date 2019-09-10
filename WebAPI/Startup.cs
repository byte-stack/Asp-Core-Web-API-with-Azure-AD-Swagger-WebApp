using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.AzureAD.UI;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebAPI.Business.Interface;
using WebAPI.Business.Logic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Swashbuckle.AspNetCore.Swagger;

namespace WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        readonly string AllowSpecificOrigins = "AllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContextPool<DataServices.Data.EBSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "EBS Web API", Version = "v1" });
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"https://login.microsoftonline.com/{Configuration["AzureAD:TenantId"]}/oauth2/authorize",
                    Scopes = new Dictionary<string, string> { { "user_impersonation", "Access API" } }

                });
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "oauth2", new[] { "user_impersonation" } } });
            });


            services.AddScoped<IBillingDetails, BillingDetails>();
            services.AddScoped<DataServices.Contracts.IBillingRepository, DataServices.Repositories.BillingRepository>();


            #region CORS Policy


            services.AddCors(options =>
            {
                options.AddPolicy(AllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("https://localhost:44385",
                            "http://webappaspcore.azurewebsites.net").
                            WithMethods("GET", "POST", "PUT", "DELETE");
                    });

            });
            
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.OAuthClientId(Configuration["Swagger:ClientId"]);
                c.OAuthClientSecret(Configuration["Swagger:ClientSecret"]);
                c.OAuthRealm(Configuration["AzureAD:ClientId"]);
                c.OAuthAppName("EBS Web API v1");
                c.OAuthScopeSeparator(" ");
                c.OAuthAdditionalQueryStringParams(new Dictionary<string, string>() { { "resource", Configuration["AzureAD:ClientId"] } });
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "EBS Web API v1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {

                app.UseHsts();
            }
         
            app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "EBS Web API");
            c.RoutePrefix = string.Empty;
        });

            app.UseCors(AllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
