using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
	public class Image
	{
		public int Id { get; set; }
		public bool IsHead { get; set; }
		public int ItemId { get; set; }
		public byte[] ImageData { get; set; }
		public string ImageMimeType { get; set; }
	}
}