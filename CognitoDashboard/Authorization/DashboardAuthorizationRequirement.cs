using Microsoft.AspNetCore.Authorization;

namespace CognitoDashboard.Authorization
{
    class DashboardAuthorizationRequirement : IAuthorizationRequirement
    {
        public string CognitoGroup { get; private set; }

        public DashboardAuthorizationRequirement(string cognitoGroup)
        {
            CognitoGroup = cognitoGroup;
        }
    }
}
