using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
	public class ItemBigViewModel
	{
		public Item Item { get; set; }
		public int GroupId { get; set; }
		public List<FullItem> FullItems  { get; set; }
		public List<Image> Images { get; set; }
		public List<ItemViewModel> OtherColors { get; set; }
		

	}

	public class ItemBigViewModelAdmin 
	{
		public List<int> GroupIds { get; set; }
		public List<ItemBigViewModel> ModelList { get; set; }
	}
}