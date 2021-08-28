using System.Net;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using CognitoDashboard.IdentityManager;
using Microsoft.AspNetCore.Components;

namespace CognitoDashboard.Pages
{
    public partial class User : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IdentityProviderClient IdentityProviderClient { get; set; }

        [Inject]
        private CognitoConfig CognitoConfig { get; set; }

        [ParameterAttribute]
        public string UserName { get; set; }

        private bool _isEditMode = false;
        private bool _isEnabled = false;
        private bool _isDeleteMode = false;

        private bool IsReadOnly => !_isEditMode;

        private List<string> _errorMessages = new();
        private List<string> _successMessages = new();
        private AdminGetUserResponse _response;
        
        protected override async Task OnInitializedAsync()
        {
            await GetUser();
        }

        private void Edit() => _isEditMode = true;
        private void Delete() => _isDeleteMode = true;
        
        private void Disable()
        {
            _isEnabled = false;
        }

        private void Enable() => _isEnabled = true;

        private void Cancel()
        {
            _isEditMode = false;
            _isDeleteMode = false;
        }

        private async Task GetUser()
        {
            _errorMessages = new();

            try
            {
                var getRequest = new AdminGetUserRequest
                {
                    UserPoolId = CognitoConfig.UserPoolId,
                    Username = UserName
                };

                _response = await IdentityProviderClient.Proxy.AdminGetUserAsync(getRequest, CancellationToken.None);
            }
            catch (AmazonServiceException e)
            {
                _errorMessages.Add(e.Message);
            }
        }

        private void ClearSuccessMessages() => _successMessages = new();

        private void ClearErrorMessages() => _errorMessages = new();

        private async Task UpdateUser()
        {
            _errorMessages = new();
            _successMessages = new();

            try
            {
                var updateRequest = new AdminUpdateUserAttributesRequest();
                var updateResponse = await IdentityProviderClient.Proxy.AdminUpdateUserAttributesAsync(updateRequest, CancellationToken.None);

                if (updateResponse.HttpStatusCode == HttpStatusCode.OK)
                {
                    _isEditMode = false;
                    _successMessages.Add("Saved Successfully");
                }
            }
            catch (AmazonServiceException e)
            {
                _errorMessages.Add(e.Message);
            }
        }

        private async Task ConfirmDeleteUser()
        {
            _errorMessages = new();
            _successMessages = new();

            try
            {
                var deleteRequest = new AdminDeleteUserRequest
                {
                    UserPoolId = CognitoConfig.UserPoolId,
                    Username = UserName
                };

                var deleteResponse = await IdentityProviderClient.Proxy.AdminDeleteUserAsync(deleteRequest, CancellationToken.None);
                NavigationManager.NavigateTo("/users");
            }
            catch (AmazonServiceException e)
            {
                _errorMessages.Add(e.Message);
            }
        }
    }
}
