using Microsoft.AspNetCore.Http;

namespace CognitoDashboard.Tests.Fakes;

internal class FakeHttpContextAccessor : IHttpContextAccessor
{
    private HttpContext _httpContext = new DefaultHttpContext();

    public HttpContext HttpContext { get => _httpContext; set => _httpContext = value; }
}
