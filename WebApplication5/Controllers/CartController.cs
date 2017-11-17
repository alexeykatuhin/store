using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Abstract;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class CartController : Controller
    {
	    private IItemRepository _repo;

	    public CartController(IItemRepository repo)
	    {
		    _repo = repo;
	    }
		public ViewResult Index(string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = GetCart(),
				ReturnUrl = returnUrl
			});
		}
		public ActionResult AddToCart(int Id, string returnUrl)
		{
			Item game = _repo.Items
				.FirstOrDefault(g => g.Id == Id);

			if (game != null)
			{
				GetCart().AddItem(game, 1, _repo.Images.First(x=>x.ItemId == Id && x.IsHead).ImgUrl_271_171, _repo.Images.First(x => x.ItemId == Id && x.IsHead).ImgUrl_75_75);
			}
			var script = @"var prev =parseInt($('#cartSpan').html());
if (prev == 0){
$('#cartSpan').css('display', 'block');
}
			$('.badge').first().html(prev + 1);";
			return JavaScript(script);
		}
		public ActionResult RemoveFromCart(int Id)
		{
			Item game = _repo.Items
				.FirstOrDefault(g => g.Id == Id);

			if (game != null)
			{
				GetCart().RemoveLine(game);
			}
			Cart cart = GetCart();
			if (cart.Lines != null && cart.Lines.Count() != 0)
			{
				return PartialView("_GetViewCart", cart.Lines);
			}
			else
			{
				return PartialView("_GetViewEmptyCart");
			}
		}
		public Cart GetCart()
		{
			Cart cart = (Cart)Session["Cart"];
			if (cart == null)
			{
				cart = new Cart();
				Session["Cart"] = cart;
			}
			return cart;
		}

	    public ActionResult GetViewCart()
	    {
		    Cart cart = GetCart();
		    if (cart.Lines != null && cart.Lines.Count() != 0)
		    {
			    return PartialView("_GetViewCart", cart.Lines);
		    }
		    else
		    {
			    return PartialView("_GetViewEmptyCart");
		    }
	    }

	    public string GetQuantity()
	    {
			Cart cart = GetCart();
			int res = 0;
			if (cart.Lines != null && cart.Lines.Count() != 0)
			{
				foreach (CartLine cartLine in cart.Lines)
				{
					res += cartLine.Quantity;
				}
			}
			return res.ToString();
		}



		public ActionResult Cart(int Id=0, int newQuantity=0)
		{
			Cart cart = GetCart();
			if (Id!=0)
				cart.Lines.First(x => x.Item.Id == Id).Quantity = newQuantity;
			if (cart.Lines == null || cart.Lines.Count() == 0)
				return View("_EmptyCart");
			return View(cart.Lines);
		}



		public PartialViewResult CartPreview()
	    {
			Cart cart = GetCart();
			int res = 0;
			if (cart.Lines != null && cart.Lines.Count() != 0)
			{
				foreach (CartLine cartLine in cart.Lines)
				{
					res += cartLine.Quantity;
				}
				return PartialView("_CartPreview",res);
			}
		    return PartialView("_EmptyCartPreview");

	    }

		public ActionResult RemoveFromBigCart(int Id)
		{
			Item game = _repo.Items
				.FirstOrDefault(g => g.Id == Id);

			if (game != null)
			{
				GetCart().RemoveLine(game);
			}
			Cart cart = GetCart();
			if (cart.Lines != null && cart.Lines.Count() != 0)
			{
				return View("Cart", cart.Lines);
			}
			else
			{
				return View("_EmptyCart");
			}
		}

	}
}