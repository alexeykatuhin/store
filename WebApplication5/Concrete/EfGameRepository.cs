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
		GameDataEntities1 _ctx1 = new GameDataEntities1();
		public IEnumerable<Image> Images => _ctx1.Image;
		GameDataEntities4 _ctx2 = new GameDataEntities4();
		public IEnumerable<FullItem> FullItems => _ctx2.FullItem;
	}
}