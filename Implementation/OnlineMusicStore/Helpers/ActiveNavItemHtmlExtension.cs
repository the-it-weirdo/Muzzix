using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineMusicStore.Helpers
{
    public static class ActiveNavItemHtmlExtension
    {
        public static string IsSelected(this IHtmlHelper htmlHelper, string controllers, string actions, string cssClassIfTrue, string cssClassIfFalse = "") //, string currentQueryParamKey = "", string expectedQueryParam = null
        {
            string currentAction;
            string currentController;
            try
            {
                currentAction = htmlHelper.ViewContext.RouteData.Values["action"].ToString();
                currentController = htmlHelper.ViewContext.RouteData.Values["controller"].ToString();
            }
            catch (Exception)
            {
                return cssClassIfFalse;
            }

            // string currentParams = htmlHelper.ViewContext.HttpContext.Request.QueryString[currentQueryParamKey];

            IEnumerable<string> acceptedActions = (actions ?? currentAction).Split(',');
            IEnumerable<string> acceptedControllers = (controllers ?? currentController).Split(',');

            return acceptedActions.Contains(currentAction)
                && acceptedControllers.Contains(currentController)
                // && currentParams == expectedQueryParam
                ? cssClassIfTrue : cssClassIfFalse;
        }
    }
}