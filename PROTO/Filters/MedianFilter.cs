using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PROTO.Filters
{
    internal class MedianFilter : FilterOptionBase
    {
        public override string FilterName => "Median";

        internal override Image FitlerImage(Image inputImage)
        {
            using Mat input = BitmapConverter.ToMat((Bitmap)inputImage);
            using Mat output = new Mat(input.Size(), input.Type());
            //int sizeKernel = 9;
            //double mean = 0f;

            //for (int i = 1; i < inputImage.Width - 1; i++)
            //{
            //    for (int j = 1; j < inputImage.Height - 1; j++)
            //    {
            //        mean = (input.At<byte>(i - 1, j - 1)
            //            + input.At<byte>(i - 1, j - 1)) / sizeKernel;
            //        output. = mean;
            //    }
            //}
            Cv2.MedianBlur(input, output, 9);
            var outputBitmap = BitmapConverter.ToBitmap(output);
            return outputBitmap;
        }
    }
}
