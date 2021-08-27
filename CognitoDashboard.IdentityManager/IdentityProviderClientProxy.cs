using System.Reflection;
using Microsoft.Extensions.Logging;

namespace CognitoDashboard.IdentityManager
{
    public class IdentityProviderClientProxy<T> : DispatchProxy where T : IIdentityProviderClient
    {
        private IIdentityProviderClient _client;

        public static IIdentityProviderClient Create(IIdentityProviderClient decorated)
        {
            object proxy = Create<T, IdentityProviderClientProxy<T>>();
            ((IdentityProviderClientProxy<T>)proxy)._client = decorated;
            return (IIdentityProviderClient)proxy;
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
            _client.Logger.LogError(exception, "EXCEPTION:: Username:{username}; Operation:{MethodName}", _client.HttpContextAccessor.HttpContext.User.Identifier(), methodInfo.Name);
        }

        private void LogBefore(MethodInfo methodInfo, object[] args)
        {
            _client.Logger.LogInformation("Username:{username}; Operation:{MethodName}", _client.HttpContextAccessor.HttpContext.User.Identifier(), methodInfo.Name);
            _client.Logger.LogDebug("BEFORE :: Username:{username}; Operation:{MethodName}, Request:{@args}", _client.HttpContextAccessor.HttpContext.User.Identifier(), methodInfo.Name, args);
        }

        private void LogAfter(MethodInfo methodInfo, object[] args, object result)
        {
            _client.Logger.LogDebug("AFTER :: Username:{username}; Operation:{MethodName}, Request:{@args}", _client.HttpContextAccessor.HttpContext.User.Identifier(), methodInfo.Name, args);
        }
    }
}
