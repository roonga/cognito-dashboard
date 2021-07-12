using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;

namespace CognitoDashboard.Pages
{
    [Authorize]
    public partial class Dashboard : ComponentBase
    { }
}