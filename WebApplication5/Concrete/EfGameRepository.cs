using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Abstract;
using WebApplication5.Models;

namespace WebApplication5.Concrete
{
	public class EfGameRepository: IItemRepository
	{
		GameDataEntities _ctx = new GameDataEntities();
		public IEnumerable<Item> Items => _ctx.Item;
	}
}