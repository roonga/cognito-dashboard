using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Amazon.CognitoIdentityProvider.Model;
using Amazon.Runtime;
using CognitoDashboard.IdentityManager;
using CognitoDashboard.Models;
using Microsoft.AspNetCore.Components;

namespace CognitoDashboard.Pages
{
    public partial class Groups : ComponentBase
    {
        [Inject]
        private IdentityProviderClientFactory IdentityProviderClientFactory { get; set; }

        [Inject]
        private CognitoConfig CognitoConfig { get; set; }

        private string _error;
        private ListGroupsRequest _request;
        private ListGroupsResponse _response;
        private readonly List<GroupTypeModel> _groups = new();

        private int _pageLimit = 10;

        private bool _selectAll;
        private bool SelectAll
        {
            get { return _selectAll; }

            set
            {
                _selectAll = value;
                _groups.ForEach(g => g.Selected = value);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            _request = new()
            {
                UserPoolId = CognitoConfig.UserPoolId,
                Limit = _pageLimit
            };

            _response = new();
            
            await LoadGroups();
        }

        private async Task LoadGroups()
        {
            try
            {
                _error = null;
                _request.NextToken = (_response?.NextToken != null) ? _response.NextToken : null;
                _request.Limit = _pageLimit;
                _response = await IdentityProviderClientFactory.Client.ListGroupsAsync(_request, CancellationToken.None);
                _groups.AddRange(_response.Groups.Select(g => new GroupTypeModel(g)));
            }
            catch (AmazonServiceException e)
            {
                _error = e.Message;
            }
        }

        private void OnSelectAll()
        {
            SelectAll = !SelectAll;
        }

        private void OnGroupSelected(GroupTypeModel group)
        {
            group.Selected = !group.Selected;
            _selectAll = _groups.All(u => u.Selected);
        }
    }
}
