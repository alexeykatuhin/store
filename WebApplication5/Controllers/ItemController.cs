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


	    public ActionResult Index()
	    {
		    return View("Index");
	    }



	
		public ViewResult List(string categoryBig, string category = null)
		{
			if (!_repo.Items.Any())
				return View(new ItemListViewModel());
			curPage = 1;
			List<Item> listItems =_repo.Items.Where(x => x.CategoryBig == categoryBig)
					.Where(p => category == null || p.Category == category)
					.OrderBy(game => game.Id)
					.Take(pageSize).ToList();
			List<ItemViewModel> listView = new List<ItemViewModel>();
			for (int i = 0; i < listItems.Count(); i++)
			{
				listView.Add(new ItemViewModel()
				{
					Item = listItems[i],
					HeadImgId = _repo.Images.Any(x => x.ItemId == listItems[i].Id && x.IsHead)? (int?)_repo.Images.First(x=>x.ItemId==listItems[i].Id && x.IsHead).Id : null,
					ColorQuantity = _repo.Groups.Count(x=>x.GroupId == listItems[i].Id)
				});
			}
			ItemListViewModel model = new ItemListViewModel()
			{
				Items	= listView
				,
				CurrentCategory = category, Count = _repo.Items.Where(x => x.CategoryBig == categoryBig).Count(x=> category == null || x.Category == category)
			}; 
			return View(model);
		}

	    public ActionResult GetList(string categoryBig, string category = null)
	    {
		    
			List<Item> listItems = _repo.Items.Where(x => x.CategoryBig == categoryBig)
				.Where(p => string.IsNullOrEmpty(category) || p.Category == category)
				.OrderBy(game => game.Id).Skip((++curPage-1)*pageSize).Take(pageSize).ToList();





			List<ItemViewModel> listView = new List<ItemViewModel>();
			for (int i = 0; i < listItems.Count(); i++)
			{
				listView.Add(new ItemViewModel()
				{
					Item = listItems[i],
					HeadImgId = _repo.Images.Any(x => x.ItemId == listItems[i].Id && x.IsHead) ? (int?)_repo.Images.First(x => x.ItemId == listItems[i].Id && x.IsHead).Id :null,
					ColorQuantity = _repo.Groups.Count(x => x.GroupId == listItems[i].Id)
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
			List<ItemViewModel> otherColors = new List<ItemViewModel>();
		    int groupId = _repo.Groups.First(y => y.ItemId == id).GroupId;
		    if (_repo.Groups.Count(x => x.GroupId == groupId) > 1)
		    {
			    List<Group> lstGroups = _repo.Groups.Where(x => x.GroupId == groupId && x.ItemId != id).ToList();
			    foreach (Group group in lstGroups)
			    {
				    otherColors.Add(new ItemViewModel()
				    {
					    Item = _repo.Items.First(x=>x.Id == group.ItemId),
						HeadImgId = _repo.Images.Any(x => x.ItemId == group.ItemId && x.IsHead)? _repo.Images.First(x=>x.ItemId == group.ItemId && x.IsHead).Id:1
				    });
			    }
		    }
		    model.OtherColors = otherColors;
		    return View(model);
	    }

	    //public ActionResult GetColors(int Id, string size)
	    //{
		   // List<string> list = _repo.FullItems.Where(x => x.ItemId == Id && x.Size == size).Select(x => x.Color).Distinct().ToList();
		   // return PartialView("_GetColors", list);
	    //}

	}
}