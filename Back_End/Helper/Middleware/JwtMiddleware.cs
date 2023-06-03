using Back_End.Helper.JwtUtils;
using Back_End.Services.UserService;

namespace Back_End.Helper.Middleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _nextRequestDelegate;

        public JwtMiddleware(RequestDelegate nextRequestDelegate)
        {
            _nextRequestDelegate = nextRequestDelegate;
        }
        public async Task Invoke(HttpContext context, IUserService userService, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = jwtUtils.ValidateJwtToken(token);
            if (userId != Guid.Empty)
            {
                context.Items["User"] = userService.GetUserMappedById(userId);
               
            }
            await _nextRequestDelegate(context);
           
        }

    }
}
