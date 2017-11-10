using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
	public class ItemListViewModel
	{
		public IEnumerable<ItemViewModel> Items { get; set; }
		public int Count { get; set; }
		//public PagingInfo PagingInfo { get; set; }
		public string CurrentCategory { get; set; }
	}
}