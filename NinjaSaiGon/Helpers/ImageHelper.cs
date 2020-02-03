using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace NinjaSaiGon.Admin.Helpers
{
    public class ImageHelper
    {
        public static string ScaleImage(string webRootPath, string imagePath, int boxWidth)
        {

            var original = new Bitmap(imagePath);
            var min = Math.Min(original.Height, original.Width);

            Bitmap target = new Bitmap(boxWidth, boxWidth);

            using (Graphics g = Graphics.FromImage(target))
            {
                g.DrawImage(original, new Rectangle(0, 0, boxWidth, boxWidth),
                                 new Rectangle((original.Width - min) / 2, (original.Height - min) / 2, min, min),
                                 GraphicsUnit.Pixel);
            }

            var year = DateTime.Now.Year; var month = DateTime.Now.Month;
            var path = Path.Combine(
              webRootPath,
              string.Format("uploaded\\{0}\\{1}", year, month));
            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);
            var fileName = string.Format("{0}_{1}.jpg", Path.GetFileNameWithoutExtension(imagePath), boxWidth);

            var newPath = Path.Combine(path, fileName);
            target.Save(newPath, ImageFormat.Jpeg);

            return string.Format("/uploaded/{0}/{1}/{2}", year, month, fileName);
        }
    }
}