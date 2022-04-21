using Bunit;
using CognitoDashboard.Pages;
using Xunit;

namespace CognitoDashboard.Tests;

public class DashboardTest
{
    [Fact]
    public void DashboardComponentRendersCorrectly()
    {
        // Arrange
        using var ctx = new TestContext();

        // Act
        var cut = ctx.RenderComponent<Dashboard>();

        // Assert
        cut.MarkupMatches(@"<nav><ol class=""breadcrumb border border-0""><li class=""breadcrumb-item active"">Dashboard</li></ol></nav>");
    }
}
