﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5.Abstract
{
	public interface IItemRepository
	{
		IEnumerable<Item> Items { get; }
		void SaveItem(Item item);
		Item DeleteItem(int Id);
		IEnumerable<Image> Images { get; }
		void SaveImg(int id, string url, string res = null);
		IEnumerable<FullItem> FullItems { get; }

	}
}