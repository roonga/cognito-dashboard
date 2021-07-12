using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using CognitoDashboard.IdentityManager;
using CognitoDashboard.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;

namespace CognitoDashboard.Pages
{
    [Authorize]
    public partial class Users : ComponentBase
    {
        [Inject]
        private IdentityProviderClientFactory IdentityProviderClientFactory { get; set; }

        [Inject]
        private CognitoConfig CognitoConfig { get; set; }

        [ParameterAttribute]
        public string GroupName { get; set; }

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

        private List<UserTypeModel> _users = new();
        private ListUsersRequest _request;
        private ListUsersResponse _response;
        private List<string> _errorMessages = new();
        private List<string> _successMessages = new();

        private string _filter;
        private int _pageLimit = 10;
        private const string PageLimitDefault = "10";

        private string _isProcessingMessage;

        protected override async Task OnInitializedAsync()
        {
            _request = new()
            {
                UserPoolId = CognitoConfig.UserPoolId,
                Limit = _pageLimit
            };

            _response = null;
            
            await NextPage();
        }

        private async Task LoadUsers()
        {
            try
            {
                _groupSelect = false;
                _errorMessages = new();
                _successMessages = new();

                _request.Limit = _pageLimit;
                _request.PaginationToken = (_response?.PaginationToken != null) ? _response.PaginationToken : null;

                _response = await IdentityProviderClientFactory.Client.ListUsersAsync(_request, CancellationToken.None);
                var users = _response?.Users.Select(u => new UserTypeModel(u)).ToList();

                if (users != null)
                    _users.AddRange(users);
            }
            catch (AmazonServiceException e)
            {
                _errorMessages.Add(e.Message);
            }
        }

        private async Task Search(KeyboardEventArgs args)
        {
            if (args != null && args.Key != "Enter")
            {
                return;
            }

            _response = null;
            _users = new();

            _request = new()
            {
                UserPoolId = CognitoConfig.UserPoolId,
                Limit = _pageLimit,
                Filter = !string.IsNullOrWhiteSpace(_filter) ? $"email ^= \"{_filter}\"" : null
            };

            await LoadUsers();
        }

        private async Task Clear()
        {
            _filter = null;
            await Search(null);
        }

        private void OnPageLimitChange(ChangeEventArgs e)
        {
            if (e.Value == null)
                return;

            _pageLimit = int.Parse(e.Value.ToString() ?? PageLimitDefault);
        }

        private async Task NextPage()
        {
            if (_response != null)
                _request.PaginationToken = _response.PaginationToken;

            await LoadUsers();
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

        private async Task AddToGroup()
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
                        var request = new AdminAddUserToGroupRequest
                        {
                            UserPoolId = CognitoConfig.UserPoolId,
                            GroupName = GroupName,
                            Username = user.UserType.Username
                        };

                        _isProcessingMessage = $"Please wait. Adding {++current} of {total} to {GroupName}";
                        await IdentityProviderClientFactory.Client.AdminAddUserToGroupAsync(request, CancellationToken.None);

                        string displayName = null;
                        if (@user.UserType.Attributes.FirstOrDefault(a => a.Name == "email") != null)
                            displayName = @user.UserType.Attributes.FirstOrDefault(a => a.Name == "email").Value;
                        else if (@user.UserType.Attributes.FirstOrDefault(a => a.Name == "phone_number") != null)
                            displayName = @user.UserType.Attributes.FirstOrDefault(a => a.Name == "phone_number").Value;
                        else
                            displayName = request.Username;

                        successMessages.Add($"Added {displayName} to {GroupName}");

                        _ = InvokeAsync(() => { StateHasChanged(); });
                    }
                    catch (AmazonServiceException e)
                    {
                        _errorMessages.Add(e.Message);
                    }
                }
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

