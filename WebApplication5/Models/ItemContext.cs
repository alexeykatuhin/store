using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
	public class ItemContext: DbContext
	{
		public DbSet<Item> Item { get; set; }
		public DbSet<FullItem> FullItem { get; set; }
		public DbSet<Image> Image { get; set; }
		public DbSet<Group> Group { get; set; }
	}
}