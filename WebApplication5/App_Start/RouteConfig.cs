using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace WebApplication5
{
	public class RouteConfig
	{
		public static void RegisterRoutes(RouteCollection routes)
		{
			routes.IgnoreRoute("{resource}.axd/{*pathInfo}");


			routes.MapRoute(null,
	"",
	new
	{
		controller = "Item",
		action = "Index"
	}
	);


			//routes.MapRoute(null,
			//	"",
			//	new
			//	{
			//		controller = "Item",
			//		action = "List",
			//		category = (string)null
			//	}
			//	);


			routes.MapRoute(null,
		  "{categoryBig}",
		  new { controller = "item", action = "List" }
	  );

			routes.MapRoute(null, "{controller}/{action}");


			
		}
	}
}
