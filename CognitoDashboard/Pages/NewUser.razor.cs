using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using CognitoDashboard.IdentityManager;
using Microsoft.AspNetCore.Components;

namespace CognitoDashboard.Pages
{
    public partial class NewUser : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IdentityProviderBuilder IdentityProvider { get; set; }

        [Inject]
        private CognitoConfig CognitoConfig { get; set; }

        private AdminCreateUserRequest _request;
        private string _error;

        private string Email { get; set; }
        private bool EmailVerified { get; set; }
        private bool SendInvitationBySms { get; set; }
        private bool SendInvitationByEmail { get; set; }
        private string Phone { get; set; }
        private bool MarkPhoneAsVerified { get; set; }

        protected override void OnInitialized()
        {
            _request = new()
            {
                UserPoolId = CognitoConfig.UserPoolId,
                UserAttributes = new List<AttributeType>()
            };
        }

        private async Task CreateUser()
        {
            _error = null;

            //invitation
            _request.DesiredDeliveryMediums = new List<string>();
            if (SendInvitationBySms) _request.DesiredDeliveryMediums.Add("SMS");
            if (SendInvitationByEmail) _request.DesiredDeliveryMediums.Add("EMAIL");

            //user attributes
            _request.UserAttributes = new List<AttributeType>();

            if (!string.IsNullOrWhiteSpace(Email))
            {
                _request.UserAttributes.Add(new() { Name = "email", Value = Email });
                _request.UserAttributes.Add(new() { Name = "email_verified", Value = EmailVerified ? "true" : "false" });
            }

            if (!string.IsNullOrWhiteSpace(Phone))
            {
                _request.UserAttributes.Add(new() { Name = "phone_number", Value = Phone });
                _request.UserAttributes.Add(new() { Name = "phone_number_verified", Value = EmailVerified ? "true" : "false" });
            }

            if (!SendInvitationBySms && !SendInvitationByEmail)
            {
                _request.MessageAction = MessageActionType.SUPPRESS;
            }

            try
            {
                var response = await IdentityProvider.Proxy.AdminCreateUserAsync(_request, CancellationToken.None);
                NavigationManager.NavigateTo("users");
            }
            catch (AmazonServiceException e)
            {
                _error = e.Message;
            }
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("users");
        }
    }
}
