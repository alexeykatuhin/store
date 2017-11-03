using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Abstract;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ItemController : Controller
    {
	    private IItemRepository _repo;
	    public int pageSize = 4;
	    public ItemController(IItemRepository repo)
	    {
		    _repo = repo;
	    }

	
		public ViewResult List(int page = 1)
		{
			ItemListViewModel model = new ItemListViewModel()
			{
				Items	= _repo.Items
				.OrderBy(game => game.Id)
				.Skip((page - 1) * pageSize)
				.Take(pageSize),
				PagingInfo = new PagingInfo()
				{
					CurrentPage = page,
					ItemsPerPage = pageSize,
					TotalItems = _repo.Items.Count()
				}
			}; 
			return View(model);
		}

	    public ActionResult GetList(int page = 1)
	    {
			ItemListViewModel model = new ItemListViewModel()
			{
				Items = _repo.Items
	.OrderBy(game => game.Id)
	.Skip((page - 1) * pageSize)
	.Take(pageSize),
				PagingInfo = new PagingInfo()
				{
					CurrentPage = page,
					ItemsPerPage = pageSize,
					TotalItems = _repo.Items.Count()
				}
			};
		    return PartialView("GetList",model);
	    }
	}
}