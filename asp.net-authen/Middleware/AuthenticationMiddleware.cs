using Microsoft.AspNetCore.Mvc;

namespace asp.net_authen.Middleware
{
    public class AuthenticationMiddleware : Controller
    {
        private readonly RequestDelegate _next;

        public AuthenticationMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public Task Invoke(HttpContext httpContext)
        {
            var path = httpContext.Request.Path;
            if (path.HasValue && path.Value.StartsWith("/home"))
            {
                if (httpContext.Session.GetString("auth") == null)
                {
                    httpContext.Response.Redirect("auth/login");
                }
            }
            return _next(httpContext);
        }


    }

    public static class AuthenticationMiddlewareExtensions
    {
        public static IApplicationBuilder UseAuthenticationMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<AuthenticationMiddleware>();
        }
    }
}
