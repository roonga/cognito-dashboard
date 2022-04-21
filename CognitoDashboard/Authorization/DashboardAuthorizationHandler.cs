using Microsoft.AspNetCore.Authorization;

namespace CognitoDashboard.Authorization;

class DashboardAuthorizationHandler : AuthorizationHandler<DashboardAuthorizationRequirement>
{
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DashboardAuthorizationRequirement requirement)
    {
        if (context.User.HasClaim(c => c.Type == "cognito:groups" && c.Value == requirement.CognitoGroup))
        {
            context.Succeed(requirement);
        }
        else
        {
            context.Fail();
        }

        return Task.CompletedTask;
    }
}
