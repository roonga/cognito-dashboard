using Amazon.CognitoIdentityProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CognitoDashboard.IdentityManager;

public class IdentityProviderBuilder
{
    private readonly IdentityProviderProxy<IAmazonCognitoIdentityProvider> _proxy;
    private readonly IAmazonCognitoIdentityProvider _client;
    private readonly ILogger<IAmazonCognitoIdentityProvider> _logger;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public IdentityProviderBuilder(
        IAmazonCognitoIdentityProvider client,
        IdentityProviderProxy<IAmazonCognitoIdentityProvider> proxy,
        ILogger<IAmazonCognitoIdentityProvider> logger,
        IHttpContextAccessor httpContextAccessor)
    {
        _client = client;
        _proxy = proxy;
        _logger = logger;
        _httpContextAccessor = httpContextAccessor;
    }

    public IAmazonCognitoIdentityProvider Proxy => _proxy.Instance(_client, _httpContextAccessor, _logger);
}
