﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
	public class ItemListViewModel
	{
		public IEnumerable<Item> Items { get; set; }
		public int CurrentPage { get; set; }
		//public PagingInfo PagingInfo { get; set; }
	}
}