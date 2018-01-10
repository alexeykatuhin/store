using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Abstract;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class AdminController : Controller
    {
	    private IItemRepository _repo;

	    public AdminController(IItemRepository repo)
	    {
		    _repo = repo;
	    }
        // GET: Admin
	    public ViewResult Index()
	    {
		    return View(_repo.Items);
	    }

	    public ViewResult Edit(int Id)
	    {
		    Item item = _repo.Items.FirstOrDefault(x => x.Id == Id);
		    return View(item);
	    }
		[HttpPost]
		public ActionResult Edit(Item game)
		{
			if (ModelState.IsValid)
			{
				_repo.SaveItem(game);
				TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", game.Name);
				return RedirectToAction("Index");
			}
			else
			{
				// Что-то не так со значениями данных
				return View(game);
			}
		}
	}
}