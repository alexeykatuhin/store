﻿using System;
using System.Collections.Generic;
using System.Drawing;
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
	    private IOrderProcessor _orderProcessor;

	    public CartController(IItemRepository repo, IOrderProcessor orderProcessor)
	    {
		    _orderProcessor = orderProcessor; 
		    _repo = repo;
	    }
		public ViewResult Index(Cart cart, string returnUrl)
		{
			return View(new CartIndexViewModel
			{
				Cart = cart,
				ReturnUrl = returnUrl
			});
		}
		public ActionResult AddToCart(Cart cart,int Id, string returnUrl, string size = null)
		{
			Item game = _repo.Items
				.FirstOrDefault(g => g.Id == Id);

			if (game != null)
			{
				cart.AddItem(game, 1,
					_repo.Images.Any(x => x.ItemId == Id) ? (int?) _repo.Images.First(x => x.ItemId == Id && x.IsHead).Id : null,
					size, _repo.FullItems.Where(x=>x.ItemId == Id).Select(x=>x.Size).ToList());
			}
			var script = @"var prev =parseInt($('#cartSpan').html());
if (prev == 0){
$('#cartSpan').css('display', 'block');
}
			$('.badge').first().html(prev + 1);";
			return JavaScript(script);
		}
		public ActionResult RemoveFromCart(Cart cart, int Id, string size)
		{
			Item game = _repo.Items
				.FirstOrDefault(g => g.Id == Id);

			if (game != null)
			{
				cart.RemoveLine(game, size);
			}
			if (cart.Lines != null && cart.Lines.Count() != 0)
			{
				return PartialView("_GetViewCart", cart.Lines);
			}
			else
			{
				return PartialView("_GetViewEmptyCart");
			}
		}
		//public Cart GetCart()
		//{
		//	Cart cart = (Cart)Session["Cart"];
		//	if (cart == null)
		//	{
		//		cart = new Cart();
		//		Session["Cart"] = cart;
		//	}
		//	return cart;
		//}

	    public ActionResult GetViewCart(Cart cart)
	    {
		    if (cart.Lines != null && cart.Lines.Count() != 0)
		    {
			    return PartialView("_GetViewCart", cart.Lines);
		    }
		    else
		    {
			    return PartialView("_GetViewEmptyCart");
		    }
	    }

	    public string GetQuantity(Cart cart)
	    {
	
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



		public ActionResult Cart(Cart cart, int Id=0, int newQuantity=0, string size = null, string oldSize = null)
		{

			if (Id != 0)
			{
				//нажали на упдате количества
				if (oldSize == null)
				{
					//защита от дурака
					if (newQuantity == 0)
						TempData["message"] = string.Format("Введите корректное значение");
					else
					{
						//проверка на наличие
						int cnt = _repo.FullItems.First(x => x.ItemId == Id && x.Size == size).Quantity;
						if (cnt >= newQuantity)
							cart.Lines.First(x => x.Item.Id == Id && x.Size == size).Quantity = newQuantity;
						else
							TempData["message"] = string.Format(
								"Нет в наличии данного количества товаров. Вы можете заказать до {0} товаров", cnt);
					}
				}
				//нажали на упдате размера
				else
				{
					//поменили чо то
					if (size != oldSize)
					{
						//уже есть такой размер
						if (cart.Lines.Any(x=>x.Item.Id == Id && x.Size == size))
							TempData["message"] = string.Format("Данный размер уже есть в корзине");
						else
						{
							cart.Lines.First(x => x.Item.Id == Id && x.Size == oldSize).Size = size;
						}
					}
				}


			}
			if (cart.Lines == null || cart.Lines.Count() == 0)
				return View("_EmptyCart");
			return View(cart.Lines);
		}



		public PartialViewResult CartPreview(Cart cart)
	    {
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

		public ActionResult RemoveFromBigCart(Cart cart, int Id, string size)
		{
			Item game = _repo.Items
				.FirstOrDefault(g => g.Id == Id);

			if (game != null)
			{
				cart.RemoveLine(game, size);
			}

			if (cart.Lines != null && cart.Lines.Count() != 0)
			{
				return View("Cart", cart.Lines);
			}
			else
			{
				return View("_EmptyCart");
			}
		}

		[HttpGet]
	    public ViewResult Checkout()
		{
			return View(new ShippingDetails());
		}


		[HttpPost]
		public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
		{
			if (!cart.Lines.Any())
			{
				ModelState.AddModelError("", "Извините, ваша корзина пуста!");
			}

			if (ModelState.IsValid)
			{
				_orderProcessor.ProcessOrder(cart, shippingDetails);
				cart.Clear();
				return View("Completed");
			}
			else
			{
				return View(shippingDetails);
			}
		}

	    [HttpPost]
	    public void test(int Id, string size)
	    {
		    var dd = 22;
	    }
	}
}