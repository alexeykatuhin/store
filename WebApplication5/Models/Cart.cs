using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
	public class Cart
	{
		private List<CartLine> lineCollection = new List<CartLine>();

		public void AddItem(Item item, int quantity, int? ImageId, string size, List<string> sizeList)
		{
			CartLine line = lineCollection
				.Where(g => g.Item.Id == item.Id && g.Size == size)
				.FirstOrDefault();

			if (line == null)
			{
				lineCollection.Add(new CartLine
				{
					Item = item,
					Quantity = quantity,
					ImageId = ImageId, 
					Size = size ?? sizeList.FirstOrDefault(),
					SizeList = sizeList
				});
			}
			else
			{
				line.Quantity += quantity;
			}
		}

		public void RemoveLine(Item item, string size)
		{
			lineCollection.RemoveAll(l => l.Item.Id == item.Id && l.Size == size);
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
		public int? ImageId { get; set; }
		public string Size { get; set; }
		public List<string> SizeList { get; set; }
	}
}