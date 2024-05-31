using System;
using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PROTO.Filters
{
    internal class AnisotropicKuwaharaFilter : FilterOptionBase
    {
        public override string FilterName => "Anisotropic Kuwahara";

        internal override Image FitlerImage(Image inputImage)
        {
            using Mat input = BitmapConverter.ToMat((Bitmap)inputImage);

            var width = input.Cols;
            var height = input.Rows;
            using MatOfByte output = new MatOfByte(height, width);

            var kernel = new Mat(7, 7, MatType.CV_64FC1);
            var hgrid = new Mat(7, 7, MatType.CV_64FC1);

            for (int i = 0; i < 7; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    kernel.Set<double>(i, j, (i - (7f / 2f)));
                    hgrid.Set<double>(i, j, (j - (7f / 2f)));
                }
            }

            //Directional elipse kernel
            kernel = kernel.Pow(2);
            hgrid = hgrid.Pow(2);
            kernel.Add(hgrid);
            kernel /= -8f;
            kernel = kernel.Exp();
            kernel *= (1f / (8f * Math.PI));

            Mat[] channels = new Mat[3];
            Cv2.Split(kernel, out channels);
            Mat gray = new Mat();


            output.ConvertTo(output, MatType.CV_8UC3);
            var outputImage = BitmapConverter.ToBitmap(output);
            return outputImage;
        }
        private byte SaturateCast(double value)
        {
            var rounded = (byte)Math.Round(value, 0);
            return Math.Min(Math.Max(byte.MinValue, rounded), byte.MaxValue);
        }
    }
}
