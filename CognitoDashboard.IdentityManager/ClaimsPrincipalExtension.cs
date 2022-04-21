using System.Security.Claims;

namespace CognitoDashboard.IdentityManager;

public static class ClaimsPrincipalExtension
{
    public static string Email(this ClaimsPrincipal user)
        => user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress")?.Value;

    public static string Username(this ClaimsPrincipal user)
        => user.FindFirst("cognito:username")?.Value;

    public static string Identifier(this ClaimsPrincipal user)
        => user.Email() ?? user.Username();

    public static string PhoneNumber(this ClaimsPrincipal user)
        => user.FindFirst("{phone_number")?.Value;

    public static string GetCustomAttributeValue(this ClaimsPrincipal user, string customAttributeName)
        => user.FindFirst($"custom:{customAttributeName}")?.Value;
}
