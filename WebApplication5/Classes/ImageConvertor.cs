using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Razor.Text;

namespace WebApplication5.Classes
{
	public static class ImageConvertor
	{
		//public static void SetImages(ref Models.Image img, string url)
		//{
		//	img.ImgUrl = url;
		//	bool dsfs = File.Exists(Directory.GetCurrentDirectory()+"//Content//Images//"+url);

		//	Image source = Image.FromFile(url);

		//	Image img271 = ScaleImage(source, 271, 171);

		//	Image img75 = ScaleImage(source, 75, 75);
		//	img75.Save();

		//	int index = 0;
		//	for (int i = url.ToCharArray().Length-1; i >0; i--)
		//	{
		//		if (url[i] == '.')
		//			index = i;
		//	}

		//	string url271 = url.Insert(index, "271_171");
		//	string url75 = url.Insert(index, "75_75");

		//	img271.Save(url271);
		//	img75.Save(url75);

		//	img.ImgUrl_271_171 = url271;
		//	img.ImgUrl_75_75 = url75;

		//}
		public static	byte[]  ScaleImage(byte[] source1, int width, int height)
	{
		Image source = (Image) ((new ImageConverter()).ConvertFrom(source1));
			Image dest = new Bitmap(width, height);
			using (Graphics gr = Graphics.FromImage(dest))
			{
				gr.FillRectangle(Brushes.White, 0, 0, width, height);  // Очищаем экран
				gr.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;

				float srcwidth = source.Width;
				float srcheight = source.Height;
				float dstwidth = width;
				float dstheight = height;

				if (srcwidth <= dstwidth && srcheight <= dstheight)  // Исходное изображение меньше целевого
				{
					int left = (width - source.Width) / 2;
					int top = (height - source.Height) / 2;
					gr.DrawImage(source, left, top, source.Width, source.Height);
				}
				else if (srcwidth / srcheight > dstwidth / dstheight)  // Пропорции исходного изображения более широкие
				{
					float cy = srcheight / srcwidth * dstwidth;
					float top = ((float)dstheight - cy) / 2.0f;
					if (top < 1.0f) top = 0;
					gr.DrawImage(source, 0, top, dstwidth, cy);
				}
				else  // Пропорции исходного изображения более узкие
				{
					float cx = srcwidth / srcheight * dstheight;
					float left = ((float)dstwidth - cx) / 2.0f;
					if (left < 1.0f) left = 0;
					gr.DrawImage(source, left, 0, cx, dstheight);
				}


				ImageConverter _imageConverter = new ImageConverter();
				byte[] xByte = (byte[])_imageConverter.ConvertTo(dest, typeof(byte[]));

				return xByte;
			}
		}
	}
}