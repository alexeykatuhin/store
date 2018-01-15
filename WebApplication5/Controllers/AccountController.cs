using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Abstract;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class AccountController : Controller
    {
	    private IAuthProvider _authProvider;

	    public AccountController(IAuthProvider authProvider)
	    {
		    _authProvider = authProvider;
	    }
		public ViewResult Login()
		{
			return View();
		}

		[HttpPost]
		public ActionResult Login(LoginViewModel model, string returnUrl)
		{

			if (ModelState.IsValid)
			{
				if (_authProvider.Authentificate(model.UserName, model.Password))
				{
					return Redirect(returnUrl ?? Url.Action("Index", "Admin"));
				}
				else
				{
					ModelState.AddModelError("", "Неправильный логин или пароль");
					return View();
				}
			}
			else
			{
				return View();
			}
		}
	}
}