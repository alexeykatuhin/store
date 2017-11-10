using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication5.Abstract;
using WebApplication5.Models;

namespace WebApplication5.Concrete
{
	public class EfImageRepository: IImageRepository
	{
		GameDataEntities1 _ctx = new GameDataEntities1();
		public IEnumerable<Image> Images => _ctx.Image;
	
	}
}