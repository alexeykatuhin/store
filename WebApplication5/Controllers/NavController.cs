using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Abstract;

namespace WebApplication5.Controllers
{
    public class NavController : Controller
    {
        // GET: Nav
	    private IItemRepository _repo;

	    public NavController(IItemRepository repo)
	    {
		    _repo = repo;
	    }

	    public PartialViewResult Menu(string category=null)
	    {
		    ViewBag.SelectedCategory = category;
		    IEnumerable<string> categories = _repo.Items
						 .Select(game => game.Category)
				.Distinct()
				.OrderBy(x => x);
			return PartialView(categories);
		}
    }
}