using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Models;

namespace WebApplication5.Abstract
{
	public interface IItemRepository
	{
		IEnumerable<Item> Items { get; }
		IEnumerable<Image> Images { get; }
	}
}