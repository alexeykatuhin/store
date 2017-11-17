using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Models;
using IModelBinder = System.Web.Mvc.IModelBinder;

namespace WebApplication5.Infrastructure.Binders
{
	public class CartModelBinder:IModelBinder
	{
		private const string sessionKey = "Cart";
		public object BindModel(ControllerContext controllerContext, ModelBindingContext bindingContext)
		{
			Cart cart = null;
			if (controllerContext.HttpContext.Session != null)
			{
				cart = (Cart) controllerContext.HttpContext.Session[sessionKey];
			}

			if (cart == null)
			{
				cart = new Cart();
				if (controllerContext.HttpContext.Session != null)
				{
					controllerContext.HttpContext.Session[sessionKey] = cart;
				}
			}
			return cart;
		}
	}
}