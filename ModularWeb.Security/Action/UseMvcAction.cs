using System;
using ExtCore.Mvc.Infrastructure.Actions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Routing;

namespace ModularWeb.Security.Action
{
    public class UseMvcAction : IUseMvcAction
    {
        public int Priority => 1000;

        public void Execute(IRouteBuilder routeBuilder, IServiceProvider serviceProvider)
        {
            routeBuilder.MapAreaRoute(
                "SecurityRoute",
                "Security",
                "Security/{controller}/{action}",
                new { controller = "Default", action = "Index", Namespace = "ModularWeb.Security.Controllers" }
            );
        }
    }
}