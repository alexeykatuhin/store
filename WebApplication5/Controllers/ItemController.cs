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
	 
	    public int pageSize = 2;

		//подгрузка объектов в листинге
	    private static int curPage;
	    public ItemController(IItemRepository repo)
	    {
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
					HeadImgUrl = _repo.Images.First(x=>x.ItemId==listItems[i].Id && x.IsHead).ImgUrl_271_171
				});
			}
			ItemListViewModel model = new ItemListViewModel()
			{
				Items	= listView
				,
				CurrentCategory = category, Count = _repo.Items.Count(x=> category == null || x.Category == category)
			}; 
			return View(model);
		}

	    public ActionResult GetList(string category)
	    {
		    
			List<Item> listItems = _repo.Items
				.Where(p => string.IsNullOrEmpty(category) || p.Category == category)
				.OrderBy(game => game.Id).Skip((++curPage-1)*pageSize).Take(pageSize).ToList();





			List<ItemViewModel> listView = new List<ItemViewModel>();
			for (int i = 0; i < listItems.Count(); i++)
			{
				listView.Add(new ItemViewModel()
				{
					Item = listItems[i],
					HeadImgUrl = _repo.Images.First(x => x.ItemId == listItems[i].Id && x.IsHead).ImgUrl_271_171
				});
			}
			ItemListViewModel model = new ItemListViewModel()
			{
				Items = listView
				, CurrentCategory = category
			};
		    return PartialView("GetList",model);
	    }

	    public ActionResult ItemBig(int id)
	    {
		    Item item = _repo.Items.First(x => x.Id == id);
		    ItemBigViewModel model = new ItemBigViewModel()
		    {
				FullItems = _repo.FullItems.Where(x=>x.ItemId == id).ToList(),
				Item = item,
				Images = _repo.Images.Where(x=>x.ItemId == id).ToList()
		    };
		    return View(model);
	    }
	}
}