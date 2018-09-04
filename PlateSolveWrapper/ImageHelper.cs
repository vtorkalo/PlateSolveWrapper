using System;
using System.Collections.Generic;
using System.Diagnostics;
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


        public static Bitmap GetMonochromeBitmap(int[,] data, int maxAdu)
        {
            int IMAGE_WIDTH = data.GetLength(0);
            int IMAGE_HEIGHT = data.GetLength(1);
            var b16bpp = new Bitmap(IMAGE_WIDTH, IMAGE_HEIGHT, System.Drawing.Imaging.PixelFormat.Format8bppIndexed);

            var rect = new Rectangle(0, 0, IMAGE_WIDTH, IMAGE_HEIGHT);
            var bitmapData = b16bpp.LockBits(rect, ImageLockMode.WriteOnly, b16bpp.PixelFormat);
            var numberOfBytes = bitmapData.Stride * IMAGE_HEIGHT;
            var bitmapBytes = new byte[IMAGE_WIDTH * IMAGE_HEIGHT];
            var random = new Random();

            for (int x = 0; x < IMAGE_WIDTH; x++)
            {
                for (int y = 0; y < IMAGE_HEIGHT; y++)
                {
                    var i = ((y * IMAGE_WIDTH) + x);
                    bitmapBytes[i] = ScaleToByte(data[x, y], maxAdu);
                }
            }

            var ptr = bitmapData.Scan0;
            Marshal.Copy(bitmapBytes, 0, ptr, bitmapBytes.Length);
            b16bpp.UnlockBits(bitmapData);

            return b16bpp;
        }


        public static Bitmap GetColorBitmap(int[,,] data, int maxAdu)
        {
            int IMAGE_WIDTH = data.GetLength(0);
            int IMAGE_HEIGHT = data.GetLength(1);
            var bitmapColor = new Bitmap(IMAGE_WIDTH, IMAGE_HEIGHT, System.Drawing.Imaging.PixelFormat.Format32bppRgb);

            var rect = new Rectangle(0, 0, IMAGE_WIDTH, IMAGE_HEIGHT);
            var bitmapData = bitmapColor.LockBits(rect, ImageLockMode.ReadOnly, bitmapColor.PixelFormat);
            var numberOfBytes = bitmapData.Stride * IMAGE_HEIGHT;
            var bitmapBytes = new int[IMAGE_WIDTH * IMAGE_HEIGHT];

            for (int x = 0; x < IMAGE_WIDTH; x++)
            {
                for (int y = 0; y < IMAGE_HEIGHT; y++)
                {
                    var i = y * IMAGE_WIDTH + x;

                    byte a = 0;
                    byte b = ScaleToByte(data[x, y, 0], maxAdu);
                    byte g = ScaleToByte(data[x, y, 1], maxAdu);
                    byte r = ScaleToByte(data[x, y, 2], maxAdu);

                    byte[] source = new byte[] { b, g, r, a };
                    byte[] target = new byte[source.Length * sizeof(byte)];
                    Buffer.BlockCopy(source, 0, target, 0, source.Length * sizeof(byte));

                    bitmapBytes[i] = BitConverter.ToInt32(target, 0);
                }
            }

            var ptr = bitmapData.Scan0;
            Marshal.Copy(bitmapBytes, 0, ptr, bitmapBytes.Length);
            bitmapColor.UnlockBits(bitmapData);

            return bitmapColor;
        }

        public static Bitmap GetBitmap(Array data, int maxAdu)
        {
            if (data.Rank == 2)
            {
                return GetMonochromeBitmap((int[,])data, maxAdu);
            }
            else if (data.Rank == 3)
            {
                return GetColorBitmap((int[,,])data, maxAdu);
            }
            else
            {
                return null;
            }
        }

        private static byte ScaleToByte(int value, int maxValue)
        {
            if (maxValue <= byte.MaxValue)
            {
                return (byte)value;
            }

            double scale = (double)value / (double)maxValue;

            byte result = (byte)(byte.MaxValue * scale);

            return result;
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

            JpegBitmapEncoder encoder = new JpegBitmapEncoder();

           // encoder.Compression = TiffCompressOption.Zip;
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
                case System.Drawing.Imaging.PixelFormat.Format64bppArgb:
                    pixelFormats = PixelFormats.Rgba64;
                    break;
                case System.Drawing.Imaging.PixelFormat.Format32bppRgb:
                    pixelFormats = PixelFormats.Bgra32;
                    break;
            }

            return pixelFormats;
        }
    }
}
