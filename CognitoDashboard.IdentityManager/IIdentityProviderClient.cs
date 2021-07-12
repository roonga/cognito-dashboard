using Amazon.CognitoIdentityProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace CognitoDashboard.IdentityManager
{
    public interface IIdentityProviderClient : IAmazonCognitoIdentityProvider 
    {
        ILogger<IdentityProviderClient> Logger { get; }

        IHttpContextAccessor HttpContextAccessor { get; }
    }
}
