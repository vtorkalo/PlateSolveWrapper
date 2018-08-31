using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace PlateSolveWrapper
{
    public static class ImageHelper
    {
        public static Bitmap GetBitmap(Array data)
        {
            int width = data.GetLength(0);
            int height = data.GetLength(1);

            Bitmap bmp = new Bitmap(width, height);
            bool isColor = data.Rank == 3;

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    if (isColor)
                    {
                        System.Drawing.Color color = System.Drawing.Color.FromArgb((int)data.GetValue(x, y, 0), (int)data.GetValue(x, y, 1), (int)data.GetValue(x, y, 2));
                        bmp.SetPixel(x, y, color);
                    }
                    else
                    {
                        int l = (int)data.GetValue(x, y);
                        System.Drawing.Color color = System.Drawing.Color.FromArgb(l, l, l);
                        bmp.SetPixel(x, y, color);
                    }
                }
            }

            return bmp;
        }

        public static Bitmap GetBitmapMonochrome(Array data)
        {
            int IMAGE_WIDTH = data.GetLength(0);
            int IMAGE_HEIGHT = data.GetLength(1);
            var b16bpp = new Bitmap(IMAGE_WIDTH, IMAGE_HEIGHT, System.Drawing.Imaging.PixelFormat.Format16bppGrayScale);

            var rect = new Rectangle(0, 0, IMAGE_WIDTH, IMAGE_HEIGHT);
            var bitmapData = b16bpp.LockBits(rect, ImageLockMode.WriteOnly, b16bpp.PixelFormat);
            // Calculate the number of bytes required and allocate them.
            var numberOfBytes = bitmapData.Stride * IMAGE_HEIGHT;
            var bitmapBytes = new short[IMAGE_WIDTH * IMAGE_HEIGHT];
            // Fill the bitmap bytes with random data.
            var random = new Random();
            for (int x = 0; x < IMAGE_WIDTH; x++)
            {
                for (int y = 0; y < IMAGE_HEIGHT; y++)
                {
                    var i = ((y * IMAGE_WIDTH) + x); // 16bpp

                    // Generate the next random pixel color value.
                    var value = (int)data.GetValue(x, y);

                    bitmapBytes[i] =(short) value;         // GRAY
                }
            }
            // Copy the randomized bits to the bitmap pointer.
            var ptr = bitmapData.Scan0;
            Marshal.Copy(bitmapBytes, 0, ptr, bitmapBytes.Length);

            // Unlock the bitmap, we're all done.
            b16bpp.UnlockBits(bitmapData);


            return b16bpp;            
        }

        public static void SaveBmp(Bitmap bmp, string path)
        {
            Rectangle rect = new Rectangle(0, 0, bmp.Width, bmp.Height);

            BitmapData bitmapData = bmp.LockBits(rect, ImageLockMode.ReadOnly, bmp.PixelFormat);

            var pixelFormats = ConvertBmpPixelFormat(bmp.PixelFormat);

            BitmapSource source = BitmapSource.Create(bmp.Width,
                                                      bmp.Height,
                                                      bmp.HorizontalResolution,
                                                      bmp.VerticalResolution,
                                                      pixelFormats,
                                                      null,
                                                      bitmapData.Scan0,
                                                      bitmapData.Stride * bmp.Height,
                                                      bitmapData.Stride);

            bmp.UnlockBits(bitmapData);


            FileStream stream = new FileStream(path, FileMode.Create);

            TiffBitmapEncoder encoder = new TiffBitmapEncoder();

            encoder.Compression = TiffCompressOption.Zip;
            encoder.Frames.Add(BitmapFrame.Create(source));
            encoder.Save(stream);

            stream.Close();
        }

        private static System.Windows.Media.PixelFormat ConvertBmpPixelFormat(System.Drawing.Imaging.PixelFormat pixelformat)
        {
            System.Windows.Media.PixelFormat pixelFormats = System.Windows.Media.PixelFormats.Default;

            switch (pixelformat)
            {
                case System.Drawing.Imaging.PixelFormat.Format32bppArgb:
                    pixelFormats = PixelFormats.Bgr32;
                    break;

                case System.Drawing.Imaging.PixelFormat.Format8bppIndexed:
                    pixelFormats = PixelFormats.Gray8;
                    break;

                case System.Drawing.Imaging.PixelFormat.Format16bppGrayScale:
                    pixelFormats = PixelFormats.Gray16;
                    break;
            }

            return pixelFormats;
        }
    }
}
