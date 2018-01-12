using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using WebApplication5.Abstract;

namespace WebApplication5.Concrete
{
	public class FormAuthProvider: IAuthProvider
	{
		public bool Authentificate(string username, string password)
		{
			bool res = FormsAuthentication.Authenticate(username, password);
			if (res)
				FormsAuthentication.SetAuthCookie(username, false);
			return res;

		}
	}
}