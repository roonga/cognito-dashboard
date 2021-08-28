using Amazon.CognitoIdentityProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CognitoDashboard.IdentityManager
{
    public class IdentityProviderClient
    {
        private readonly IdentityProviderClientProxy<IAmazonCognitoIdentityProvider> _proxy;
        private readonly IAmazonCognitoIdentityProvider _client;
        private readonly ILogger<IAmazonCognitoIdentityProvider> _logger;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IdentityProviderClient(IAmazonCognitoIdentityProvider client, IdentityProviderClientProxy<IAmazonCognitoIdentityProvider> proxy, ILogger<IAmazonCognitoIdentityProvider> logger, IHttpContextAccessor httpContextAccessor)
        {
            _client = client;
            _proxy = proxy;
            _logger = logger;
            _httpContextAccessor = httpContextAccessor;
        }

        public IAmazonCognitoIdentityProvider Proxy => _proxy.Client(_client, _httpContextAccessor, _logger);
    }
}
