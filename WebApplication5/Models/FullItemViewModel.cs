using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
	public class FullItemViewModel
	{
		public IEnumerable<FullItem> FullItems { get; set; }
		public int Id { get; set; }
	}
}