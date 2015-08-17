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
    // TODO: Add in profiles to resize images to standard sizes required for app store pages
    /*
    class ITunesSettings
    {
        public static readonly string[] validExtensions = new string[] { ".png", ".jpg", ".jpeg" };
        private Dictionary<string, Size> resolutions = new Dictionary<string, Size> { 
            {"5.5", new Size { Width = 2208, Height = 1242 } }, 
            {"4.7", new Size { Width = 1334, Height = 750 } }, 
            {"4", new Size { Width = 1136, Height = 640 } }, 
            {"3.5", new Size { Width = 960, Height = 600 } },
            {"ipad", new Size { Width = 2048, Height = 1536 } },
            {"mac", new Size { Width = 2880, Height = 1800 } }
        };

        public string SourceDir { get; set; }
        public string DestDir { get; set; }

        public void Convert()
        {
            this.Validate();

            if (!Directory.Exists(DestDir))
                Directory.CreateDirectory(DestDir);

            foreach (string key in this.resolutions.Keys)
                if (!Directory.Exists(Path.Combine(this.DestDir, key)))
                    Directory.CreateDirectory(Path.Combine(this.DestDir, key));

            string[] files = Directory.GetFiles(this.SourceDir);

            foreach (string file in files)
            {
                FileInfo fileInfo = new FileInfo(file);
                if (validExtensions.Contains(fileInfo.Extension.ToLower()))
                {
                    using (Image img = Image.FromFile(file))
                    {
                        foreach (var kvp in this.resolutions)
                        {
                            int width = img.Height < img.Width ? kvp.Value.Width : kvp.Value.Height;
                            int height = img.Height < img.Width ? kvp.Value.Height : kvp.Value.Width;
                            using (Bitmap bmp = this.ResizeImage(img, width, height))
                            {
                                string destFile = Path.Combine(this.DestDir, kvp.Key, fileInfo.Name);
                                Console.WriteLine(destFile);
                                if (bmp != null)
                                    bmp.Save(destFile);
                            }
                        }
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
            if (string.IsNullOrWhiteSpace(this.SourceDir))
                throw new ArgumentException("SourceDir not provided");
            if (string.IsNullOrWhiteSpace(this.DestDir))
                throw new ArgumentException("DestDir not provided");

            if (!Directory.Exists(this.SourceDir))
                throw new ArgumentException("SourceDir doesn't exist.");
            if (Directory.GetFiles(this.SourceDir).Length == 0)
                throw new ArgumentException("SourceDir contains 0 files.");
        }     
    }
     */
}
