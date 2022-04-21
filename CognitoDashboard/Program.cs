using CognitoDashboard.Authorization;
using CognitoDashboard.IdentityManager;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Identity.Web;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Serilog.Events;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(
    new LoggerConfiguration()
   .MinimumLevel.Information()
   .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
   .Enrich.FromLogContext()
   .WriteTo.Console()
   .CreateLogger());

ConfigureOpenIdConnect(builder);

builder.Services.AddHttpContextAccessor();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor()
    .AddMicrosoftIdentityConsentHandler();

builder.Services.AddSingleton<IAuthorizationHandler, DashboardAuthorizationHandler>();
builder.Services.ConfigureIdentityManager(builder.Configuration);

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.Run();

void ConfigureOpenIdConnect(WebApplicationBuilder builder)
{
    var cognitoConfig = builder.Configuration.GetSection(CognitoConfig.Name).Get<CognitoConfig>();

    builder.Services.AddAuthentication(options =>
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

        options.Events.OnRedirectToIdentityProvider = async c =>
        {
            c.ProtocolMessage.RedirectUri = cognitoConfig.RedirectUri;
            await Task.CompletedTask;
        };

        options.Events.OnRedirectToIdentityProviderForSignOut = async c =>
        {
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

    // authorization for dashboard
    builder.Services.AddAuthorization(options =>
        options.AddPolicy(PolicyName.DashboardAdministrators, policy => policy.Requirements.Add(new DashboardAuthorizationRequirement("dashboard-administrators")))
    );
}
