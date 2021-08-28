using Amazon;
using Amazon.CognitoIdentityProvider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CognitoDashboard.IdentityManager
{
    public static class Startup
    {
        public static void ConfigureIdentityManager(this IServiceCollection services, IConfiguration configuration)
        {
            var cognitoConfig = configuration.GetSection(CognitoConfig.Name).Get<CognitoConfig>();
            var region = RegionEndpoint.GetBySystemName(cognitoConfig.Region);

            services.AddSingleton(cognitoConfig);
            services.AddSingleton<IdentityProviderClient>();
            services.AddSingleton<IdentityProviderClientProxy<IAmazonCognitoIdentityProvider>>();
            services.AddSingleton<IAmazonCognitoIdentityProvider>(new AmazonCognitoIdentityProviderClient(region));
        }
    }
}
