using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;

namespace Ruan_du_Plessis_25978888_ITRW316_Project
{
    public static class BitmapExtension
    {


        public enum BlurType
        {
            Average3x3,
            Average5x5,
            Average7x7,
            Average9x9,
            Average11x11
        }

        public static Bitmap CopyCanvas(this Bitmap bSource, int canvas)
        {
            
            int max = bSource.Width > bSource.Height ? bSource.Width : bSource.Height;
            float rate = 1.0f;
            rate = (float)max / (float)canvas;

            Bitmap bResult = (bSource.Width > bSource.Height ? new Bitmap(canvas, (int)(bSource.Height / rate))
                                    : new Bitmap((int)(bSource.Width / rate), canvas));

            using (Graphics gResult = Graphics.FromImage(bResult))
            {
                gResult.CompositingQuality = CompositingQuality.HighQuality;
                gResult.InterpolationMode = InterpolationMode.HighQualityBicubic;
                gResult.PixelOffsetMode = PixelOffsetMode.HighQuality;

                gResult.DrawImage(bSource,
                                        new Rectangle(0, 0,
                                            bResult.Width, bResult.Height),
                                        new Rectangle(0, 0,
                                            bSource.Width, bSource.Height),
                                            GraphicsUnit.Pixel);
                gResult.Flush();
            }

            return bResult;
        }

        private static Bitmap ConvoFilter(this Bitmap sourceBitmap,
                                                  double[,] filterMatrix,
                                                       double factor = 1,
                                                            int bias = 0)
        {
            BitmapData sourceData = sourceBitmap.LockBits(new Rectangle(0, 0,
                                     sourceBitmap.Width, sourceBitmap.Height),
                                                       ImageLockMode.ReadOnly,
                                                 PixelFormat.Format32bppArgb);

            byte[] pixelBuffer = new byte[sourceData.Stride * sourceData.Height];
            byte[] resultBuffer = new byte[sourceData.Stride * sourceData.Height];

            Marshal.Copy(sourceData.Scan0, pixelBuffer, 0, pixelBuffer.Length);
            sourceBitmap.UnlockBits(sourceData);

            double blue = 0.0;
            double green = 0.0;
            double red = 0.0;

            int filterWidth = filterMatrix.GetLength(1);
            int filterHeight = filterMatrix.GetLength(0);

            int filterOffset = (filterWidth - 1) / 2;
            int calcOffset = 0;

            int byteOffset = 0;

            for (int offsetY = filterOffset; offsetY <
                sourceBitmap.Height - filterOffset; offsetY++)
            {
                for (int offsetX = filterOffset; offsetX <
                    sourceBitmap.Width - filterOffset; offsetX++)
                {
                    blue = 0;
                    green = 0;
                    red = 0;

                    byteOffset = offsetY *
                                 sourceData.Stride +
                                 offsetX * 4;

                    for (int filterY = -filterOffset;
                        filterY <= filterOffset; filterY++)
                    {
                        for (int filterX = -filterOffset;
                            filterX <= filterOffset; filterX++)
                        {
                            calcOffset = byteOffset +
                                         (filterX * 4) +
                                         (filterY * sourceData.Stride);

                            blue += (double)(pixelBuffer[calcOffset]) *
                                    filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            green += (double)(pixelBuffer[calcOffset + 1]) *
                                     filterMatrix[filterY + filterOffset,
                                                        filterX + filterOffset];

                            red += (double)(pixelBuffer[calcOffset + 2]) *
                                   filterMatrix[filterY + filterOffset,
                                                      filterX + filterOffset];
                        }
                    }

                    blue = factor * blue + bias;
                    green = factor * green + bias;
                    red = factor * red + bias;

                    blue = (blue > 255 ? 255 :
                           (blue < 0 ? 0 :
                            blue));

                    green = (green > 255 ? 255 :
                            (green < 0 ? 0 :
                             green));

                    red = (red > 255 ? 255 :
                          (red < 0 ? 0 :
                           red));

                    resultBuffer[byteOffset] = (byte)(blue);
                    resultBuffer[byteOffset + 1] = (byte)(green);
                    resultBuffer[byteOffset + 2] = (byte)(red);
                    resultBuffer[byteOffset + 3] = 255;
                }
            }

            Bitmap resultBitmap = new Bitmap(sourceBitmap.Width, sourceBitmap.Height);

            BitmapData resultData = resultBitmap.LockBits(new Rectangle(0, 0,
                                     resultBitmap.Width, resultBitmap.Height),
                                                      ImageLockMode.WriteOnly,
                                                 PixelFormat.Format32bppArgb);

            Marshal.Copy(resultBuffer, 0, resultData.Scan0, resultBuffer.Length);
            resultBitmap.UnlockBits(resultData);

            return resultBitmap;
        }

        public static Bitmap BlurFilter(this Bitmap bSource,
                                                    BlurType bType)
        {
            Bitmap bResult = null;

            switch (bType)
            {
                case BlurType.Average3x3:
                    {
                        bResult = bSource.ConvoFilter(
                                       BlurMatrix.Average3x3, 1.0 / 9.0, 0);
                    }
                    break;
                case BlurType.Average5x5:
                    {
                        bResult = bSource.ConvoFilter(
                                       BlurMatrix.Average5x5, 1.0 / 25.0, 0);
                    }
                    break;
                case BlurType.Average7x7:
                    {
                        bResult = bSource.ConvoFilter(
                                       BlurMatrix.Average7x7, 1.0 / 49.0, 0);
                    }
                    break;
                case BlurType.Average9x9:
                    {
                        bResult = bSource.ConvoFilter(
                                       BlurMatrix.Average9x9, 1.0 / 81.0, 0);
                    }
                    break;
                case BlurType.Average11x11:
                    {
                        bResult = bSource.ConvoFilter(
                                BlurMatrix.Average11x11, 1.0 / 121.0, 0);
                    }
                    break;
            }

            return bResult;
        }

    }
}
