using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Abstract;
using WebApplication5.Classes;
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
		    List<ItemBigViewModel> viewModel = new List<ItemBigViewModel>();
		    List <Item> lstItm= _repo.Items.ToList();
		    foreach (Item item in lstItm)
		    {
			    viewModel.Add(new ItemBigViewModel()
			    {
				    Item = item,
					FullItems = _repo.FullItems.Where(x=>x.ItemId == item.Id).ToList(),
					Images = _repo.Images.Where(x=>x.ItemId == item.Id).ToList()
			    });

		    }
		    return View(viewModel);
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
				if (!_repo.Images.Any(x=>x.Id == game.Id))
					_repo.AddImg(game.Id, "/Content/Images/NoImg.jpg", true);
				TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", game.Name);
				return RedirectToAction("Edit", game);
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

	    public ActionResult Images(int Id)
	    {
		    IEnumerable<Image> img = _repo.Images.Where(x => x.ItemId == Id);
		    return View(img);
	    }

	    public ActionResult AddPhoto(int ItemId, int Id=0)
	    {
		    Image img = Id == 0 ? new Image() {Id=0, ItemId = ItemId, ImgUrl = "/Content/Images/NoImg.jpg", ImgUrl_271_171 = "/Content/Images/NoImg271_171.jpg", ImgUrl_75_75 = "/Content/Images/NoImg75_75.jpg", IsHead = false} : _repo.Images.FirstOrDefault(x => x.Id == Id); 
		    return View(img);
	    }


		public ActionResult GetPhoto(int ItemId, int Id, bool IsHead, HttpPostedFileBase Image)
		{
			if (Id != 0)
			{
				Models.Image img = _repo.Images.First(x => x.Id == Id);
				System.IO.File.Delete(img.ImgUrl);
				System.IO.File.Delete(img.ImgUrl_271_171);
				System.IO.File.Delete(img.ImgUrl_75_75);

			}

			string fileName = System.IO.File.Exists("~/Content/Images/" + Image.FileName)
			? ItemId.ToString() + "_" + Id.ToString() + Image.FileName
			: Image.FileName;

		
			Image.SaveAs(Server.MapPath("~/Content/Images/" + fileName));

			_repo.AddImg(ItemId, "/Content/Images/" + fileName,IsHead,Id, fileName);

			Models.Image newImg = Id == 0 ? _repo.Images.Last() : _repo.Images.First(x => x.Id == Id);




			return View("AddPhoto", newImg);
		}
	}
}