using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using CognitoDashboard.IdentityManager;
using Microsoft.AspNetCore.Components;

namespace CognitoDashboard.Pages
{
    public partial class NewGroup : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IdentityProviderClientFactory IdentityProviderClientFactory { get; set; }

        [Inject]
        private CognitoConfig CognitoConfig { get; set; }

        private CreateGroupRequest _request;
        private string _error;

        protected override void OnInitialized()
        {
            _request = new()
            {
                UserPoolId = CognitoConfig.UserPoolId
            };
        }

        private async Task CreateGroup()
        {
            _error = null;

            try
            {
                var response = await IdentityProviderClientFactory.Client.CreateGroupAsync(_request, CancellationToken.None);
                NavigationManager.NavigateTo("Groups");
            }
            catch (AmazonServiceException e)
            {
                _error = e.Message;
            }
        }

        private void Cancel()
        {
            NavigationManager.NavigateTo("groups");
        }
    }
}
