using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
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
        private IdentityProviderClientFactory IdentityProviderClientFactory { get; set; }

        [Inject]
        private CognitoConfig CognitoConfig { get; set; }

        [ParameterAttribute]
        public string UserName { get; set; }

        private bool _isEditMode = false;
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

                _response = await IdentityProviderClientFactory.Client.AdminGetUserAsync(getRequest, CancellationToken.None);
            }
            catch (AmazonServiceException e)
            {
                _errorMessages.Add(e.Message);
            }
        }

        private void ClearSuccessMessages() => _successMessages = new();

        private void ClearErrorMessages() => _errorMessages = new();

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

                var deleteResponse = await IdentityProviderClientFactory.Client.AdminDeleteUserAsync(deleteRequest, CancellationToken.None);
                NavigationManager.NavigateTo("/users");
            }
            catch (AmazonServiceException e)
            {
                _errorMessages.Add(e.Message);
            }
        }
    }
}
