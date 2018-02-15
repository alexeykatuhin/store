using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Abstract;
using WebApplication5.Models;

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

	    public PartialViewResult Menu(string categoryBig,string category=null)
	    {
		    ViewBag.SelectedCategory = category;
			List<NavViewModel> res = new List<NavViewModel>();
			IEnumerable<string> categories = _repo.Items.Where(x => x.CategoryBig == categoryBig)
						 .Select(game => game.Category)
				.Distinct()
				.OrderBy(x => x);
			if (categories.Any())
		    foreach (string s in categories)
		    {
			    res.Add(new NavViewModel()
			    {
				    Name = s,
					Count = _repo.Items.Count(x=>x.Category == s)
			    });
		    }
			return PartialView(res);
		}
    }
}