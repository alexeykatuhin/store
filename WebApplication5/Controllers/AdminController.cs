using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Abstract;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
	[Authorize]
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

	    public ViewResult Create()
	    {
		    return View("Edit", new Item());
	    }

	    [HttpPost]
	    public ActionResult Delete(int Id)
	    {
		    Item deletedItem = _repo.DeleteItem(Id);
		    if (deletedItem != null)
			    TempData["message"] = string.Format("Товар \"{0}\" был удален", deletedItem.Name);
		    return RedirectToAction("Index");
	    }

	    public ActionResult AddImage(int Id)
	    {
		    WebApplication5.Models.Image img = _repo.Images.FirstOrDefault(x => x.ItemId == Id);
		    return View(img);
	    }

	    public ActionResult GetPhoto(int Id,HttpPostedFileBase Image)
	    {
		    
			    string filename = System.IO.Path.GetFileName(Image.FileName);
			    if (System.IO.File.Exists("~/Content/Images/" + filename))
				    filename = Image.GetHashCode().ToString();
				Image.SaveAs(Server.MapPath("~/Content/Images/" + filename));
				
		    _repo.SaveImg(Id, "/Content/Images/" + filename);

			

			

		    return View("AddImage", _repo.Images.First(x=>x.ItemId==Id));
	    }
	}
}