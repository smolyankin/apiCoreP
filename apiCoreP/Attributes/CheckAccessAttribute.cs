using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using apiCoreP.Enums;
using apiCoreP.Services;

namespace apiCoreP.Attributes
{
    /// <summary>
    /// check user permission to action
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class CheckAccessAttribute : ActionFilterAttribute
    {
        public UserRoleType[] GrantAccessByUserRoleTypes { get; set; }

        /// <summary>
        /// check user permission to action
        /// </summary>
        /// <param name="userRoleTypes">user role types array</param>
        public CheckAccessAttribute(params UserRoleType[] userRoleTypes)
        {
            GrantAccessByUserRoleTypes = userRoleTypes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filterContext"></param>
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (!filterContext.HttpContext.User.Identity.IsAuthenticated)
                return;

            var allowAccess = false;
            if (GrantAccessByUserRoleTypes != null)
            {
                var userService = (IUserService)filterContext.HttpContext.RequestServices.GetService(typeof(IUserService));
                allowAccess = userService.CheckAccess(filterContext.HttpContext.User.Identity.Name, GrantAccessByUserRoleTypes);
            }

            if (!allowAccess)
                filterContext.Result = new ForbidResult();
        }
    }
}
