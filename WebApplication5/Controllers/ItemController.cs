using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Abstract;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ItemController : Controller
    {
	    private IItemRepository _repo;
	    private IImageRepository _imgRepo;
	    public int pageSize = 2;

		//подгрузка объектов в листинге
	    private static int curPage;
	    public ItemController(IItemRepository repo, IImageRepository imgRepo)
	    {
		    _imgRepo = imgRepo;
		    _repo = repo;
	    }

	
		public ViewResult List(string category)
		{
			curPage = 1;
			List<Item> listItems =_repo.Items
					.Where(p => category == null || p.Category == category)
					.OrderBy(game => game.Id)
					.Take(pageSize).ToList();
			List<ItemViewModel> listView = new List<ItemViewModel>();
			for (int i = 0; i < listItems.Count(); i++)
			{
				listView.Add(new ItemViewModel()
				{
					Item = listItems[i],
					HeadImgUrl = _imgRepo.Images.Fi
				});
			}
			ItemListViewModel model = new ItemListViewModel()
			{
				Items	= _repo.Items
				.Where(p => category == null || p.Category == category)
				.OrderBy(game => game.Id)
				
				.Take(pageSize), Count = _repo.Items.Count(p => category == null || p.Category == category),
				CurrentCategory = category
			}; 
			return View(model);
		}

	    public ActionResult GetList(string category)
	    {
	
		    ItemListViewModel model = new ItemListViewModel()
			{
				Items = _repo.Items
				.Where(p => string.IsNullOrEmpty(category) || p.Category == category)
	.OrderBy(game => game.Id)
	.Skip((++curPage - 1) * pageSize)
	.Take(pageSize), CurrentCategory = category
			};
		    return PartialView("GetList",model);
	    }
	}
}