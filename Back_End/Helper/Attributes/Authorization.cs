using Back_End.Enums;
using Back_End.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Diagnostics;

namespace Back_End.Helper.Attributes
{
    public class Authorization : System.Attribute, IAuthorizationFilter
    {
        private readonly ICollection<Role> _roles;
        public Authorization(params Role[] roles) 
        {
            _roles = roles;
        }
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var unauthorizedStatusObject = new JsonResult(new { Message = "Unauthorized" }){ StatusCode=StatusCodes.Status401Unauthorized};
            var user = context.HttpContext.Items["User"] as User;

            
      
            if(_roles==null)
            {
                context.Result = unauthorizedStatusObject;
            }

            else if (user==null || !_roles.Contains(user.Role))
            {
                context.Result = unauthorizedStatusObject;
            }

            
        }
    }
}
