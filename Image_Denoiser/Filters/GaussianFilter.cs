using System.Drawing;
using OpenCvSharp;
using OpenCvSharp.Extensions;

namespace PROTO.Filters
{
    internal class GaussianFilter : FilterOptionBase
    {
        public override string FilterName => "Gaussian";

        internal override Image FitlerImage(Image inputImage)
        {
            using Mat input = BitmapConverter.ToMat((Bitmap)inputImage);
            using Mat output = new Mat(input.Size(), input.Type());
            Cv2.GaussianBlur(input, output, new OpenCvSharp.Size(9, 9), 0, 0);
            var outputBitmap = BitmapConverter.ToBitmap(output);
            return outputBitmap;
        }
    }
}
