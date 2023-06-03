using Back_End.Enums;
using Back_End.Models.Users;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

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
            if(_roles==null || _roles.Count==0)
            {
                context.Result = unauthorizedStatusObject;
            }

            var user = (User)context.HttpContext.Items["User"];
            if(user==null)
            {
                context.Result = unauthorizedStatusObject;
            }
            if (user==null || !_roles.Contains(user.Role))
            {
                context.Result = unauthorizedStatusObject;
            }
            
        }
    }
}
