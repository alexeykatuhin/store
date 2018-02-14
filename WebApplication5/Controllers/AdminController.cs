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
			if (lstItm.Any())
		    foreach (Item item in lstItm)
		    {
			    viewModel.Add(new ItemBigViewModel()
			    {
				    Item = item,
					FullItems = _repo.FullItems.Where(x=>x.ItemId == item.Id).ToList(),
					Images = _repo.Images.Where(x=>x.ItemId == item.Id).ToList(),
					GroupId = _repo.Groups.First(x=>x.ItemId == item.Id).GroupId
			    });

		    }
			ItemBigViewModelAdmin model = new ItemBigViewModelAdmin()
			{
				ModelList =  viewModel,
				GroupIds = _repo.Groups.Select(x=>x.GroupId).Distinct().ToList()
			};
		    return View(model);
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

		    return View(new ImageViewModel() {Images = img, Id = Id});
	    }

	    public ActionResult AddPhoto(int ItemId, int Id=0)
	    {
		    bool _isHead = !_repo.Images.Any(x => x.ItemId == ItemId && x.IsHead);
		    Image img = Id == 0 ? new Image() {Id=0, ItemId = ItemId, IsHead = _isHead} : _repo.Images.FirstOrDefault(x => x.Id == Id); 
		    return View(img);
	    }
		public ActionResult CreateFullItem(int Id)
		{
			return View("EditFullItem", new FullItem()
			{
				ItemId = Id
			});
		}
		[HttpPost]
		public ActionResult GetPhoto(int ItemId, int Id, bool IsHead, HttpPostedFileBase Image)
		{
		
			Image img = new Image()
			{
				Id = Id,
				ItemId = ItemId,
				IsHead = IsHead,
				ImageMimeType = Image.ContentType,
				ImageData = new byte[Image.ContentLength]
			};
			Image.InputStream.Read(img.ImageData, 0, Image.ContentLength);

			_repo.AddImg(img);

			Models.Image newImg = Id == 0 ? _repo.Images.Last() : _repo.Images.First(x => x.Id == Id);

			TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", _repo.Items.First(x=>x.Id == ItemId).Name);
			return View("AddPhoto", newImg);
		}

	
	    public ActionResult SetHead(int Id, int ItemId)
	    {
		    Image former = _repo.Images.First(x => x.ItemId == ItemId && x.IsHead);
		    former.IsHead = false;
		    Image newer = _repo.Images.First(x => x.Id == Id);
		    newer.IsHead = true;
			_repo.AddImg(former);
			_repo.AddImg(newer);
		    return RedirectToAction("Images", new ImageViewModel()
		    {
			    Id = ItemId,
			    Images = _repo.Images.Where(x => x.Id == ItemId)
		    });
	    }

	    public ActionResult DeleteImage(int Id)
	    {
		    Image img = _repo.Images.First(x => x.Id == Id);
		    int ItemId = img.ItemId;
			_repo.DeleteImage(Id);
			return RedirectToAction("Images", new ImageViewModel()
			{
				Id = ItemId,
				Images = _repo.Images.Where(x => x.Id == ItemId)
			});
		}
		public ActionResult FullItems(int Id)
		{
			IEnumerable<FullItem> fullItems = _repo.FullItems.Where(x => x.ItemId == Id);

			return View(new FullItemViewModel() { FullItems = fullItems, Id = Id });
		}
		public ViewResult EditFullItem(int Id)
		{
			FullItem item = _repo.FullItems.FirstOrDefault(x => x.Id == Id);
		
			return View(item);
		}
		[HttpPost]
		public ViewResult EditFullItem(FullItem game)
		{
			if (ModelState.IsValid)
			{
				TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", _repo.Items.First(x=>x.Id==game.ItemId).Name);
				_repo.SaveFullItem(game);
				return View("EditFullItem", game);
			}
			else
			{
				return View("EditFullItem", game);
			}
		}

		public ActionResult DeleteFullItem(int id)
		{
			int itemId = _repo.FullItems.First(x => x.Id == id).ItemId;
			_repo.DeleteFullItem(id);
			return RedirectToAction("FullItems", new { Id = itemId});

		}

	    public ActionResult Group(int Id, string group)
	    {
			Models.Group gr = new Group() {ItemId = Id};
		    if (group == "new")
		    {
			    gr.GroupId = _repo.Groups.Max(x => x.GroupId) + 1;
		    }
		    else
		    {
			    gr.GroupId = Convert.ToInt32(group);
		    }
			TempData["message"] = string.Format("Изменения в товаре \"{0}\" были сохранены", _repo.Items.First(x => x.Id == Id).Name);

			_repo.SaveGroup(gr);
		    return RedirectToAction("Index");
	    }
	}
}