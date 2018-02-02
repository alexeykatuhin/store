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

		public void AddImg(Image img)
		{
			if (img.Id == 0 || !_cntx.Image.Any(x => x.Id == img.Id))
			{
				_cntx.Image.Add(img);
			}
			else
			{
				Image image = _cntx.Image.Find(img.Id);
				image.ImageData = img.ImageData;
				image.ImageMimeType = img.ImageMimeType;
				image.IsHead = img.IsHead;
				image.ItemId = img.ItemId;
			}
			_cntx.SaveChanges();
		}

		public void DeleteImage(int Id)
		{
			Image Img = _cntx.Image.Find(Id);
			_cntx.Image.Remove(Img);
			_cntx.SaveChanges();
		}
		public IEnumerable<FullItem> FullItems => _cntx.FullItem;
		public IEnumerable<Group> Groups => _cntx.Group;
	}
}