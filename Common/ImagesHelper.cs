using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.IO;
using System.Drawing;

namespace Common
{
    public static class ImagesHelper
    {
        /// <summary>
        /// 处理图片方法
        /// </summary>
        /// <param name="images"></param>
        /// <param name="path"></param>
        public static List<string> ImagesSave(IEnumerable<HttpPostedFileBase> images , string path) {
            List<string> bigImages = new List<string>();
            foreach (HttpPostedFileBase image in images)
            {
                string imgName = Guid.NewGuid().ToString();
                string extName = Path.GetExtension(image.FileName);

                string realPath = path + imgName + extName;
                image.SaveAs(realPath);
                bigImages.Add(imgName + extName);
            }
            return bigImages;
        }

        public static List<string> smallImage(IEnumerable<HttpPostedFileBase> images , string path) {
            List<string> smallImages = new List<string>();
            foreach (var img in images)
            {
                Image bigImage = Image.FromStream(img.InputStream);
                int longSize = 200;
                int w, h;
                if (bigImage.Width > bigImage.Height)
                {
                    w = longSize;
                    h = w * bigImage.Height / bigImage.Width;
                }
                else
                {
                    h = longSize;
                    w = h * bigImage.Width / bigImage.Height;
                }
                Image smallImage = new Bitmap(bigImage,new Size(w,h));
                string smallImageName = Guid.NewGuid().ToString();
                string extName = Path.GetExtension(img.FileName);
                smallImage.Save(path + smallImageName + extName);
                smallImages.Add(smallImageName + extName);
            }
            return smallImages;
        }
    }
}
