using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication5.Abstract;
using WebApplication5.Classes;
using WebApplication5.Models;

namespace WebApplication5.Controllers
{
    public class ImageController : Controller
    {
	    private IItemRepository _repo;

	    public ImageController(IItemRepository repo)
	    {
		    _repo = repo;
	    }
		public FileContentResult GetImage(int? Id)
		{
			Image img = _repo.Images
				.FirstOrDefault(g => g.Id == Id);

			if (img != null)
			{
				return File(img.ImageData, img.ImageMimeType);
			}
			else
			{
				Image img1 = _repo.Images.First(x => x.Id == 1);
				return File(img1.ImageData, img1.ImageMimeType);
			}
		}
		public FileContentResult GetImage271(int? Id)
		{
			Image img = _repo.Images
				.FirstOrDefault(g => g.Id == Id);

			if (img != null)
			{
				
				return File(ImageConvertor.ScaleImage(img.ImageData, 271,171), img.ImageMimeType);
			}
			else
			{
				Image img1 = _repo.Images.First(x => x.Id == 1);
				return File(ImageConvertor.ScaleImage(img1.ImageData, 271,171), img1.ImageMimeType);
			}
		}
		public FileContentResult GetImage75(int? Id)
		{
			Image img = _repo.Images
				.FirstOrDefault(g => g.Id == Id);

			if (img != null)
			{

				return File(ImageConvertor.ScaleImage(img.ImageData, 75, 75), img.ImageMimeType);
			}
			else
			{
				Image img1 = _repo.Images.First(x => x.Id == 1);
				return File(ImageConvertor.ScaleImage(img1.ImageData, 75, 75), img1.ImageMimeType);
			}
		}
		public FileContentResult GetImage50(int? Id)
		{
			Image img = _repo.Images
				.FirstOrDefault(g => g.Id == Id);

			if (img != null)
			{

				return File(ImageConvertor.ScaleImage(img.ImageData, 50, 50), img.ImageMimeType);
			}
			else
			{
				Image img1 = _repo.Images.First(x => x.Id == 1);
				return File(ImageConvertor.ScaleImage(img1.ImageData, 75, 50), img1.ImageMimeType);
			}
		}
	}
}