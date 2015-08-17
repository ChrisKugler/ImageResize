using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ImageResize
{
    public class Resizer
    {
        public static readonly string[] validExtensions = new string[] { ".png", ".jpg", ".jpeg" };
        public string SourceFile { get; set; }
        public string DestFile { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }

        public void Convert()
        {
            this.Validate();                                
                                    
            FileInfo fileInfo = new FileInfo(this.SourceFile);
            if (validExtensions.Contains(fileInfo.Extension.ToLower()))
            {
                using (Image img = Image.FromFile(this.SourceFile))
                {
                   
                    using (Bitmap bmp = this.ResizeImage(img, this.Width, this.Height))
                    {
                        this.DestFile = this.SourceFile.Replace(fileInfo.Extension, string.Format("{0}_{1}{2}", this.Width, this.Height, fileInfo.Extension));                                                 
                        if (bmp != null)
                            bmp.Save(this.DestFile);
                    }                    
                }
            }            
        }

        public Bitmap ResizeImage(Image image, int width, int height)
        {
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        private void Validate()
        {
            if (string.IsNullOrWhiteSpace(this.SourceFile))
                throw new ArgumentException("SourceDir not provided.");            

            if(!File.Exists(this.SourceFile))
                throw new ArgumentException("SourceDir doesn't exist.");            

            if(this.Height <= 0)
                throw new ArgumentException("Height must be greater than 0.");

            if(this.Width <=0)
                throw new ArgumentException("Width must be greater than 0.");
        }
    }
}
