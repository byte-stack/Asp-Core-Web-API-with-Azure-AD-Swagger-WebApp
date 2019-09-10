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

        // CORS policy name 
        readonly string AllowSpecificOrigins = "AllowSpecificOrigins";

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            ///Azure Ad authentication service 
            services.AddAuthentication(AzureADDefaults.BearerAuthenticationScheme)
                .AddAzureADBearer(options => Configuration.Bind("AzureAd", options));


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            //Database service - connection string from the app settings.
            services.AddDbContextPool<DataServices.Data.EBSContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            ///Swagger configuration with Azure Ad authentication
            services.AddSwaggerGen(c =>
            {
                //initiate default
                c.SwaggerDoc("v1", new Info { Title = "EBS Web API", Version = "v1" });

                //Added Oauth 
                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Type = "oauth2",
                    Flow = "implicit",
                    AuthorizationUrl = $"https://login.microsoftonline.com/{Configuration["AzureAD:TenantId"]}/oauth2/authorize",
                    Scopes = new Dictionary<string, string> { { "user_impersonation", "Access API" } }

                });
                //Added permission
                c.AddSecurityRequirement(new Dictionary<string, IEnumerable<string>> { { "oauth2", new[] { "user_impersonation" } } });
            });

            //Activating the repository service from Business and DataService 
            services.AddScoped<IBillingDetails, BillingDetails>();
            services.AddScoped<DataServices.Contracts.IBillingRepository, DataServices.Repositories.BillingRepository>();


            #region CORS Policy

            //Adding the cors policy of local host and Azure
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
            //configure the swagger 
            app.UseSwagger();

            //Swagger UI default.
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

            //Swagger UI configuration and display name settings
            app.UseSwaggerUI(c =>{
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "EBS Web API");
            c.RoutePrefix = string.Empty;
        });

            //Adding cors to app 
            app.UseCors(AllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
