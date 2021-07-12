using CognitoDashboard.IdentityManager;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Threading.Tasks;

namespace CognitoDashboard
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
            var cognitoConfig = Configuration.GetSection(CognitoConfig.Name).Get<CognitoConfig>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddCookie()
            .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
            {
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.MetadataAddress = cognitoConfig.MetadataAddress;
                options.ClientId = cognitoConfig.DashboardClientId;
                options.ClientSecret = cognitoConfig.DashboardClientSecret;
                options.SaveTokens = true;
                
                options.Events.OnRedirectToIdentityProvider = async c => {
                    c.ProtocolMessage.RedirectUri = cognitoConfig.RedirectUri;
                    await Task.CompletedTask;
                };
                
                options.Events.OnRedirectToIdentityProviderForSignOut = async c => {
                    c.ProtocolMessage.IssuerAddress = cognitoConfig.PostLogoutRedirectUri;
                    await Task.CompletedTask;
                };

                options.NonceCookie = new CookieBuilder
                {
                    Name = OpenIdConnectDefaults.CookieNoncePrefix,
                    HttpOnly = true,
                    SameSite = SameSiteMode.None,
                    SecurePolicy = CookieSecurePolicy.Always,
                    IsEssential = true
                };
                options.CorrelationCookie = new CookieBuilder
                {
                    SecurePolicy = CookieSecurePolicy.Always
                };
            });

            services.AddHttpContextAccessor();
            services.AddRazorPages();
            services.AddServerSideBlazor()
                .AddMicrosoftIdentityConsentHandler();

            //services
            IdentityManager.Startup.ConfigureServices(Configuration, services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                var cognitoConfig = Configuration.GetSection(CognitoConfig.Name).Get<CognitoConfig>();
                logger.LogInformation("Cognito Config {@cognitoConfig}", cognitoConfig);
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
