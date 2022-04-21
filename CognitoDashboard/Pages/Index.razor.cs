using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace CognitoDashboard.Pages;

public partial class Index : ComponentBase
{
    [CascadingParameter]
    private Task<AuthenticationState> AuthenticationStateTask { get; set; }

    [Inject]
    private NavigationManager NavigationManager { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var authenticationState = await AuthenticationStateTask;

        if (authenticationState.User.Identity.IsAuthenticated)
        {
            NavigationManager.NavigateTo("/dashboard");
        }
    }
}
