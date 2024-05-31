using System;
using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PROTO.Filters
{
    internal class AdaptiveWienerFilter : FilterOptionBase
    {
        public override string FilterName => "Adaptive Wiener";

        internal override Image FitlerImage(Image inputImage)
        {
            using Mat input = BitmapConverter.ToMat((Bitmap)inputImage);

            var width = input.Cols;
            var height = input.Rows;
            using MatOfByte output = new MatOfByte(height, width);

            var means = new MatOfDouble();
            var sqrMeans = new MatOfDouble();
            var variances = new MatOfDouble();
            var sqrInput = input.Mul(input).ToMat();
            var avgVariance = new MatOfDouble();
            
            var ksize = new OpenCvSharp.Size(5, 5);
            var anchor = new OpenCvSharp.Point(-1, -1);
            Cv2.BoxFilter(input, means, MatType.CV_64F, ksize, anchor, true, BorderTypes.Replicate);
            Cv2.BoxFilter(sqrInput, sqrMeans, MatType.CV_64F, ksize, anchor, true, BorderTypes.Replicate);

            var means2 = means.Mul(means).ToMat();
            MatOfDouble tempMeans2 = new MatOfDouble();
            means2.ConvertTo(tempMeans2, MatType.CV_64F);
            (sqrMeans - tempMeans2).ToMat().ConvertTo(variances, MatType.CV_64F);

            Cv2.Reduce(variances, avgVariance, ReduceDimension.Column, ReduceTypes.Sum, -1);
            Cv2.Reduce(avgVariance, avgVariance, ReduceDimension.Row, ReduceTypes.Sum, -1);

            double noiseVariance = Math.Abs(avgVariance.Get<double>(0, 0) / (width * height));

            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    var s = input.Get<byte>(i, j);
                    var m = means.Get<double>(i, j);
                    var v = variances.Get<double>(i, j);
                    var value = SaturateCast(m
                        + Math.Max(0f, v - noiseVariance)
                        / Math.Max(v, noiseVariance)
                        * (s - m)
                     );
                    output.Set(i, j, value);
                }
            }
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
