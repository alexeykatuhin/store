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

	
		public ViewResult List(int page = 1)
		{
			curPage = 1;
			ItemListViewModel model = new ItemListViewModel()
			{
				Items	= _repo.Items
				.OrderBy(game => game.Id)
				.Skip((page - 1) * pageSize)
				.Take(pageSize), Count = _repo.Items.Count()
			}; 
			return View(model);
		}

	    public ActionResult GetList()
	    {
	
		    ItemListViewModel model = new ItemListViewModel()
			{
				Items = _repo.Items
	.OrderBy(game => game.Id)
	.Skip((++curPage - 1) * pageSize)
	.Take(pageSize)
			};
		    return PartialView("GetList",model);
	    }
	}
}