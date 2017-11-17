using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
	public class Cart
	{
		private List<CartLine> lineCollection = new List<CartLine>();

		public void AddItem(Item item, int quantity, string url271, string url)
		{
			CartLine line = lineCollection
				.Where(g => g.Item.Id == item.Id)
				.FirstOrDefault();

			if (line == null)
			{
				lineCollection.Add(new CartLine
				{
					Item = item,
					Quantity = quantity,
					ImageUrl = url,
					ImgUrl_271_171 = url271
				});
			}
			else
			{
				line.Quantity += quantity;
			}
		}

		public void RemoveLine(Item item)
		{
			lineCollection.RemoveAll(l => l.Item.Id == item.Id);
		}

		public decimal ComputeTotalValue()
		{
			return lineCollection.Sum(e => Convert.ToDecimal(e.Item.Price) * e.Quantity);

		}
		public void Clear()
		{
			lineCollection.Clear();
		}

		public IEnumerable<CartLine> Lines
		{
			get { return lineCollection; }
		}
	}
	public class CartLine
	{
		public Item Item { get; set; }
		public int Quantity { get; set; }
		public string ImageUrl { get; set; }
		public string ImgUrl_271_171 { get; set; }
	}
}