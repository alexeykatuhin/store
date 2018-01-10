using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
	public class ShippingDetails
	{
		[Required(ErrorMessage = "Укажите как вас зовут")]
		public string Name { get; set; }
		[Required(ErrorMessage = "Вставьте адрес доставки")]
		public string Line1 { get; set; }
		[Required(ErrorMessage = "Укажите город")]
		public string City { get; set; }
		[Required(ErrorMessage = "Укажите страну")]
		public string Country { get; set; }
	}
}