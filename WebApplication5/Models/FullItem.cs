﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System.ComponentModel.DataAnnotations;

namespace WebApplication5.Models
{
	using System;
	using System.Collections.Generic;

	public partial class FullItem
	{
		public int Id { get; set; }
		public int ItemId { get; set; }
		[Required(ErrorMessage = "Пожалуйста, укажите размер")]
		public string Size { get; set; }
		[Range(1, int.MaxValue, ErrorMessage = "Пожалуйста, введите положительное значение для количества")]
		public int Quantity { get; set; }
	}
}
