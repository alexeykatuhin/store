using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
	public class ImageViewModel
	{
		public IEnumerable<Image> Images { get; set; }
		public int  Id { get; set; }
	}
}