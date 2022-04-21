using Amazon.CognitoIdentityProvider;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace CognitoDashboard.IdentityManager;

// ref: https://jonskeet.uk/csharp/singleton.html, forth version
public class IdentityProviderProxy<T> : DispatchProxy where T : IAmazonCognitoIdentityProvider
{
    private static readonly IAmazonCognitoIdentityProvider _provider = Create<T, IdentityProviderProxy<T>>();

    private IAmazonCognitoIdentityProvider _client;
    private ILogger<IAmazonCognitoIdentityProvider> _logger;
    private IHttpContextAccessor _httpContextAccessor;

    static IdentityProviderProxy() { }

    public IAmazonCognitoIdentityProvider Instance(
        IAmazonCognitoIdentityProvider client,
        IHttpContextAccessor httpContextAccessor,
        ILogger<IAmazonCognitoIdentityProvider> logger)
    {
        var provider = (IdentityProviderProxy<T>)_provider;
        provider._client = client;
        provider._httpContextAccessor = httpContextAccessor;
        provider._logger = logger;

        return _provider;
    }

    protected override object Invoke(MethodInfo targetMethod, object[] args)
    {
        try
        {
            LogBefore(targetMethod, args);

            var result = targetMethod.Invoke(_client, args);

            LogAfter(targetMethod, args, result);

            return result;
        }
        catch (Exception ex) when (ex is TargetInvocationException)
        {
            LogException(ex.InnerException ?? ex, targetMethod);
            throw ex.InnerException ?? ex;
        }
    }

    private void LogException(Exception exception, MethodInfo methodInfo = null)
    {
        _logger.LogError(exception, "EXCEPTION:: Username:{username}; Operation:{MethodName}", _httpContextAccessor.HttpContext.User.Identifier(), methodInfo.Name);
    }

    private void LogBefore(MethodInfo methodInfo, object[] args)
    {
        _logger.LogInformation("Username:{username}; Operation:{MethodName}", _httpContextAccessor.HttpContext.User.Identifier(), methodInfo.Name);
        _logger.LogDebug("BEFORE :: Username:{username}; Operation:{MethodName}, Request:{@args}", _httpContextAccessor.HttpContext.User.Identifier(), methodInfo.Name, args);
    }

    private void LogAfter(MethodInfo methodInfo, object[] args, object result)
    {
        _logger.LogDebug("AFTER :: Username:{username}; Operation:{MethodName}, Request:{@args}", _httpContextAccessor.HttpContext.User.Identifier(), methodInfo.Name, args);
    }
}
