using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using CognitoDashboard.IdentityManager;
using CognitoDashboard.Models;
using Microsoft.AspNetCore.Components;

namespace CognitoDashboard.Pages
{
    public partial class GroupUsers : ComponentBase
    {
        [Inject]
        private IdentityProviderBuilder IdentityProvider { get; set; }

        [Inject]
        private CognitoConfig CognitoConfig { get; set; }

        [ParameterAttribute]
        public string GroupName { get; set; }

        private ListUsersInGroupRequest _request;
        private ListUsersInGroupResponse _response;

        private int _pageLimit = 10;
        private const string PageLimitDefault = "10";
        private List<UserTypeModel> _users = new();

        private List<string> _errorMessages = new();
        private List<string> _successMessages = new();
        private string _isProcessingMessage;

        private bool _groupSelect;
        private bool GroupSelect
        {
            get => _groupSelect;

            set
            {
                _groupSelect = value;
                _users.ForEach(u => u.Selected = _groupSelect);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _users = null;
            await LoadUsers();
        }

        private async Task LoadUsers()
        {
            _response = null;

            try
            {
                _errorMessages = new();

                _request = new()
                {
                    UserPoolId = CognitoConfig.UserPoolId,
                    GroupName = GroupName,
                    Limit = _pageLimit,
                    NextToken = (_response?.NextToken != null) ? _response.NextToken : null
                };

                _response = await IdentityProvider.Proxy.ListUsersInGroupAsync(_request, CancellationToken.None);
                var users = _response?.Users.Select(u => new UserTypeModel(u)).ToList();

                if(_users == null)
                    _users = new();

                if (users != null)
                    _users.AddRange(users);
            }
            catch (AmazonServiceException e)
            {
                _errorMessages.Add(e.Message);
            }
        }

        private void OnChangePageLimit(ChangeEventArgs e)
        {
            if (e.Value == null)
                return;

            _pageLimit = int.Parse(e.Value.ToString() ?? PageLimitDefault);
        }

        private void UserSelected(UserTypeModel user)
        {
            user.Selected = !user.Selected;
            _groupSelect = _users.All(u => u.Selected);
        }

        private void OnGroupSelect()
        {
            GroupSelect = !GroupSelect;
        }

        private void ClearSuccessMessages()
        {
            _successMessages = new();
        }

        private async Task RemoveUsers()
        {
            var errorMessages = new List<string>();
            _errorMessages = new();

            var successMessages = new List<string>();
            _successMessages = new();

            _isProcessingMessage = null;

            try
            {
                var total = _users.Count(u => u.Selected);
                var current = 0;

                foreach (var user in _users.Where(u => u.Selected))
                {
                    try
                    {
                        _errorMessages = new();

                        var request = new AdminRemoveUserFromGroupRequest
                        {
                            UserPoolId = CognitoConfig.UserPoolId,
                            GroupName = GroupName,
                            Username = user.UserType.Username
                        };

                        _isProcessingMessage = $"Please wait. Removing {++current} of {total} from {GroupName}";
                        await IdentityProvider.Proxy.AdminRemoveUserFromGroupAsync(request, CancellationToken.None);

                        string displayName = null;
                        if (@user.UserType.Attributes.FirstOrDefault(a => a.Name == "email") != null)
                            displayName = @user.UserType.Attributes.FirstOrDefault(a => a.Name == "email").Value;
                        else if (@user.UserType.Attributes.FirstOrDefault(a => a.Name == "phone_number") != null)
                            displayName = @user.UserType.Attributes.FirstOrDefault(a => a.Name == "phone_number").Value;
                        else
                            displayName = request.Username;

                        successMessages.Add($"Removed {displayName} from {GroupName}");

                        _ = InvokeAsync(() => { StateHasChanged(); });
                    }
                    catch (AmazonServiceException e)
                    {
                        _errorMessages.Add(e.Message);
                    }
                }

                _users = null;
                await LoadUsers();
            }
            finally
            {
                _successMessages = successMessages;
                _errorMessages = errorMessages;
                _isProcessingMessage = null;
            }
        }
    }
}
