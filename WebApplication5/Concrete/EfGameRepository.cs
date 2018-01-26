using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Abstract;
using WebApplication5.Classes;
using WebApplication5.Models;

namespace WebApplication5.Concrete
{
	public class EfGameRepository: IItemRepository
	{
		ItemContext _cntx = new ItemContext();
		public IEnumerable<Item> Items => _cntx.Item;
		public void SaveItem(Item item)
		{
			if (item.Id == 0)
				_cntx.Item.Add(item);
			else
			{
				Item dbEntry = _cntx.Item.Find(item.Id);
				if (dbEntry != null)
				{
					dbEntry.Name = item.Name;
					dbEntry.Description = item.Description;
					dbEntry.Price = item.Price;
					dbEntry.Category = item.Category;
					dbEntry.FullDescription = item.FullDescription;
				}
			}
			_cntx.SaveChanges();
		}
		public Item DeleteItem(int Id)
		{
			Item dbEntry = _cntx.Item.Find(Id);
			if (dbEntry != null)
			{
				_cntx.Item.Remove(dbEntry);
				List<FullItem> lstItms = _cntx.FullItem.Where(x => x.ItemId == Id).ToList();
				if (lstItms.Count != 0)
					_cntx.FullItem.RemoveRange(lstItms);
				List<Image> listImg = _cntx.Image.Where(x => x.ItemId == Id).ToList();
				if (listImg.Count != 0)
					_cntx.Image.RemoveRange(listImg);


				_cntx.SaveChanges();

			}
			return dbEntry;
		}
		public IEnumerable<Image> Images => _cntx.Image;

		public void AddImg(int idItem,string url, bool isHead=false, int id = 0, string shortFileName = null)
		{
			Image img;

			img = new Image() {IsHead = isHead, ItemId = idItem};
			if (url == "/Content/Images/NoImg.jpg")
			{
				img.ImgUrl = url;
				img.ImgUrl_271_171 = "/Content/Images/NoImg271_171.jpg";
				img.ImgUrl_75_75 = "/Content/Images/NoImg75_75.jpg";
			}
			else
				ImageConvertor.SetImages(ref img, url);

			if (id == 0)
				_cntx.Image.Add(img);
			else
			{
				Image img1 =_cntx.Image.Find(id);
				img1.ImgUrl = img.ImgUrl;
				img1.ImgUrl_271_171 = img.ImgUrl_271_171;
				img1.ImgUrl_75_75 = img.ImgUrl_75_75;
				img1.IsHead = img.IsHead;
				img1.ItemId = img.ItemId;
			}


			_cntx.SaveChanges();








			
		}

		public IEnumerable<FullItem> FullItems => _cntx.FullItem;
	}
}