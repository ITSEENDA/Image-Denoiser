using System.Drawing;
using OpenCvSharp.Util;
using OpenCvSharp;
using OpenCvSharp.Extensions;
//using OpenCvSharp.

namespace PROTO.Filters
{
    internal class WienerFilter : FilterOptionBase
    {
        public override string FilterName => "Wiener";

        internal override Image FitlerImage(Image inputImage)
        {
            using Mat input = BitmapConverter.ToMat((Bitmap)inputImage);
            using MatOfByte output = new MatOfByte();

            var width = input.Cols;
            var height = input.Rows;

            //MatOfDouble means, sqrtMeans, variances = new MatOfDouble(width, height, 0);
            //MatOfDouble avgVariance;

            //var ksize = new OpenCvSharp.Size(3, 3);
            //var anchor = new OpenCvSharp.Point(-1, -1);
            //Cv2.BoxFilter(input, means, MatType.CV_64FC1, ksize, anchor, true, BorderTypes.Replicate);

            //var means2 = means.Mul(means);
            //Cv2
            //for (int i = 0; i < height; i++)
            //{
            //    var srcRow = input.Ptr(i);
            //    var dstRow = output.Ptr(i);
            //    var varRow = variances.Ptr(i);
            //    var meanRow = means.Ptr(i);
            //}
            var outputImage = BitmapConverter.ToBitmap(output);
            return outputImage;
        }
    }
}
