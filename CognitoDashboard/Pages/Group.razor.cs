using System.Net;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using CognitoDashboard.IdentityManager;
using Microsoft.AspNetCore.Components;

namespace CognitoDashboard.Pages
{
    public partial class Group : ComponentBase
    {
        [Inject]
        private NavigationManager NavigationManager { get; set; }

        [Inject]
        private IdentityProviderClientFactory IdentityProviderClientFactory { get; set; }

        [Inject]
        private CognitoConfig CognitoConfig { get; set; }

        [ParameterAttribute]
        public string GroupName { get; set; }

        private UpdateGroupRequest _updateRequest;
        private UpdateGroupResponse _updateResponse;

        private bool _isEditMode = false;
        private bool _isDeleteMode = false;

        private bool IsReadOnly => !_isEditMode;

        private List<string> _errorMessages = new();
        private List<string> _successMessages = new();

        protected override async Task OnInitializedAsync()
        {
            await GetGroup();
        }

        private void Edit() => _isEditMode = true;
        private void Delete() => _isDeleteMode = true;

        private void Cancel()
        {
            _isEditMode = false;
            _isDeleteMode = false;
        }

        private async Task GetGroup()
        {
            _errorMessages = new();

            try
            {
                var getRequest = new GetGroupRequest
                {
                    UserPoolId = CognitoConfig.UserPoolId,
                    GroupName = GroupName
                };

                var getResponse = await IdentityProviderClientFactory.Client.GetGroupAsync(getRequest, CancellationToken.None);

                _updateRequest = new UpdateGroupRequest
                {
                    UserPoolId = CognitoConfig.UserPoolId,
                    GroupName = GroupName,
                    Description = getResponse.Group.Description,
                    RoleArn = getResponse.Group.RoleArn,
                    Precedence = getResponse.Group.Precedence
                };
            }
            catch (AmazonServiceException e)
            {
                _errorMessages.Add(e.Message);
            }
        }

        private async Task UpdateGroup()
        {
            _errorMessages = new();
            _successMessages = new();

            try
            {
                if (string.IsNullOrWhiteSpace(_updateRequest.RoleArn))
                    _updateRequest.RoleArn = null;

                _updateResponse = await IdentityProviderClientFactory.Client.UpdateGroupAsync(_updateRequest, CancellationToken.None);

                if (_updateResponse.HttpStatusCode == HttpStatusCode.OK)
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

        private void ClearSuccessMessages() => _successMessages = new();

        private void ClearErrorMessages() => _errorMessages = new();

        private async Task ConfirmDeleteGroup()
        {
            _errorMessages = new();
            _successMessages = new();

            try
            {
                var deleteRequest = new DeleteGroupRequest
                {
                    UserPoolId = CognitoConfig.UserPoolId,
                    GroupName = GroupName
                };

                var deleteResponse = await IdentityProviderClientFactory.Client.DeleteGroupAsync(deleteRequest, CancellationToken.None);
                NavigationManager.NavigateTo("/groups");
            }
            catch (AmazonServiceException e)
            {
                _errorMessages.Add(e.Message);
            }
        }
    }
}
