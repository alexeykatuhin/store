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
			{
				
				_cntx.Item.Add(item);
				_cntx.SaveChanges();
				int itmId = _cntx.Item.Max(x => x.Id);

				int grpId;
				if (_cntx.Group.Any())
					grpId = _cntx.Group.Max(x => x.GroupId) + 1;
				else
					grpId = 1;

				Group gr = new Group()
				{
					GroupId = grpId,
					ItemId = itmId
				};
				_cntx.Group.Add(gr);
			}

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

				if (_cntx.Group.Any(x => x.ItemId == Id))
					_cntx.Group.Remove(_cntx.Group.First(x => x.ItemId == Id));


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

		public void SaveFullItem(FullItem item)
		{
			if (_cntx.FullItem.Any(x => x.Id == item.Id))
			{
				FullItem itemBase = _cntx.FullItem.Find(item.Id);
				itemBase.ItemId = item.ItemId;
				itemBase.Quantity = item.Quantity;
				itemBase.Size = item.Size;
			}
			else
				_cntx.FullItem.Add(item);
			_cntx.SaveChanges();

		}

		public void DeleteFullItem(int Id)
		{
			if (_cntx.FullItem.Any(x => x.Id == Id))
			{
				FullItem item = _cntx.FullItem.Find(Id);
				_cntx.FullItem.Remove(item);
				_cntx.SaveChanges();
			}

		}
		public IEnumerable<Group> Groups => _cntx.Group;

		public void SaveGroup(Group group)
		{
			Group gr = new Group();
			if (_cntx.Group.Any(x => x.ItemId == group.ItemId))
			{
				int id = _cntx.Group.First(x => x.ItemId == group.ItemId).ID;
				gr = _cntx.Group.Find(id);
				gr.ItemId = group.ItemId;
				gr.GroupId = group.GroupId;
			}
			else
			{
				_cntx.Group.Add(group);
			}
			_cntx.SaveChanges();
		}
	}
}