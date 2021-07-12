namespace CognitoDashboard.IdentityManager
{
    public class CognitoConfig
    {
        public const string Name = "Cognito";
        
        public string MetadataAddress => $"https://cognito-idp.{Region}.amazonaws.com/{UserPoolId}/.well-known/openid-configuration";
        
        public string DashboardClientId { get; set; }

        public string DashboardClientSecret { get;  set; }
        
        public string UserPoolId { get; set; }
        
        public string Region { get; set; }
        
        public string RedirectUri { get; set; }
        
        public string PostLogoutRedirectUri { get; set; }
    }
}
